using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackgammonOnline.Web.Models.Entities
{
    public class ChatMessage
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int GameRoomId { get; set; }
        [Required]
        public string SenderId { get; set; } = string.Empty;
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Content { get; set; } = string.Empty;
        public DateTime SendAtUtc { get; set; } = DateTime.UtcNow;
        public GameRoom GameRoom { get; set; } = null!;
        public IdentityUser Sender { get; set; } = null!;
    }
}
