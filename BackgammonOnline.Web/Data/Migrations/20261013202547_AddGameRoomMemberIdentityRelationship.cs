using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgammonOnline.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGameRoomMemberIdentityRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GameRoomMembers_UserId",
                table: "GameRoomMembers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRoomMembers_AspNetUsers_UserId",
                table: "GameRoomMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRoomMembers_AspNetUsers_UserId",
                table: "GameRoomMembers");

            migrationBuilder.DropIndex(
                name: "IX_GameRoomMembers_UserId",
                table: "GameRoomMembers");
        }
    }
}
