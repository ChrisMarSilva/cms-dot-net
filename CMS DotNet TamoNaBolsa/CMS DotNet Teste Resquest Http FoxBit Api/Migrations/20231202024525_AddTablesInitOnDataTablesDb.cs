using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesInitOnDataTablesDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TbTnBFoxbit_Trades",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    sn = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    client_order_id = table.Column<decimal>(type: "numeric(15,0)", nullable: false),
                    market_symbol = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    side = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    state = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(21,8)", nullable: false),
                    price_avg = table.Column<decimal>(type: "decimal(21,8)", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(21,8)", nullable: false),
                    quantity_executed = table.Column<decimal>(type: "decimal(21,8)", nullable: false),
                    instant_amount = table.Column<decimal>(type: "decimal(21,8)", nullable: false),
                    instant_amount_executed = table.Column<decimal>(type: "decimal(21,8)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", precision: 6, nullable: false),
                    trades_count = table.Column<decimal>(type: "numeric(9,0)", nullable: false),
                    remark = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    funds_received = table.Column<decimal>(type: "decimal(21,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkTnBFoxbit_Trades", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbTnBFoxbit_Trades");
        }
    }
}
