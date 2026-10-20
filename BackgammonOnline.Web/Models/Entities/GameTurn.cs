using System.ComponentModel.DataAnnotations;

namespace BackgammonOnline.Web.Models.Entities
{
    public class GameTurn
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int TurnNumber { get; set; }
        public string? PlayerId { get; set; }
        public int DieOne {  get; set; }
        public int DieTwo { get; set; }
        public DateTime StartedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAtUtc { get; set; }
        public Game Game { get; set; } = null!;
    }
}
