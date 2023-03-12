using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.API.Migrations;

/// <inheritdoc />
public partial class PopulaProdutos : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder mb)
    {
        mb.Sql("Insert into Produto(id, nome, descricao, preco, imagem_url, estoque, data_cadastro, categoria_id) Values(uuid(), 'Coca-Cola Diet', 'Refrigerante de Cola 350 ml',    5.45, 'cocacola.jpg', 50, now(), (Select id from Categoria where nome = 'Bebidas'))");
        mb.Sql("Insert into Produto(id, nome, descricao, preco, imagem_url, estoque, data_cadastro, categoria_id) Values(uuid(), 'Lanche de Atum', 'Lanche de Atum com maionese',    8.50, 'atum.jpg',     10, now(), (Select id from Categoria where nome = 'Lanches'))");
        mb.Sql("Insert into Produto(id, nome, descricao, preco, imagem_url, estoque, data_cadastro, categoria_id) Values(uuid(), 'Pudim 100 g',    'Pudim de leite condensado 100g', 6.75, 'pudim.jpg',    20, now(), (Select id from Categoria where nome = 'Sobremesas'))");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder mb)
    {
        mb.Sql("Delete from Produto");
    }
}
