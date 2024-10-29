using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekWinkel.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignKeyToProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VerlanglijstModel_ProductId",
                table: "VerlanglijstModel",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_VerlanglijstModel_BoekModel_ProductId",
                table: "VerlanglijstModel",
                column: "ProductId",
                principalTable: "BoekModel",
                principalColumn: "BoekId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerlanglijstModel_BoekModel_ProductId",
                table: "VerlanglijstModel");

            migrationBuilder.DropIndex(
                name: "IX_VerlanglijstModel_ProductId",
                table: "VerlanglijstModel");
        }
    }
}
