using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesInitOnDataTablesDb08 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "funds_received",
                table: "TbTnBFoxbit_Trades");

            migrationBuilder.DropColumn(
                name: "instant_amount",
                table: "TbTnBFoxbit_Trades");

            migrationBuilder.DropColumn(
                name: "instant_amount_executed",
                table: "TbTnBFoxbit_Trades");

            migrationBuilder.DropColumn(
                name: "price_avg",
                table: "TbTnBFoxbit_Trades");

            migrationBuilder.DropColumn(
                name: "remark",
                table: "TbTnBFoxbit_Trades");

            migrationBuilder.DropColumn(
                name: "state",
                table: "TbTnBFoxbit_Trades");

            migrationBuilder.DropColumn(
                name: "trades_count",
                table: "TbTnBFoxbit_Trades");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "TbTnBFoxbit_Trades",
                newName: "fee_currency_symbol");

            migrationBuilder.RenameColumn(
                name: "quantity_executed",
                table: "TbTnBFoxbit_Trades",
                newName: "fee");

            migrationBuilder.RenameColumn(
                name: "client_order_id",
                table: "TbTnBFoxbit_Trades",
                newName: "order_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "TbTnBFoxbit_Trades",
                type: "numeric(15,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "TbTnBFoxbit_Trades",
                newName: "client_order_id");

            migrationBuilder.RenameColumn(
                name: "fee_currency_symbol",
                table: "TbTnBFoxbit_Trades",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "fee",
                table: "TbTnBFoxbit_Trades",
                newName: "quantity_executed");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "TbTnBFoxbit_Trades",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(15,0)");

            migrationBuilder.AddColumn<decimal>(
                name: "funds_received",
                table: "TbTnBFoxbit_Trades",
                type: "decimal(21,8)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "instant_amount",
                table: "TbTnBFoxbit_Trades",
                type: "decimal(21,8)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "instant_amount_executed",
                table: "TbTnBFoxbit_Trades",
                type: "decimal(21,8)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "price_avg",
                table: "TbTnBFoxbit_Trades",
                type: "decimal(21,8)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "remark",
                table: "TbTnBFoxbit_Trades",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "TbTnBFoxbit_Trades",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "trades_count",
                table: "TbTnBFoxbit_Trades",
                type: "numeric(9,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
