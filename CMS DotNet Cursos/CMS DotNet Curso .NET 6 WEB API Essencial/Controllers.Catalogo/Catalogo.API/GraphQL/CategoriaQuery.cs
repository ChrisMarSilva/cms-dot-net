using Catalogo.Infrastructure.Context.Interfaces;
using GraphQL;
using GraphQL.Types;

namespace Catalogo.API.GraphQL;

//Mapeamos os campos para uma dada consulta para uma chamada no repositorio (CategoriasRepository)
public class CategoriaQuery : ObjectGraphType
{
    //recebe a instância do nosso UnitOfWork que contém  as instãncias dos repositórios
    public CategoriaQuery(IUnitOfWork _uow)
    {
        // nosso método vai retornar um objeto Categoria
        Field<CategoriaType>("categoria",
            arguments: new QueryArguments(
                new QueryArgument<IntGraphType>() { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return _uow.Categorias.GetByIdNoTrackingAsync(c => c.Id == id).Result;
                });

        //nosso método vai retornar uma lista de objetos categoria aqui resolve vai mapear a requisição do cliente com os dados da consulta Get definida em CategoriaRepository
        Field<ListGraphType<CategoriaType>>("categorias",
            resolve: context =>
            {
                return _uow.Categorias.GetCategoriasAsync(null).Result;
            });
    }
}