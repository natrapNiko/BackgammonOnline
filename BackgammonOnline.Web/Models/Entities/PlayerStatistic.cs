using BackgammonOnline.Web.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackgammonOnline.Web.Models.Entities
{
    public class PlayerStatistic
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        public GameVariant Variant { get; set; }
        public int GamesPlayed { get; set; } = 0;
        public int GamesWon { get; set; } = 0;
        public int GamesLost { get; set; } = 0;
        public int GamesAbandoned { get; set; } = 0;
        public DateTime LastUpdatedAtUtc { get; set; } = DateTime.UtcNow;
        public IdentityUser User { get; set; } = null!;
    }
}
