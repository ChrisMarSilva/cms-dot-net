using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Dtos.Response;
using System.Net;

namespace Project.Filters.Idempotency;

[AttributeUsage(AttributeTargets.Method)]
public sealed class ValidateIdempotencyKeyAttribute : ActionFilterAttribute
{
    private readonly string _headerName;

    public ValidateIdempotencyKeyAttribute(string headerName = "IdempotenceKey")
    {
        _headerName = headerName;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(_headerName, out var _))
            context.Result = new BadRequestObjectResult(ErrorResponseDto.Begin(HttpStatusCode.BadRequest, "Header " + _headerName + " obrigatório"));
    }
}