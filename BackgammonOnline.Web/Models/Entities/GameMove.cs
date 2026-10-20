using System.ComponentModel.DataAnnotations;

namespace BackgammonOnline.Web.Models.Entities
{
    public class GameMove
    {
        [Key]
        public int id { get; set; }
        public int GameId { get; set; }
        public int TurnNumber { get; set; }
        public int MoveNumber { get; set; }
        public int FromPoint { get; set; }
        public int ToPoint { get; set; }
        public int DieValue { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public Game Game { get; set; } = null!;
    }
}
