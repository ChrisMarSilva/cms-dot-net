using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.Domain.Dtos.Response;
using System.Net;

namespace Project.Filters;

public sealed class ValidateModelFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;

        var errorResponseDto = ErrorResponseDto.Begin(HttpStatusCode.BadRequest, "Payload inválido");
        
        foreach (string key in context.ModelState.Keys)
        {
            var modelStateEntry = context.ModelState[key];
            if (modelStateEntry == null || modelStateEntry.ValidationState != ModelValidationState.Invalid)
                continue;

            var error = errorResponseDto.AddError(key);
            foreach (ModelError error2 in modelStateEntry.Errors)
            {
                error.AddDescription(error2.ErrorMessage);
            }
        }

        context.Result = new BadRequestObjectResult(errorResponseDto.End());
    }
}