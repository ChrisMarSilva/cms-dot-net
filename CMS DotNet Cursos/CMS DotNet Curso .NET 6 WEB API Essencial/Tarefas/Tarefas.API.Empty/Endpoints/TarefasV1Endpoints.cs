using Microsoft.EntityFrameworkCore;
using Tarefas.Data.Persistence;
using Tarefas.Domain.Models;

namespace Tarefas.API.Empty.Endpoints;

public static class TarefasV1Endpoints
{
    public static void UseMapTarefasV1Endpoints(this WebApplication app)
    {
        app.MapGet("/api/v1/tarefas", async (AppDbContext ctx) =>
        {
            var results = await ctx.Tarefas.ToListAsync(); //  ?? new tarefa()
            if (results is null || !results.Any())
                return Results.NotFound();
            return Results.Ok(results);
        });

        app.MapGet("/api/v1/tarefas/{id:guid}", async (AppDbContext ctx, Guid id) =>
        {
            var result = await ctx.Tarefas.FirstOrDefaultAsync(d => d.Id == id); //  ?? new tarefa()
            if (result is null)
                return Results.NotFound();
            return Results.Ok(result);
        });

        app.MapPost("/api/v1/tarefas", async (AppDbContext ctx, Tarefa input) =>
        {
            await ctx.Tarefas.AddAsync(input);
            await ctx.SaveChangesAsync();
            return Results.Created($"/tarefas/{input.Id}", input);
        });

        app.MapPut("/api/v1/tarefas/{id:guid}", async (AppDbContext ctx, Guid id, Tarefa input) =>
        {
            var tarefa = await ctx.Tarefas.FirstOrDefaultAsync(d => d.Id == id);
            if (tarefa is null)
                return Results.NotFound();
            tarefa.Update(atividade: input.Atividade, status: input.Status);
            ctx.Tarefas.Update(tarefa);
            await ctx.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("/api/v1/tarefas/{id:guid}", async (AppDbContext ctx, Guid id) =>
        {
            var tarefa = await ctx.Tarefas.FirstOrDefaultAsync(d => d.Id == id);
            if (tarefa is null)
                return Results.NotFound();
            ctx.Tarefas.Remove(tarefa);
            await ctx.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
