using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgammonOnline.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGameTurns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameTurns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    TurnNumber = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DieOne = table.Column<int>(type: "int", nullable: false),
                    DieTwo = table.Column<int>(type: "int", nullable: false),
                    StartedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTurns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameTurns_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameTurns_GameId_TurnNumber",
                table: "GameTurns",
                columns: new[] { "GameId", "TurnNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameTurns");
        }
    }
}
