using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cache.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProduts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificação única do produto."),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Nome do produto."),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Descrição do produto."),
                    Price = table.Column<decimal>(type: "numeric", nullable: false, comment: "Preço do produto.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkProduct", x => x.Id);
                },
                comment: "Informações de controle do produto.");

            migrationBuilder.CreateTable(
                name: "TbUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificação única do usuario."),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Nome do usuario."),
                    Email = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "E-mail do usuario."),
                    Password = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Senha do usuario."),
                    DtHrCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora da criação do registro.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkUser", x => x.Id);
                },
                comment: "Informações de controle do usuario.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbProduct");

            migrationBuilder.DropTable(
                name: "TbUser");
        }
    }
}
