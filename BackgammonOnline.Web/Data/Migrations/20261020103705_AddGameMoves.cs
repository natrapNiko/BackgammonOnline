using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgammonOnline.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGameMoves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameMoves",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    TurnNumber = table.Column<int>(type: "int", nullable: false),
                    MoveNumber = table.Column<int>(type: "int", nullable: false),
                    FromPoint = table.Column<int>(type: "int", nullable: false),
                    ToPoint = table.Column<int>(type: "int", nullable: false),
                    DieValue = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMoves", x => x.id);
                    table.ForeignKey(
                        name: "FK_GameMoves_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameMoves_GameId_TurnNumber_MoveNumber",
                table: "GameMoves",
                columns: new[] { "GameId", "TurnNumber", "MoveNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameMoves");
        }
    }
}
