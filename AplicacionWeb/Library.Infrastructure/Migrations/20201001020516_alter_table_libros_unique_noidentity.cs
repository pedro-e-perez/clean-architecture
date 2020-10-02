using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class alter_table_libros_unique_noidentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_autores_has_libros_libros_LibrosId",
                table: "autores_has_libros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_libros",
                table: "libros");

            migrationBuilder.DropIndex(
                name: "IX_autores_has_libros_LibrosId",
                table: "autores_has_libros");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "libros");

            migrationBuilder.DropColumn(
                name: "LibrosId",
                table: "autores_has_libros");

            migrationBuilder.AddColumn<int>(
                name: "ISBN",
                table: "libros",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_libros",
                table: "libros",
                column: "ISBN");

            migrationBuilder.CreateIndex(
                name: "IX_autores_has_libros_LibrosISBN",
                table: "autores_has_libros",
                column: "LibrosISBN");

            migrationBuilder.AddForeignKey(
                name: "FK_autores_has_libros_libros_LibrosISBN",
                table: "autores_has_libros",
                column: "LibrosISBN",
                principalTable: "libros",
                principalColumn: "ISBN",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_autores_has_libros_libros_LibrosISBN",
                table: "autores_has_libros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_libros",
                table: "libros");

            migrationBuilder.DropIndex(
                name: "IX_autores_has_libros_LibrosISBN",
                table: "autores_has_libros");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "libros");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "libros",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "LibrosId",
                table: "autores_has_libros",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_libros",
                table: "libros",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_autores_has_libros_LibrosId",
                table: "autores_has_libros",
                column: "LibrosId");

            migrationBuilder.AddForeignKey(
                name: "FK_autores_has_libros_libros_LibrosId",
                table: "autores_has_libros",
                column: "LibrosId",
                principalTable: "libros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
