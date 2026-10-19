using BackgammonOnline.Web.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackgammonOnline.Web.Models.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public int? GameRoomId { get; set; }
        public GameVariant Variant { get; set; }
        public GameStatus Status { get; set; }
        [Required]
        public string PlayerOneId { get; set; } = string.Empty;
        public string? PlayerTwoId { get; set; }
        public string? WinnerId { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? StartedAtUtc { get; set; }
        public DateTime? FinishedAtUtc { get; set; }
        public GameRoom? GameRoom { get; set; }
        public IdentityUser PlayerOne { get; set; } = null!;
        public IdentityUser? PlayerTwo { get; set; }
        public IdentityUser? Winner { get; set; }

    }
}
