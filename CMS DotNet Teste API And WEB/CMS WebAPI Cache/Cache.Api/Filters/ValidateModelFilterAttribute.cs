using Cache.Api.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Cache.Api.Filters;

public sealed class ValidateModelFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;

        var erroResponseDto = ErrorResponseDto.Iniciar(HttpStatusCode.BadRequest, "Payload inválido");

        foreach (string key in context.ModelState.Keys)
        {
            var modelStateEntry = context.ModelState[key];

            if (modelStateEntry == null || 
                modelStateEntry.ValidationState != ModelValidationState.Invalid)
                continue;

            var erro = erroResponseDto.AddErro(key);

            foreach (ModelError error in modelStateEntry.Errors)
            {
                erro.AddMensagem(error.ErrorMessage);
            }
        }

        context.Result = new BadRequestObjectResult(erroResponseDto);
    }
}
