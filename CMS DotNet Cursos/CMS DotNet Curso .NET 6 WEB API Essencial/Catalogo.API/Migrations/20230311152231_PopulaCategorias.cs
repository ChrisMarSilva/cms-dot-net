using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalogo.API.Migrations;

/// <inheritdoc />
public partial class PopulaCategorias : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Produto",
            keyColumn: "id",
            keyValue: new Guid("66cfdb2d-fb0d-401e-8f4c-260923210607"));

        migrationBuilder.DeleteData(
            table: "Produto",
            keyColumn: "id",
            keyValue: new Guid("896ffedf-3169-4058-9c62-e9f54e821ac2"));

        migrationBuilder.DeleteData(
            table: "Produto",
            keyColumn: "id",
            keyValue: new Guid("aab9689a-3b89-4896-a060-e3b0ca7ad5bc"));

        migrationBuilder.DeleteData(
            table: "Categoria",
            keyColumn: "id",
            keyValue: new Guid("a93044ab-55a8-40f6-9edb-bcf5848034d0"));

        migrationBuilder.DeleteData(
            table: "Categoria",
            keyColumn: "id",
            keyValue: new Guid("b32a2a72-28df-4c9f-9b42-d57546f64b59"));

        migrationBuilder.DeleteData(
            table: "Categoria",
            keyColumn: "id",
            keyValue: new Guid("d96986d5-4889-45a7-a031-4f2c663251d6"));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Categoria",
            columns: new[] { "id", "data_alteracao", "data_cadastro", "imagem_url", "nome" },
            values: new object[,]
            {
                { new Guid("a93044ab-55a8-40f6-9edb-bcf5848034d0"), null, new DateTime(2023, 3, 11, 15, 19, 11, 800, DateTimeKind.Utc).AddTicks(3085), null, "Categora 02" },
                { new Guid("b32a2a72-28df-4c9f-9b42-d57546f64b59"), null, new DateTime(2023, 3, 11, 15, 19, 11, 800, DateTimeKind.Utc).AddTicks(3090), null, "Categora 03" },
                { new Guid("d96986d5-4889-45a7-a031-4f2c663251d6"), null, new DateTime(2023, 3, 11, 15, 19, 11, 800, DateTimeKind.Utc).AddTicks(3066), null, "Categora 01" }
            });

        migrationBuilder.InsertData(
            table: "Produto",
            columns: new[] { "id", "categoria_id", "data_alteracao", "data_cadastro", "descricao", "estoque", "imagem_url", "nome", "preco" },
            values: new object[,]
            {
                { new Guid("66cfdb2d-fb0d-401e-8f4c-260923210607"), new Guid("d96986d5-4889-45a7-a031-4f2c663251d6"), null, new DateTime(2023, 3, 11, 15, 19, 11, 800, DateTimeKind.Utc).AddTicks(3274), null, 100f, null, "Produto 01", 10m },
                { new Guid("896ffedf-3169-4058-9c62-e9f54e821ac2"), new Guid("b32a2a72-28df-4c9f-9b42-d57546f64b59"), null, new DateTime(2023, 3, 11, 15, 19, 11, 800, DateTimeKind.Utc).AddTicks(3289), null, 300f, null, "Produto 03", 30m },
                { new Guid("aab9689a-3b89-4896-a060-e3b0ca7ad5bc"), new Guid("a93044ab-55a8-40f6-9edb-bcf5848034d0"), null, new DateTime(2023, 3, 11, 15, 19, 11, 800, DateTimeKind.Utc).AddTicks(3284), null, 200f, null, "Produto 02", 20m }
            });
    }
}
