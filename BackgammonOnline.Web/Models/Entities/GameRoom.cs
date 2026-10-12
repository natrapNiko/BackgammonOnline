namespace BackgammonOnline.Web.Models.Entities
{
    public class GameRoom
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;

        public bool IsPrivate { get; set; }

        public string? PasswordHash { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
