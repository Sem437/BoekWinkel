using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekWinkel.Data.Migrations
{
    /// <inheritdoc />
    public partial class verlanglijstModeL2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VerlanglijstModel",
                columns: table => new
                {
                    VerlanglijstId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GebruikersId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OpVerlanglijst = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerlanglijstModel", x => x.VerlanglijstId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerlanglijstModel");
        }
    }
}
