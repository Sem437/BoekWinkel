using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekWinkel.Data.Migrations
{
    /// <inheritdoc />
    public partial class winkelwagenUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "aantalItems",
                table: "Winkelwagen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Winkelwagen_BoekId",
                table: "Winkelwagen",
                column: "BoekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Winkelwagen_BoekModel_BoekId",
                table: "Winkelwagen",
                column: "BoekId",
                principalTable: "BoekModel",
                principalColumn: "BoekId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Winkelwagen_BoekModel_BoekId",
                table: "Winkelwagen");

            migrationBuilder.DropIndex(
                name: "IX_Winkelwagen_BoekId",
                table: "Winkelwagen");

            migrationBuilder.DropColumn(
                name: "aantalItems",
                table: "Winkelwagen");
        }
    }
}
