using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackgammonOnline.Web.Models.Entities
{
    public class GameRoomBan
    {
        [Key]
        public int id { get; set; }
        public int GameRoomId { get; set; }
        [Required]
        public string BannedUserId { get; set; } = string.Empty;
        [Required]
        public string BannedByUserId { get; set; } = string.Empty;
        [StringLength(250)]
        public string? Reason { get; set; }
        public DateTime BannedAtUtc { get; set; } = DateTime.UtcNow;
        public GameRoom GameRoom { get; set; } = null!;
        public IdentityUser BannedUser { get; set; } = null!;
        public IdentityUser BannedByUser { get; set; } = null!;
    }
}
