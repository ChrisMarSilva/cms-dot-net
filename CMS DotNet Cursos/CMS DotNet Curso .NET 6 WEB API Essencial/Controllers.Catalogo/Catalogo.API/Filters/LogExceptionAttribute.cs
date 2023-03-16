using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Authentication;

namespace Catalogo.API.Filters;

public class LogExceptionAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is AuthenticationException || context.Exception is UnauthorizedAccessException)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        context.Result = GenerateErrorResult(context);
    }

    private ObjectResult GenerateErrorResult(ExceptionContext context)
    {
        var runResult = new
        {
            Success = false,
            SummaryMessage = "Falha ao processar requisição",
            Exception = context.Exception
        };

        var contextResult = new ObjectResult(runResult)
        {
            StatusCode = ExtractStatusCode(context.Exception)
        };
        return contextResult;
    }

    private int ExtractStatusCode(Exception contextException)
    {
        if (contextException is DivideByZeroException)
            return 401;
        return 400;
    }
}
