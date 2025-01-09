using Cache.Api.Contracts.Responses;
using System.Net;

namespace Cache.Api.Middlewares;

public class IdempotencyMiddleware
{
    private const string _headerName = "Idempotency-Key";
    private readonly RequestDelegate _next; // RequestDelegate // ActionExecutionDelegate

    public IdempotencyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context) // HttpContext // ActionExecutingContext
    {
        if (!context.Request.Headers.TryGetValue(_headerName, out var idempotenceKeyValue) ||  // ttpContext.Request
            string.IsNullOrWhiteSpace(idempotenceKeyValue) ||
            !Guid.TryParse(idempotenceKeyValue, out Guid idempotenceKey))
        {
            var error = ErroResponseDto.Iniciar(HttpStatusCode.BadRequest, $"Header {_headerName} obrigatório"); 
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = context.Request.ContentType ?? "application/json; charset=utf-8";
            await context.Response.WriteAsJsonAsync(error);
            //context.Result = new BadRequestObjectResult(error);
            return;
        }

        //await _next();
        await _next(context);
        //await _next.Invoke(context.HttpContext);
    }
}
