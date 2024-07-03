using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Net;

namespace Project.Filters.Idempotency;

public class IdempotencyResult : IActionResult
{
    private readonly HttpStatusCode _httpStatusCode;
    private readonly MemoryStream _body;

    public IdempotencyResult(HttpStatusCode httpStatusCode, MemoryStream body)
    {
        _httpStatusCode = httpStatusCode;
        _body = body;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        await using (_body)
        {
            context.HttpContext.Response.StatusCode = (int)_httpStatusCode;
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;

            await _body.CopyToAsync(context.HttpContext.Response.Body);
            await context.HttpContext.Response.Body.FlushAsync();
        }
    }
}