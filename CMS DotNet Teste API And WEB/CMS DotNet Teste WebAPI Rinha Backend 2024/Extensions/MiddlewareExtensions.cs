using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rinha.Backend._2024.API.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseDefaultExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(delegate (IApplicationBuilder errorApp)
        {
            errorApp.Run(async delegate (HttpContext context)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                //var value = ErrorResponseDto.Begin(HttpStatusCode.InternalServerError, "Falha interna durante o processamento. Tente novamente.");
                var value = Results.UnprocessableEntity("Payload inválido.");
                await context.Response.WriteAsync(JsonSerializer.Serialize(value, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));
            });
        });

        return app;
    }
    public static IApplicationBuilder UseDefaultStatusCodePages(this IApplicationBuilder app)
    {
        app.UseStatusCodePages(async delegate (StatusCodeContext context)
        {
            //var value = ErrorResponseDto.Begin((HttpStatusCode)context.HttpContext.Response.StatusCode, ReasonPhrases.GetReasonPhrase(context.HttpContext.Response.StatusCode));
            var value = Results.UnprocessableEntity(ReasonPhrases.GetReasonPhrase(context.HttpContext.Response.StatusCode));
            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(value, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));
        });

        return app;
    }

    public static IApplicationBuilder UseDefaultCors(this IApplicationBuilder app)
    {
        return app.UseCors("CorsPolicy");
    }
}
