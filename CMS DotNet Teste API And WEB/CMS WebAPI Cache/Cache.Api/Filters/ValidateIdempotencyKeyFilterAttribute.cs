using Cache.Api.Contracts.Responses;
using Cache.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cache.Api.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class ValidateIdempotencyKeyFilterAttribute : ActionFilterAttribute, IAsyncActionFilter
{
    private readonly string _headerName;
    private readonly TimeSpan _cacheDuration;

    public ValidateIdempotencyKeyFilterAttribute(string headerName = "Idempotency-Key", int cacheTimeInMinutes = 60)
    {
        _headerName = headerName;
        _cacheDuration = TimeSpan.FromMinutes(cacheTimeInMinutes); // FromSeconds // FromMinutes
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        // Parse the Idempotence-Key header from the request
        if (!context.HttpContext.Request.Headers.TryGetValue(_headerName, out StringValues idempotenceKeyValue) ||
           string.IsNullOrWhiteSpace(idempotenceKeyValue) ||
           !Guid.TryParse(idempotenceKeyValue, out Guid idempotenceKey))
        {
            context.Result = new BadRequestObjectResult(ErrorResponseDto.Iniciar(HttpStatusCode.BadRequest, $"Header {_headerName} obrigatório"));
            return;
        }

        // Check if we already processed this request and return a cached response (if it exists)
        var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
        var cacheKey = $"idempotent:{context.HttpContext.Request.Path}:{idempotenceKey}"; // context.HttpContext.Request.Path
        var cachedResult = await cacheService.GetCacheValueAsync<string>(cacheKey);

        if (cachedResult is not null)
        {
            var response = JsonSerializer.Deserialize<IdempotentResponse>(cachedResult)!;
            context.Result = new ObjectResult(response.Value) { StatusCode = response.StatusCode };
            context.HttpContext.Response.Headers.Add("X-Idempotency-Key", idempotenceKeyValue);
            context.HttpContext.Response.Headers.Add("X-Idempotency-From", "Cache");
            return;
        }

        // Execute the request and cache the response for the specified duration
        context.HttpContext.Response.Headers.Add("X-Idempotency-From", "BD");
        var executedContext = await next();

        if (executedContext.Result is ObjectResult { StatusCode: >= 200 and < 300 } objectResult)
        {
            var statusCode = objectResult.StatusCode ?? StatusCodes.Status200OK;
            var response = new IdempotentResponse(statusCode, objectResult.Value);
            var json = JsonSerializer.Serialize<IdempotentResponse>(response);
            await cacheService.SetCacheValueAsync<string>(cacheKey, json, _cacheDuration);
        }
    }
}

internal sealed class IdempotentResponse
{
    [JsonConstructor]
    public IdempotentResponse(int statusCode, object? value)
    {
        StatusCode = statusCode;
        Value = value;
    }

    public int StatusCode { get; }
    public object? Value { get; }
}