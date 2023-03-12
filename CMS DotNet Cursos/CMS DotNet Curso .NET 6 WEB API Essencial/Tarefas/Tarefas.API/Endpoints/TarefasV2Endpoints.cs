using Tarefas.Domain.Models;
using Tarefas.Service.Interfaces;

namespace Tarefas.API.Endpoints;

public static class TarefasV2Endpoints
{
    public static void UseMapTarefasV2Endpoints(this WebApplication app)
    {
        app.MapGet("/api/v2/tarefas", async (ITarefaService service) => {
            var results = await service.GetAllAsync();
            if (results is null || !results.Any())
                return Results.NotFound("No records found");
            return Results.Ok(results);
        });

        app.MapGet("/api/v2/tarefas/{id:Guid}", async (ITarefaService service, Guid id) => {
            var result = await service.GetByIdAsync(id);
            if (result is null || result?.Id == Guid.Empty)
                return Results.NotFound("No record found");
            return Results.Ok(result);
        });

        app.MapPost("/api/v2/tarefas", async (ITarefaService service, Tarefa input) => {
            var result = await service.InsertAsync(input);
            if (result is null || result?.Id == Guid.Empty)
                return Results.BadRequest();
            return Results.Created($"/tarefas/{input.Id}", input);
        });

        app.MapPut("/api/v2/tarefas/{id:Guid}", async (ITarefaService service, Guid id, Tarefa input) => {
            var result = await service.UpdateAsync(id, input);
            if (result is null || result?.Id == Guid.Empty)
                return Results.NotFound("No records found");
            return Results.NoContent();
        });

        app.MapDelete("/api/v2/tarefas/{id:Guid}", async (ITarefaService service, Guid id) => {

            var result = await service.DeleteAsync(id);
            if (!result)
                return Results.NotFound("No records found");
            return Results.NoContent();
        });
    }
}
