using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekWinkel.Data.Migrations
{
    /// <inheritdoc />
    public partial class Lijst2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        
            migrationBuilder.DropIndex(
                name: "IX_VerlanglijstModel_BoekId",
                table: "VerlanglijstModel");

            migrationBuilder.DropColumn(
                name: "BoekId",
                table: "VerlanglijstModel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_VerlanglijstModel_BoekModel_BoekId",
                table: "VerlanglijstModel",
                column: "BoekId",
                principalTable: "BoekModel",
                principalColumn: "BoekId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
