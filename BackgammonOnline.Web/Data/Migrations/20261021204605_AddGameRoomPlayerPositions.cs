using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgammonOnline.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGameRoomPlayerPositions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerPosition",
                table: "GameRoomMembers",
                type: "int",
                nullable: true);

            migrationBuilder.Sql("""
                UPDATE m
                SET PlayerPosition = 1
                FROM GameRoomMembers AS m
                INNER JOIN GameRooms AS r
                    ON r.Id = m.GameRoomId
                WHERE m.Role = 1
                  AND m.UserId = r.OwnerId;
                """);

            migrationBuilder.CreateIndex(
                name: "IX_GameRoomMembers_GameRoomId_PlayerPosition",
                table: "GameRoomMembers",
                columns: new[] { "GameRoomId", "PlayerPosition" },
                unique: true,
                filter: "[PlayerPosition] IS NOT NULL");

            migrationBuilder.AddCheckConstraint(
                name: "CK_GameRoomMembers_PlayerPosition",
                table: "GameRoomMembers",
                sql: "[PlayerPosition] IS NULL OR [PlayerPosition] IN (1, 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameRoomMembers_GameRoomId_PlayerPosition",
                table: "GameRoomMembers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_GameRoomMembers_PlayerPosition",
                table: "GameRoomMembers");

            migrationBuilder.DropColumn(
                name: "PlayerPosition",
                table: "GameRoomMembers");
        }
    }
}
