using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesInitOnDataTablesDb02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "TbTnBFoxbit_Trades",
                type: "datetime(3)",
                precision: 3,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldPrecision: 6);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "TbTnBFoxbit_Trades",
                type: "datetime(6)",
                precision: 6,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(3)",
                oldPrecision: 3);
        }
    }
}
