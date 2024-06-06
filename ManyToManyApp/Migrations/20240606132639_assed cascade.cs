using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManyToManyApp.Migrations
{
    /// <inheritdoc />
    public partial class assedcascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boeken_Auteurs_AuteurId",
                table: "Boeken");

            migrationBuilder.DropForeignKey(
                name: "FK_BoekGenres_Boeken_BoekId",
                table: "BoekGenres");

            migrationBuilder.AddForeignKey(
                name: "FK_Boeken_Auteurs_AuteurId",
                table: "Boeken",
                column: "AuteurId",
                principalTable: "Auteurs",
                principalColumn: "AuteurId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoekGenres_Boeken_BoekId",
                table: "BoekGenres",
                column: "BoekId",
                principalTable: "Boeken",
                principalColumn: "BoekId",
                onDelete: ReferentialAction.Cascade);
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
        }
    }
}
