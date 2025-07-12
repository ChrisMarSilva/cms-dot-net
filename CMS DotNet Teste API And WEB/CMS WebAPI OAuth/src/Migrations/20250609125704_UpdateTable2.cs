using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_WebAPI_OAuth.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: "DtHrExpireRefreshToken",
                table: "TbAuth_Usuario",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "TbAuth_Usuario",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtHrExpireRefreshToken",
                table: "TbAuth_Usuario");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "TbAuth_Usuario");
        }
    }
}
