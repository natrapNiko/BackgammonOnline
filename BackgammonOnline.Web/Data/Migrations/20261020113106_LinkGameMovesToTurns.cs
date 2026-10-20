using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgammonOnline.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class LinkGameMovesToTurns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameTurnId",
                table: "GameMoves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameMoves_GameTurnId",
                table: "GameMoves",
                column: "GameTurnId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameMoves_GameTurns_GameTurnId",
                table: "GameMoves",
                column: "GameTurnId",
                principalTable: "GameTurns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameMoves_GameTurns_GameTurnId",
                table: "GameMoves");

            migrationBuilder.DropIndex(
                name: "IX_GameMoves_GameTurnId",
                table: "GameMoves");

            migrationBuilder.DropColumn(
                name: "GameTurnId",
                table: "GameMoves");
        }
    }
}
