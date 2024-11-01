using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekWinkel.Data.Migrations
{
    /// <inheritdoc />
    public partial class userMoneyModelUpdate10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Achternaam",
                table: "UserMoneyModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Land",
                table: "UserMoneyModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "UserMoneyModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Regio_Provincie",
                table: "UserMoneyModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stad",
                table: "UserMoneyModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Straatnaam",
                table: "UserMoneyModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TussenVoegsel",
                table: "UserMoneyModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Voornaam",
                table: "UserMoneyModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Achternaam",
                table: "UserMoneyModel");

            migrationBuilder.DropColumn(
                name: "Land",
                table: "UserMoneyModel");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "UserMoneyModel");

            migrationBuilder.DropColumn(
                name: "Regio_Provincie",
                table: "UserMoneyModel");

            migrationBuilder.DropColumn(
                name: "Stad",
                table: "UserMoneyModel");

            migrationBuilder.DropColumn(
                name: "Straatnaam",
                table: "UserMoneyModel");

            migrationBuilder.DropColumn(
                name: "TussenVoegsel",
                table: "UserMoneyModel");

            migrationBuilder.DropColumn(
                name: "Voornaam",
                table: "UserMoneyModel");
        }
    }
}
