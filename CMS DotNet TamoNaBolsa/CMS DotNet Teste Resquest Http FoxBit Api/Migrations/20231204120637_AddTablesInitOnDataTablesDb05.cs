using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesInitOnDataTablesDb05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbTnBFoxbit_MarketQuotes",
                columns: table => new
                {
                    market_symbol = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    side = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    base_amount = table.Column<decimal>(type: "decimal(21,8)", nullable: false),
                    quote_amount = table.Column<decimal>(type: "numeric(3,0)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(21,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkTnBFoxbit_MarketQuotes", x => x.market_symbol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbTnBFoxbit_MarketQuotes");
        }
    }
}
