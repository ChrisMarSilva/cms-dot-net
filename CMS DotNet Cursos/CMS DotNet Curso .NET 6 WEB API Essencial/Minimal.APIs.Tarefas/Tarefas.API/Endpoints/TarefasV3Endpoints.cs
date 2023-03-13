using Dapper.Contrib.Extensions;
using Tarefas.Domain.Models;
using static Tarefas.Data.Persistence.AppDbContextCon;

namespace Tarefas.API.Endpoints;

public static class TarefasV3Endpoints
{
    public static void UseMapTarefasV3Endpoints(this WebApplication app)
    {
        app.MapGet("/api/v3/tarefas", async (GetConnection connectionGetter) =>
        {
            using var con = await connectionGetter();
            var tarefas = con.GetAll<TarefaCon>().ToList();
            //var tarefas = con.Query<TarefaCon>("Select id, atividade, status, data_cadastro, data_alteracao from Tarefas")
            //    .Select(x => new TarefaCon(x.Id, x.Atividade, x.Status, x.Data_Cadastro, x.Data_Alteracao))
            //    .ToList();
            if (tarefas is null || !tarefas.Any())
                return Results.NotFound();
            return Results.Ok(tarefas);
        });

        app.MapGet("/api/v3/tarefas/{id:Guid}", async (GetConnection connectionGetter, Guid id) =>
        {
            using var con = await connectionGetter();
            //var tarefa = con.Get<TarefaCon>(id);
            //if (tarefa is null)
            //    return Results.NotFound();
            //return Results.Ok(tarefa);
            return con.Get<TarefaCon>(id) is TarefaCon tarefa ? Results.Ok(tarefa) : Results.NotFound();
        });

        app.MapPost("/api/v3/tarefas", async (GetConnection connectionGetter, TarefaCon tarefa) =>
        {
            using var con = await connectionGetter();
            var id = con.Insert(tarefa);
            return Results.Created($"/tarefas/{id}", tarefa);
        });

        app.MapPut("/api/v3/tarefas", async (GetConnection connectionGetter, TarefaCon tarefa) =>
        {
            using var con = await connectionGetter();
            var id = con.Update(tarefa);
            return Results.Ok();
        });

        app.MapDelete("/api/v3/tarefas/{id:Guid}", async (GetConnection connectionGetter, Guid id) =>
        {
            using var con = await connectionGetter();
            var deleted = con.Get<TarefaCon>(id);
            if (deleted is null) return Results.NotFound();
            con.Delete(deleted);
            return Results.Ok(deleted);
        });
    }
}
