using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekWinkel.Data.Migrations
{
    /// <inheritdoc />
    public partial class verlanglijstModeL3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoekId",
                table: "VerlanglijstModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VerlanglijstModel_BoekId",
                table: "VerlanglijstModel",
                column: "BoekId");

            migrationBuilder.AddForeignKey(
                name: "Boek_Id",
                table: "VerlanglijstModel",
                column: "BoekId",
                principalTable: "BoekModel",
                principalColumn: "BoekId",
                onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerlanglijstModel_BoekModel_BoekId",
                table: "VerlanglijstModel");

            migrationBuilder.DropIndex(
                name: "IX_VerlanglijstModel_BoekId",
                table: "VerlanglijstModel");

            migrationBuilder.DropColumn(
                name: "BoekId",
                table: "VerlanglijstModel");
        }
    }
}
