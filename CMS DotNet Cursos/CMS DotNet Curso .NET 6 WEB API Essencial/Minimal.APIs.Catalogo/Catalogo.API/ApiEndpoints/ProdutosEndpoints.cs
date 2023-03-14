using Catalogo.API.Context;
using Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.API.ApiEndpoints;

public static class ProdutosEndpoints
{
    public static void MapProdutosEndpoints(this WebApplication app)
    {
        app.MapPost("/produtos", async ([FromBody] Produto prod, [FromServices] AppDbContext ctx) =>
        {
            await ctx.Produtos.AddAsync(prod);
            await ctx.SaveChangesAsync();

            return Results.Created($"/produtos/{prod.ProdutoId}", prod);
        }).Accepts<Produto>("application/json")
          .Produces<Produto>(StatusCodes.Status201Created)
          .WithName("CriarNovoProduto")
          .WithTags("Produtos");

        app.MapGet("/produtos", async (AppDbContext ctx) => await ctx.Produtos.ToListAsync()
           ).Produces<IEnumerable<Produto>>(StatusCodes.Status200OK)
            .WithTags("Produtos")
            .RequireAuthorization();

        app.MapGet("/produtos/{id:int}", async (int id, AppDbContext ctx) =>
        {
            return await ctx.Produtos.FindAsync(id) is Produto prod
                         ? Results.Ok(prod)
                         : Results.NotFound();
        }).Produces<Produto>(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status404NotFound)
          .WithTags("Produtos")
          .RequireAuthorization();

        app.MapPut("/produtos/{id:int}", async (int id, Produto input, AppDbContext ctx) =>
        {
            if (input.ProdutoId != id)
                return Results.BadRequest();

            var prod = await ctx.Produtos.FindAsync(id);

            if (prod is null) 
                return Results.NotFound();

            prod.Nome = input.Nome;
            prod.Descricao = input.Descricao;
            prod.Preco = input.Preco;
            prod.Imagem = input.Imagem;
            prod.DataCompra = input.DataCompra;
            prod.Estoque = input.Estoque;
            prod.CategoriaId = input.CategoriaId;
            await ctx.SaveChangesAsync();

            return Results.Ok(prod); 
        }).Produces<Produto>(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status400BadRequest)
          .Produces(StatusCodes.Status404NotFound)
          .WithTags("Produtos")
          .RequireAuthorization();

        app.MapDelete("/produtos/{id:int}", async (int id, AppDbContext ctx) =>
        {
            var prod = await ctx.Produtos.FindAsync(id);

            if (prod is null)
                return Results.NotFound();

            ctx.Produtos.Remove(prod);
            await ctx.SaveChangesAsync();

            return Results.NoContent();
        }).Produces(StatusCodes.Status204NoContent)
          .Produces(StatusCodes.Status404NotFound)
          .WithTags("Produtos")
          .RequireAuthorization();
    }
}
