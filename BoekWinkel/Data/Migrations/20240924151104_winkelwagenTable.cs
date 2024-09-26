using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekWinkel.Data.Migrations
{
    /// <inheritdoc />
    public partial class winkelwagenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Winkelwagen",
                columns: table => new
                {
                    WinkelwagenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gebruikersId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoekId = table.Column<int>(type: "int", nullable: false),
                    InWinkelwagen = table.Column<bool>(type: "bit", nullable: false),
                    Betaald = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Winkelwagen", x => x.WinkelwagenId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Winkelwagen");
        }
    }
}
