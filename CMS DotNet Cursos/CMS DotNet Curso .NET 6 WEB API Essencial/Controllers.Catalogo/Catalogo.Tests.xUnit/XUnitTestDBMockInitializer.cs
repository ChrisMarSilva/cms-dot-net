using Catalogo.Domain.Models;
using Catalogo.Infrastructure.Context;

namespace Catalogo.Tests.xUnit;

public class XUnitTestDBMockInitializer
{
    public XUnitTestDBMockInitializer() { }

    public void SeedCategorias(AppDbContext context)
    {
        // Guid.NewGuid() // Guid.Parse("9940548e-dadc-405f-83f9-57431685cf5d")
        context.Categorias.Add(new Categoria(Guid.Parse("908065a4-8080-481b-b9d3-585565068501"), "Sucos", "sucos1.jpg"));
        context.Categorias.Add(new Categoria(Guid.Parse("816bb968-99a7-4740-8995-6bad95b026e6"), "Doces", "doces1.jpg"));
        context.Categorias.Add(new Categoria(Guid.Parse("b92e26d7-c8d0-476d-b21f-bb2ff35bfc36"), "Salgados", "Salgados1.jpg"));
        context.Categorias.Add(new Categoria(Guid.Parse("3b24d169-d64f-4895-a6c8-9d7dbf5c9595"), "Tortas", "tortas1.jpg"));
        context.Categorias.Add(new Categoria(Guid.Parse("5d29e67f-f070-4c07-a540-c0141220158a"), "Bolos", "bolos1.jpg"));
        context.Categorias.Add(new Categoria(Guid.Parse("019a76c0-cefd-4b1a-9efb-c7e08c09c3e3"), "Lanches", "lanches1.jpg"));
        context.Categorias.Add(new Categoria(Guid.Parse("d467b043-4bb3-4deb-ba83-540f52556dbd"), "Bebidas999", "bebidas999.jpg"));
        context.SaveChanges();
    }
}
