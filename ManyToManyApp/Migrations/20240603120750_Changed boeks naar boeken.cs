using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManyToManyApp.Migrations
{
    /// <inheritdoc />
    public partial class Changedboeksnaarboeken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoekGenres_Boeks_BoekId",
                table: "BoekGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_Boeks_Auteurs_AuteurId",
                table: "Boeks");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Boeks_BoekId",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boeks",
                table: "Boeks");

            migrationBuilder.RenameTable(
                name: "Boeks",
                newName: "Boeken");

            migrationBuilder.RenameIndex(
                name: "IX_Boeks_AuteurId",
                table: "Boeken",
                newName: "IX_Boeken_AuteurId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boeken",
                table: "Boeken",
                column: "BoekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boeken_Auteurs_AuteurId",
                table: "Boeken",
                column: "AuteurId",
                principalTable: "Auteurs",
                principalColumn: "AuteurId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoekGenres_Boeken_BoekId",
                table: "BoekGenres",
                column: "BoekId",
                principalTable: "Boeken",
                principalColumn: "BoekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Boeken_BoekId",
                table: "Genres",
                column: "BoekId",
                principalTable: "Boeken",
                principalColumn: "BoekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boeken_Auteurs_AuteurId",
                table: "Boeken");

            migrationBuilder.DropForeignKey(
                name: "FK_BoekGenres_Boeken_BoekId",
                table: "BoekGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Boeken_BoekId",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boeken",
                table: "Boeken");

            migrationBuilder.RenameTable(
                name: "Boeken",
                newName: "Boeks");

            migrationBuilder.RenameIndex(
                name: "IX_Boeken_AuteurId",
                table: "Boeks",
                newName: "IX_Boeks_AuteurId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boeks",
                table: "Boeks",
                column: "BoekId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoekGenres_Boeks_BoekId",
                table: "BoekGenres",
                column: "BoekId",
                principalTable: "Boeks",
                principalColumn: "BoekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boeks_Auteurs_AuteurId",
                table: "Boeks",
                column: "AuteurId",
                principalTable: "Auteurs",
                principalColumn: "AuteurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Boeks_BoekId",
                table: "Genres",
                column: "BoekId",
                principalTable: "Boeks",
                principalColumn: "BoekId");
        }
    }
}
