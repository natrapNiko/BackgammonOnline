using BackgammonOnline.Web.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackgammonOnline.Web.Models.Entities
{
    public class GameRoomMember
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int GameRoomId { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        public RoomMemberRole Role { get; set; }
        public int? PlayerPosition { get; set; }
        public DateTime JoinedAtUtc { get; set; } = DateTime.UtcNow;
        
        //Navigation property to the game room
        public GameRoom GameRoom { get; set; } = null!;
        //Navigaion property to the registered Identity User
        public IdentityUser User { get; set; } = null!;
    }
}