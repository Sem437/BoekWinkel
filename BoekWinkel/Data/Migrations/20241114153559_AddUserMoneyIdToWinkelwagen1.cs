using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekWinkel.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserMoneyIdToWinkelwagen1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Winkelwagen_UserMoneyModel_UserMoneyId",
                table: "Winkelwagen");

            migrationBuilder.AlterColumn<int>(
                name: "UserMoneyId",
                table: "Winkelwagen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Winkelwagen_UserMoneyModel_UserMoneyId",
                table: "Winkelwagen",
                column: "UserMoneyId",
                principalTable: "UserMoneyModel",
                principalColumn: "UserMoneyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Winkelwagen_UserMoneyModel_UserMoneyId",
                table: "Winkelwagen");

            migrationBuilder.AlterColumn<int>(
                name: "UserMoneyId",
                table: "Winkelwagen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Winkelwagen_UserMoneyModel_UserMoneyId",
                table: "Winkelwagen",
                column: "UserMoneyId",
                principalTable: "UserMoneyModel",
                principalColumn: "UserMoneyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
