using Catalogo.Domain.Models;
using GraphQL.Types;

namespace Catalogo.API.GraphQL;

public class CategoriaType : ObjectGraphType<Categoria>
{
    public CategoriaType()
    {
        // campos do Type
        Field(x => x.Id);
        Field(x => x.Nome);
        Field(x => x.ImagemUrl);
        Field<ListGraphType<CategoriaType>>("categorias");
    }
}
