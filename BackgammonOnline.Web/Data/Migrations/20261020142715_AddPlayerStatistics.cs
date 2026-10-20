using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgammonOnline.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRoomBans_AspNetUsers_BannedByUserId1",
                table: "GameRoomBans");

            migrationBuilder.DropIndex(
                name: "IX_GameRoomBans_BannedByUserId1",
                table: "GameRoomBans");

            migrationBuilder.DropColumn(
                name: "BannedByUserId1",
                table: "GameRoomBans");

            migrationBuilder.CreateTable(
                name: "PlayerStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Variant = table.Column<int>(type: "int", nullable: false),
                    GamesPlayed = table.Column<int>(type: "int", nullable: false),
                    GamesWon = table.Column<int>(type: "int", nullable: false),
                    GamesLost = table.Column<int>(type: "int", nullable: false),
                    GamesAbandoned = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameRoomBans_BannedUserId",
                table: "GameRoomBans",
                column: "BannedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_UserId_Variant",
                table: "PlayerStatistics",
                columns: new[] { "UserId", "Variant" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRoomBans_AspNetUsers_BannedUserId",
                table: "GameRoomBans",
                column: "BannedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRoomBans_AspNetUsers_BannedUserId",
                table: "GameRoomBans");

            migrationBuilder.DropTable(
                name: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_GameRoomBans_BannedUserId",
                table: "GameRoomBans");

            migrationBuilder.AddColumn<string>(
                name: "BannedByUserId1",
                table: "GameRoomBans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GameRoomBans_BannedByUserId1",
                table: "GameRoomBans",
                column: "BannedByUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRoomBans_AspNetUsers_BannedByUserId1",
                table: "GameRoomBans",
                column: "BannedByUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
