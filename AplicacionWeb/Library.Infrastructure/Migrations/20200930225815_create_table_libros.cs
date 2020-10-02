using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class create_table_libros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "libros",
                columns: table => new
                {
                    ISBN = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EditorialesId = table.Column<int>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 45, nullable: false),
                    Sinopsis = table.Column<string>(type: "text", nullable: false),
                    n_paginas = table.Column<string>(maxLength: 45, nullable: false),
                    LogicalErasure = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    EraseDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_libros", x => x.ISBN);
                    table.ForeignKey(
                        name: "FK_libros_editoriales_EditorialesId",
                        column: x => x.EditorialesId,
                        principalTable: "editoriales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "autores_has_libros",
                columns: table => new
                {
                    AutoresId = table.Column<int>(nullable: false),
                    LibrosISBN = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    LogicalErasure = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    EraseDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autores_has_libros", x => new { x.AutoresId, x.LibrosISBN });
                    table.ForeignKey(
                        name: "FK_autores_has_libros_autores_AutoresId",
                        column: x => x.AutoresId,
                        principalTable: "autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_autores_has_libros_libros_LibrosISBN",
                        column: x => x.LibrosISBN,
                        principalTable: "libros",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_autores_has_libros_LibrosISBN",
                table: "autores_has_libros",
                column: "LibrosISBN");

            migrationBuilder.CreateIndex(
                name: "IX_libros_EditorialesId",
                table: "libros",
                column: "EditorialesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "autores_has_libros");

            migrationBuilder.DropTable(
                name: "libros");
        }
    }
}
