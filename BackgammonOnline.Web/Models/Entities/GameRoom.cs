using BackgammonOnline.Web.Models.Enums;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BackgammonOnline.Web.Models.Entities
{
    public class GameRoom
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string OwnerId { get; set; } = string.Empty;
        public GameVariant Variant { get; set; } = GameVariant.Portes;
        public RoomStatus Status { get; set; } = RoomStatus.Waiting;

        public bool IsPrivate { get; set; }

        public string? PasswordHash { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public ICollection<GameRoomMember> Members { get; set; } = new List<GameRoomMember>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public ICollection<GameRoomInvitation> Invitations { get; set; } = new List<GameRoomInvitation>();
    }
}
