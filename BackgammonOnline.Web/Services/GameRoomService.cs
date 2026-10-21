using BackgammonOnline.Web.Data;
using BackgammonOnline.Web.Models.Entities;
using BackgammonOnline.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackgammonOnline.Web.Services
{
    public class GameRoomService : IGameRoomService
    {
        private readonly ApplicationDbContext _context;
        public GameRoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GameRoom>> GetPublicRoomsAsync()
        {
            return await _context.GameRooms
                .AsNoTracking()
                .Where(r => !r.IsPrivate)
                .OrderByDescending(r => r.CreatedAtUtc)
                .ToListAsync();
        }

        public async Task<GameRoom?> GetRoomByIdAsync(int roomId)
        {
            return await _context.GameRooms
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task<bool> RoomExistsAsync(int roomId)
        {
            return await _context.GameRooms
                .AnyAsync(r => r.Id == roomId);
        }
    }
}
