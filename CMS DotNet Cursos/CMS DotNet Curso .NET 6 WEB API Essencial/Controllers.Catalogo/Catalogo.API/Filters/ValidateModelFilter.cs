using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalogo.API.Filters;

public class ValidateModelFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var erroViewModel = new ErroResponseVM(400, "Solicitação Incorreta");
            foreach (var key in context.ModelState.Keys)
            {
                var modelStateVal = context.ModelState[key];
                foreach (var error in modelStateVal.Errors)
                {
                    erroViewModel.AdicionarErro(key, error.ErrorMessage);
                }
            }
            context.Result = new BadRequestObjectResult(erroViewModel);
        }
    }
}

public class ErroResponseVM
{
    public ErroResponseVM(int num, string texto)
    {

    }

    public void AdicionarErro(string key, string message)
    { 
    
    }
}
