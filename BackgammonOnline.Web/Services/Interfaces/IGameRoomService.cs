using BackgammonOnline.Web.Models.Entities;
using BackgammonOnline.Web.ViewModels;

namespace BackgammonOnline.Web.Services.Interfaces
{
        public interface IGameRoomService
            {
                Task<GameRoom?> GetRoomByIdAsync(int roomId);
                Task<List<GameRoom>> GetPublicRoomsAsync();
                Task<bool> RoomExistsAsync(int roomId);
                Task<GameRoom> CreateRoomAsync(CreateGameRoomViewModel model, string ownerId);
                Task<RoomDetailsViewModel?> GetPublicRoomDetailsAsync(int roomId, string? currentUserId);
                Task<bool> JoinPublicRoomAsPlayerAsync(int roomId, string userId);
                Task<bool> JoinPublicRoomAsSpectatorAsync(int roomId, string userId);
                Task<bool> LeaveRoomAsSpectatorAsync(int roomId, string userId);
                Task<bool> LeaveRoomAsPlayerAsync(int roomId, string userId);
        }
}
 