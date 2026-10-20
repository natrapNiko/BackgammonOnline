using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgammonOnline.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGameRoomBans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameRoomBans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameRoomId = table.Column<int>(type: "int", nullable: false),
                    BannedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BannedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BannedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BannedByUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoomBans", x => x.id);
                    table.ForeignKey(
                        name: "FK_GameRoomBans_AspNetUsers_BannedByUserId",
                        column: x => x.BannedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameRoomBans_AspNetUsers_BannedByUserId1",
                        column: x => x.BannedByUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameRoomBans_GameRooms_GameRoomId",
                        column: x => x.GameRoomId,
                        principalTable: "GameRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameRoomBans_BannedByUserId",
                table: "GameRoomBans",
                column: "BannedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRoomBans_BannedByUserId1",
                table: "GameRoomBans",
                column: "BannedByUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_GameRoomBans_GameRoomId_BannedUserId",
                table: "GameRoomBans",
                columns: new[] { "GameRoomId", "BannedUserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameRoomBans");
        }
    }
}
