using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekWinkel.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserMoneyModelRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserMoneyId",
                table: "Winkelwagen",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Winkelwagen_UserMoneyId",
                table: "Winkelwagen",
                column: "UserMoneyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Winkelwagen_UserMoneyModel_UserMoneyId",
                table: "Winkelwagen",
                column: "UserMoneyId",
                principalTable: "UserMoneyModel",
                principalColumn: "UserMoneyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Winkelwagen_UserMoneyModel_UserMoneyId",
                table: "Winkelwagen");

            migrationBuilder.DropIndex(
                name: "IX_Winkelwagen_UserMoneyId",
                table: "Winkelwagen");

            migrationBuilder.DropColumn(
                name: "UserMoneyId",
                table: "Winkelwagen");
        }
    }
}
