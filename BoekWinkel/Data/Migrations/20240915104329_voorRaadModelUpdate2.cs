using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekWinkel.Data.Migrations
{
    /// <inheritdoc />
    public partial class voorRaadModelUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VoorRaadBoeken",
                columns: table => new
                {
                    voorraadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    boekId = table.Column<int>(type: "int", nullable: false),
                    voorRaad = table.Column<int>(type: "int", nullable: false),
                    verkocht = table.Column<int>(type: "int", nullable: false),
                    geretourd = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoorRaadBoeken", x => x.voorraadId);
                    table.ForeignKey(
                        name: "FK_VoorRaadBoeken_BoekModel_boekId",
                        column: x => x.boekId,
                        principalTable: "BoekModel",
                        principalColumn: "BoekId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoorRaadBoeken_boekId",
                table: "VoorRaadBoeken",
                column: "boekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoorRaadBoeken");
        }
    }
}
