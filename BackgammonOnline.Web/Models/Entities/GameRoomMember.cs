using BackgammonOnline.Web.Models.Enums;
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
        public DateTime JoinedAtUtc { get; set; } = DateTime.UtcNow;
        public GameRoom GameRoom { get; set; } = null!;
    }
}