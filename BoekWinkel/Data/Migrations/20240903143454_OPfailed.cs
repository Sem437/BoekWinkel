using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoekWinkel.Data.Migrations
{
    /// <inheritdoc />
    public partial class OPfailed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoekModel",
                columns: table => new
                {
                    BoekId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoekTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoekAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoekDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoekPrice = table.Column<int>(type: "int", nullable: false),
                    BoekCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoekImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoekImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoekModel", x => x.BoekId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoekModel");
        }
    }
}
