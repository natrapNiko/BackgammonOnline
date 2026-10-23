using BackgammonOnline.Web.Models.Enums;

namespace BackgammonOnline.Web.ViewModels
{
    public class RoomDetailsViewModel
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public GameVariant Variant { get; set; }
        public RoomStatus Status { get; set; }
        public DateTime CreateAtUtc { get; set; }
        public int PlayerCount { get; set; }
        public int SpectatorCount { get; set; }
        public bool IsCurrentUserSpectator { get; set; }
    }
}
