using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesInitOnDataTablesDb06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbTnBFoxbit_SystemTime",
                columns: table => new
                {
                    type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    iso = table.Column<DateTime>(type: "datetime(6)", maxLength: 20, nullable: false),
                    timestamp = table.Column<long>(type: "bigint", precision: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkTnBFoxbit_SystemTime", x => new { x.type, x.iso });
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbTnBFoxbit_SystemTime");
        }
    }
}
