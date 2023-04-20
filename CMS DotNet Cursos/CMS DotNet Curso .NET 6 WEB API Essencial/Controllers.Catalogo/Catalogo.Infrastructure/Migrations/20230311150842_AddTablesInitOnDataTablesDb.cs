using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.API.Migrations;

/// <inheritdoc />
public partial class AddTablesInitOnDataTablesDb : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Categoria",
            columns: table => new
            {
                id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                nome = table.Column<string>(type: "varchar(80)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                imagem_url = table.Column<string>(type: "varchar(300)", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                data_cadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                data_alteracao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categoria", x => x.id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Produto",
            columns: table => new
            {
                id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                nome = table.Column<string>(type: "varchar(80)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                descricao = table.Column<string>(type: "varchar(300)", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                imagem_url = table.Column<string>(type: "varchar(300)", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                estoque = table.Column<float>(type: "float", nullable: false),
                categoria_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                data_cadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                data_alteracao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Produto", x => x.id);
                table.ForeignKey(
                    name: "FK_Produto_Categoria_categoria_id",
                    column: x => x.categoria_id,
                    principalTable: "Categoria",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Produto_categoria_id",
            table: "Produto",
            column: "categoria_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Produto");

        migrationBuilder.DropTable(
            name: "Categoria");
    }
}
