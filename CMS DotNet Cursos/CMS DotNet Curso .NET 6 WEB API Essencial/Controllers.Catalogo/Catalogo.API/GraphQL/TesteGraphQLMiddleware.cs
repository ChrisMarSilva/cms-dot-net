using Catalogo.Data.Persistence.Interfaces;
using GraphQL;
using GraphQL.Types;
// using GraphQL.Http;

namespace Catalogo.API.GraphQL;

//É incluido no pipeline do request para processar  a requisição http usando a instãncia do nosso repositorio
public class TesteGraphQLMiddleware
{
    private readonly RequestDelegate _next; // instancia para processar o request http 
    private readonly IUnitOfWork _uow; // instancia do UnitOfWork 

    public TesteGraphQLMiddleware(RequestDelegate next, IUnitOfWork uow)
    {
        _next = next;
        _uow = uow;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        // verifica se o caminho do request é /graphql
        if (httpContext.Request.Path.StartsWithSegments("/graphql"))
        {
            // tenta ler o corpo do request usando um StreamReader
            using (var stream = new StreamReader(httpContext.Request.Body))
            {
                var query = await stream.ReadToEndAsync();

                if (!String.IsNullOrWhiteSpace(query))
                {
                    //um objeto schema é criado com a propridade Query definida com uma instância do nosso contexto (repositorio)
                    var schema = new Schema
                    {
                        Query = new CategoriaQuery(_uow)
                    };

                    //Criamos um  DocumentExecuter que  Executa a consulta contra o schema e o resultado é escrito no response como JSON via WriteResult
                    var result = await new DocumentExecuter().ExecuteAsync(options =>
                    {
                        options.Schema = schema;
                        options.Query = query;
                    });

                    // await WriteResult(httpContext, result);
                }
            }
        }
        else
        {
            await _next(httpContext);
        }
    }

    //private async Task WriteResult(HttpContext httpContext, ExecutionResult result)
    //{
    //    var json = new DocumentWriter(indent: true).Write(result);
    //    httpContext.Response.StatusCode = 200;
    //    httpContext.Response.ContentType = "application/json";
    //    await httpContext.Response.WriteAsync(json);
    //}

}