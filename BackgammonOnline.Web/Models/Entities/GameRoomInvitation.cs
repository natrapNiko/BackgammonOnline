using System.ComponentModel.DataAnnotations;
using BackgammonOnline.Web.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace BackgammonOnline.Web.Models.Entities
{
    public class GameRoomInvitation
    {
        [Key]
        public int Id { get; set; }

        public int GameRoomId { get; set; }

        [Required]
        public string SenderId { get; set; } = string.Empty;

        [Required]
        public string RecipientId { get; set; } = string.Empty;

        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime ExpiresAtUtc { get; set; } = DateTime.UtcNow.AddHours(24);

        public DateTime? RespondedAtUtc { get; set; }

        public GameRoom GameRoom { get; set; } = null!;

        public IdentityUser Sender { get; set; } = null!;

        public IdentityUser Recipient { get; set; } = null!;
    }
}