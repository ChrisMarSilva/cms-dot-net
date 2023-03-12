using Dapper.Contrib.Extensions;
using Microsoft.EntityFrameworkCore;
using Tarefas.Data.Persistence;
using Tarefas.Data.Persistence.Interfaces;
using Tarefas.Domain.Models;

namespace Tarefas.API.Endpoints;

public static class TarefasEndpoints
{
    public static void UseMapTarefasEndpoints(this WebApplication app)
    {

        //app.MapGet("/tarefas", async (GetConnection connectionGetter) =>
        //{
        //    using var con = await connectionGetter();
        //    var tarefas = con.GetAll<Tarefa>().ToList();

        //    if (tarefas is null)
        //        return Results.NotFound();

        //    return Results.Ok(tarefas);
        //});

        app.MapGet("/tarefas", async (AppDbContext ctx) => {
            var tarefas = await ctx.Tarefas.ToListAsync(); //  ?? new Tarefa()
            if (tarefas is null || !tarefas.Any())
                return Results.NotFound();
            return Results.Ok(tarefas);
        });

        //app.MapGet("/tarefas/{id}", async (GetConnection connectionGetter, int id) =>
        //{
        //    using var con = await connectionGetter();
        //    //var tarefa = con.Get<Tarefa>(id);
        //    //if (tarefa is null)
        //    //    return Results.NotFound();
        //    //return Results.Ok(tarefa);

        //    return con.Get<Tarefa>(id) is Tarefa tarefa ? Results.Ok(tarefa) : Results.NotFound();
        //});
        app.MapGet("/tarefas/{id:Guid}", async (AppDbContext ctx, Guid id) => {
            var tarefa = await ctx.Tarefas.FirstOrDefaultAsync(d => d.Id == id); //  ?? new Tarefa()
            if (tarefa is null)
                return Results.NotFound();
            return Results.Ok(tarefa);
        });

        //app.MapPost("/tarefas", async (GetConnection connectionGetter, Tarefa Tarefa) =>
        //{
        //    using var con = await connectionGetter();
        //    var id = con.Insert(Tarefa);
        //    return Results.Created($"/tarefas/{id}", Tarefa);
        //});

        app.MapPost("/tarefas", async (AppDbContext ctx, Tarefa input) => {
            await ctx.Tarefas.AddAsync(input);
            await ctx.SaveChangesAsync();
            return Results.Created($"/tarefas/{input.Id}", input);
        });

        //app.MapPut("/tarefas", async (GetConnection connectionGetter, Tarefa Tarefa) =>
        //{
        //    using var con = await connectionGetter();
        //    var id = con.Update(Tarefa);
        //    return Results.Ok();
        //});

        app.MapPut("/tarefas/{id:Guid}", async (AppDbContext ctx, Guid id, Tarefa input) => {
            var tarefa = await ctx.Tarefas.FirstOrDefaultAsync(d => d.Id == id);
            if (tarefa is null)
                return Results.NotFound();
            tarefa.Update(atividade: input.Atividade, status: input.Status);
            ctx.Tarefas.Update(tarefa);
            await ctx.SaveChangesAsync();
            return Results.NoContent();
        });

        //app.MapDelete("/tarefas/{id}", async (GetConnection connectionGetter, int id) =>
        //{
        //    using var con = await connectionGetter();

        //    var deleted = con.Get<Tarefa>(id);

        //    if (deleted is null)
        //        return Results.NotFound();

        //    con.Delete(deleted);
        //    return Results.Ok(deleted);
        //});

        app.MapDelete("/tarefas/{id:Guid}", async (AppDbContext ctx, Guid id) => {

            var tarefa = await ctx.Tarefas.FirstOrDefaultAsync(d => d.Id == id);
            if (tarefa is null)
                return Results.NotFound();
            ctx.Tarefas.Remove(tarefa);
            await ctx.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
