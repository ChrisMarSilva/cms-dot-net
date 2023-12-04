using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesInitOnDataTablesDb03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbTnBFoxbit_Currencies",
                columns: table => new
                {
                    symbol = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precision = table.Column<decimal>(type: "numeric(3,0)", nullable: true),
                    category_code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deposit_min_to_confirm = table.Column<decimal>(type: "numeric(3,0)", nullable: false),
                    deposit_min_amount = table.Column<decimal>(type: "decimal(21,8)", nullable: false),
                    withdraw_enabled = table.Column<decimal>(type: "numeric(1,0)", nullable: true),
                    withdraw_min_amount = table.Column<decimal>(type: "decimal(21,8)", nullable: false),
                    withdraw_fee = table.Column<decimal>(type: "decimal(21,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkTnBFoxbit_Currencies", x => x.symbol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbTnBFoxbit_Currencies");
        }
    }
}
