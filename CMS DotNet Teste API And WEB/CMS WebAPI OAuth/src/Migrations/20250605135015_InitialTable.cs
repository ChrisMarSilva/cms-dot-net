using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CMS_WebAPI_OAuth.Migrations
{
    /// <inheritdoc />
    public partial class InitialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbAuth_Funcao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAuth_Funcao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbAuth_Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<decimal>(type: "numeric(1,0)", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<decimal>(type: "numeric(1,0)", nullable: false),
                    TwoFactorEnabled = table.Column<decimal>(type: "numeric(1,0)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<decimal>(type: "numeric(1,0)", nullable: false),
                    AccessFailedCount = table.Column<decimal>(type: "numeric(10,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAuth_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbAuth_FuncaoReiv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAuth_FuncaoReiv", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbAuth_FuncaoReiv_TbAuth_Funcao_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TbAuth_Funcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbAuth_UsuarioFuncao",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAuth_UsuarioFuncao", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TbAuth_UsuarioFuncao_TbAuth_Funcao_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TbAuth_Funcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbAuth_UsuarioFuncao_TbAuth_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "TbAuth_Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbAuth_UsuarioLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAuth_UsuarioLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_TbAuth_UsuarioLogin_TbAuth_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "TbAuth_Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbAuth_UsuarioReiv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAuth_UsuarioReiv", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbAuth_UsuarioReiv_TbAuth_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "TbAuth_Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbAuth_UsuarioToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAuth_UsuarioToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_TbAuth_UsuarioToken_TbAuth_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "TbAuth_Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "TbAuth_Funcao",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbAuth_FuncaoReiv_RoleId",
                table: "TbAuth_FuncaoReiv",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "TbAuth_Usuario",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "TbAuth_Usuario",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbAuth_UsuarioFuncao_RoleId",
                table: "TbAuth_UsuarioFuncao",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TbAuth_UsuarioLogin_UserId",
                table: "TbAuth_UsuarioLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TbAuth_UsuarioReiv_UserId",
                table: "TbAuth_UsuarioReiv",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbAuth_FuncaoReiv");

            migrationBuilder.DropTable(
                name: "TbAuth_UsuarioFuncao");

            migrationBuilder.DropTable(
                name: "TbAuth_UsuarioLogin");

            migrationBuilder.DropTable(
                name: "TbAuth_UsuarioReiv");

            migrationBuilder.DropTable(
                name: "TbAuth_UsuarioToken");

            migrationBuilder.DropTable(
                name: "TbAuth_Funcao");

            migrationBuilder.DropTable(
                name: "TbAuth_Usuario");
        }
    }
}
