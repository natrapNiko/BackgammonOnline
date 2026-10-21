using BackgammonOnline.Web.Models.Entities;

namespace BackgammonOnline.Web.Services.Interfaces
{
        public interface IGameRoomService
        {
            Task<GameRoom?> GetRoomByIdAsync(int roomId);
            Task<List<GameRoom>> GetPublicRoomsAsync();
            Task<bool> RoomExistsAsync(int roomId);
        }
}
