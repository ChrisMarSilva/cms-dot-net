using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Project.Filters.Idempotency;

public class IdempotencyMiddleware
{
    private readonly RequestDelegate _next;

    public IdempotencyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request is null || HttpMethods.IsGet(context.Request.Method))
        {
            await _next.Invoke(context);
            return;
        }

        var serviceProvider = context.RequestServices;

        var keyReader = serviceProvider.GetRequiredService<IIdempotencyKeyReader<HttpRequest>>();

        var idempotencyKey = keyReader.Read(context.Request);

        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            await _next.Invoke(context);
            return;
        }

        var options = serviceProvider.GetService<IOptions<IdempotencyOptions>>();
        if (options?.Value.EnableWhiteList ?? false)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            if (endpoint?.Metadata.GetMetadata<IgnoreIdempotencyAttribute>() is not null)
            {
                await _next.Invoke(context);
                return;
            }
        }

        var logger = serviceProvider.GetService<ILogger<IdempotencyMiddleware>>();
        var repository = serviceProvider.GetRequiredService<IIdempotencyRepository>();

        context.Request.EnableBuffering();

        if (logger?.IsEnabled(LogLevel.Information) ?? false)
            logger.LogInformation("Idempotency: Key {idempotencyKey} detected.", idempotencyKey);

        if (await repository.TryAddAsync(idempotencyKey))
        {
            await using var stream = MemoryStreamExtension.RecyclableMemoryStreamManager.GetStream("Idempotency.HttpResponse");
            try
            {
                var originalResponseBody = context.Response.Body;
                context.Response.Body = stream;

                try
                {
                    await _next.Invoke(context);
                }
                finally
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    await stream.CopyToAsync(originalResponseBody);

                    context.Response.Body = originalResponseBody;
                }
            }
            catch (Exception ex)
            {
                await repository.RemoveAsync(idempotencyKey);
                logger?.LogError(ex, "Idempotency Key: {idempotencyKey} - Falha geral no Middleware", idempotencyKey);

                throw;
            }

            if (context.Response.StatusCode >= (int)HttpStatusCode.BadRequest)
            {
                await repository.RemoveAsync(idempotencyKey);

                if (logger?.IsEnabled(LogLevel.Information) ?? false)
                    logger.LogInformation("Idempotency Key: {idempotencyKey} - Falha no primeiro request.", idempotencyKey);

                return;
            }

            if (context.RequestAborted.IsCancellationRequested && stream.Length <= 0)
            {
                if (context.Items[IdempotencyOptions.IdempotencyResponseBodyKey] is ObjectResult { StatusCode: < (int)HttpStatusCode.BadRequest, Value: not null } objectResult)
                {
                    await JsonSerializer.SerializeAsync(stream, objectResult.Value, new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    var updateRegisterWhenRequestAborted = await IdempotencyRegister.Of(idempotencyKey, context.Response.StatusCode, context.Response.ContentType, context.Request.Body, stream);
                    await repository.UpdateAsync(idempotencyKey, updateRegisterWhenRequestAborted);
                    return;
                }

                return;
            }

            var updatedRegister = await IdempotencyRegister.Of(idempotencyKey, context.Response.StatusCode, context.Response.ContentType, context.Request.Body, stream);
            await repository.UpdateAsync(idempotencyKey, updatedRegister);

            if (logger?.IsEnabled(LogLevel.Information) ?? false)
                logger.LogInformation("Idempotency Key: {idempotencyKey} - Primeiro request completo.", idempotencyKey);

            return;
        }

        var register = await repository.GetAsync<IdempotencyRegister>(idempotencyKey);

        if (register is null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = context.Request.ContentType ?? "application/json; charset=utf-8";
            logger?.LogCritical("Idempotency Key: {idempotencyKey} - Falha grave ao localizar o conteúdo original. Provavelmente alguma falha no mecanismo de persistência.", idempotencyKey);
            return;
        }

        if (!register.IsCompleted)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Processing;
            context.Response.ContentType = context.Request.ContentType ?? "application/json; charset=utf-8";
            if (logger?.IsEnabled(LogLevel.Information) ?? false)
                logger.LogInformation("Idempotency Key: {idempotencyKey} - Conflito detectado, repondido 102 (Processing).", idempotencyKey);

            return;
        }

        if (register.HashOfRequest is null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = context.Request.ContentType ?? "application/json; charset=utf-8";
            logger?.LogCritical("Idempotency Key: {idempotencyKey} - Falha grave ao identificar o hash do conteúdo enviado. Provavelmente alguma falha no mecanismo de persistência.", idempotencyKey);
            return;
        }

        var hashOfRequest = await IdempotencyRegister.ComputeHash(context.Request.Body);
        if (!hashOfRequest.SequenceEqual(register.HashOfRequest))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Response.ContentType = context.Request.ContentType ?? "application/json; charset=utf-8";
            logger?.LogCritical("Idempotency Key: {idempotencyKey} - Conflito detectado, mesma chave de idempotência porém conteúdos diferentes. Repondido 409.", idempotencyKey);
            return;
        }

        context.Response.StatusCode = register.StatusCode.GetValueOrDefault(200);
        context.Response.ContentType = register.ContentType;
        await context.Response.Body.WriteAsync(register.Value.ToArray());

        if (logger?.IsEnabled(LogLevel.Information) ?? false)
            logger.LogInformation("Idempotency Key: {idempotencyKey} - Response do cache.", idempotencyKey);
    }
}