﻿using FluentValidation;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebApplication1.Filters
{
    public class ValidationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is ValidationException)
            {
                var resultado = new ResultadoValidacao("Ocorreram erros de validacao nessa requisicao. Verifique a lista de erros.");

                (actionExecutedContext.Exception as ValidationException).Errors.ToList().ForEach(e => resultado.AdicionarErro(e.PropertyName, e.ErrorMessage));

                var resposta = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new System.Net.Http.ObjectContent<ResultadoValidacao>(
                        resultado,
                        new System.Net.Http.Formatting.JsonMediaTypeFormatter())
                };

                actionExecutedContext.Response = resposta;
            }
        }
    }
}