using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.API.Migrations;

/// <inheritdoc />
public partial class PopulaCategorias02 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder mb)
    {
        mb.Sql("Insert into Categoria(id, nome, imagem_url, data_cadastro) Values(uuid(), 'Bebidas',    'bebidas.jpg',    now())");
        mb.Sql("Insert into Categoria(id, nome, imagem_url, data_cadastro) Values(uuid(), 'Lanches',    'lanches.jpg',    now())");
        mb.Sql("Insert into Categoria(id, nome, imagem_url, data_cadastro) Values(uuid(), 'Sobremesas', 'sobremesas.jpg', now())");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder mb)
    {
        mb.Sql("Delete from Categoria");
    }
}
