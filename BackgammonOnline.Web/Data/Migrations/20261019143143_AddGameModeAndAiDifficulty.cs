using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgammonOnline.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGameModeAndAiDifficulty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Games");
        }
    }
}
