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
        public bool IsPrivate { get; set; } = false;
        public string? PasswordHash { get; set; }
        public GameVariant Variant { get; set; } = GameVariant.Portes;
        public RoomStatus Status { get; set; } = RoomStatus.Waiting;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public ICollection<GameRoomMember> Members { get; set; } = new List<GameRoomMember>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public ICollection<GameRoomInvitation> Invitations { get; set; } = new List<GameRoomInvitation>();
        public ICollection<GameRoomBan> Bans { get; set; } = new List<GameRoomBan>();
    }
}
