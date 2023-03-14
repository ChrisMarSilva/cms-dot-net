using Catalogo.API.Context;
using Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.API.ApiEndpoints;

public static class CategoriasEndpoints
{
    public static void MapCategoriasEndpoints(this WebApplication app)
    {
        app.MapPost("/categorias", async ([FromBody] Categoria input, [FromServices] AppDbContext ctx) =>
        {
            await ctx.Categorias.AddAsync(input);
            await ctx.SaveChangesAsync();

            return Results.Created($"/categorias/{input.CategoriaId}", input);
        }).Accepts<Categoria>("application/json")
          .Produces<Categoria>(StatusCodes.Status201Created)
          .WithName("CriarNovaCategoria")
          .WithTags("Categorias");

        app.MapGet("/categorias", async (AppDbContext ctx) => await ctx.Categorias.ToListAsync()
           ).Produces<IEnumerable<Categoria>>(StatusCodes.Status200OK)
            .WithTags("Categorias")
            .RequireAuthorization();

        app.MapGet("/categorias/{id:int}", async (int id, AppDbContext ctx) =>
        {
            return await ctx.Categorias.FindAsync(id) is Categoria categ
                         ? Results.Ok(categ) 
                         : Results.NotFound();
        }).Produces<Categoria>(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status404NotFound)
          .WithTags("Categorias")
          .RequireAuthorization();

        app.MapPut("/categorias/{id:int}", async (int id, Categoria input, AppDbContext ctx) =>
        {
            if (input.CategoriaId != id)
                return Results.BadRequest();

            var categ = await ctx.Categorias.FindAsync(id);

            if (categ is null) 
                return Results.NotFound();

            categ.Nome = input.Nome;
            categ.Descricao = input.Descricao;
            await ctx.SaveChangesAsync();

            return Results.Ok(categ);
        }).Produces<Categoria>(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status400BadRequest)
          .Produces(StatusCodes.Status404NotFound)
          .WithTags("Categorias")
          .RequireAuthorization();

        app.MapDelete("/categorias/{id:int}", async (int id, AppDbContext ctx) =>
        {
            var categ = await ctx.Categorias.FindAsync(id);

            if (categ is null)
                return Results.NotFound();

            ctx.Categorias.Remove(categ);
            await ctx.SaveChangesAsync();

            return Results.NoContent();
        }).Produces(StatusCodes.Status204NoContent)
          .Produces(StatusCodes.Status404NotFound)
          .WithTags("Categorias")
          .RequireAuthorization();
    }
}
