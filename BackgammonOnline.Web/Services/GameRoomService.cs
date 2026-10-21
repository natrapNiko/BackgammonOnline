using BackgammonOnline.Web.Data;
using BackgammonOnline.Web.Models.Entities;
using BackgammonOnline.Web.Services.Interfaces;
using BackgammonOnline.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using BackgammonOnline.Web.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace BackgammonOnline.Web.Services
{
    public class GameRoomService : IGameRoomService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<GameRoom> _passwordHasher;

        public GameRoomService(ApplicationDbContext context, 
            IPasswordHasher<GameRoom> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<GameRoom> CreateRoomAsync(
            CreateGameRoomViewModel model,
            string ownerId)
        {
            if (model == null)
            { 
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(ownerId))
            {
                throw new ArgumentException(
                    "A valid room owner is required.",
                nameof(ownerId));           
            }

            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(
                model,
                new ValidationContext(model),
                validationResults,
                validateAllProperties: true);

            if (!isValid)
            {
                throw new ValidationException(
                    validationResults.First().ErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ValidationException(
                    "Room name cannot be empty or whitespace.");
            }

            if (!Enum.IsDefined(model.Variant))
            {
                throw new ValidationException(
                    "Invalid game variant.");
            }

            var room = new GameRoom
            {
                Name = model.Name.Trim(),
                OwnerId = ownerId,
                Variant = model.Variant,
                Status = RoomStatus.Waiting,
                IsPrivate = model.IsPrivate,
                CreatedAtUtc = DateTime.UtcNow
            };

            if (model.IsPrivate)
            {
                room.PasswordHash = _passwordHasher.HashPassword(
                    room,
                    model.Password!);
            }

            room.Members.Add(new GameRoomMember
            {
                UserId = ownerId,
                Role = RoomMemberRole.Player,
                PlayerPosition = 1,
                JoinedAtUtc = DateTime.UtcNow
            });

            _context.GameRooms.Add(room);

            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<RoomDetailsViewModel?> GetPublicRoomDetailsAsync(int roomId)
        {
            return await _context.GameRooms
                .AsNoTracking()
                .Where(r => r.Id == roomId && !r.IsPrivate)
                .Select(r => new RoomDetailsViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Variant = r.Variant,
                    Status = r.Status,
                    CreateAtUtc = r.CreatedAtUtc,

                    PlayerCount = r.Members.Count(
                        m => m.Role == RoomMemberRole.Player),

                    SpectatorCount = r.Members.Count(
                        m => m.Role == RoomMemberRole.Spectator)
                })
                .FirstOrDefaultAsync();
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

        public async Task<bool> JoinPublicRoomAsPlayerAsync(int roomId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) 
            {
                return false;
            }

            var room = await _context.GameRooms
                .Include(r => r.Members)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if(room == null || room.IsPrivate || room.Status != RoomStatus.Waiting)
            {
                return false;
            }

            bool isAlreadyMember = room.Members
                .Any(m => m.UserId == userId);

            if (isAlreadyMember) 
            {
                return false;
            }

            bool isBanned = await _context.GameRoomBans
                .AnyAsync(b => b.GameRoomId == roomId && b.BannedUserId == userId);

            if (isBanned)
            {
                return false;
            }

            int playerCount = room.Members
                .Count(m => m.Role == RoomMemberRole.Player);

            if (playerCount >= 2)
            {
                return false;
            }

            bool secondPositionTaken = room.Members
                .Any(m => m.PlayerPosition == 2);

            if (secondPositionTaken)
            {
                return false;
            }

            _context.GameRoomMembers.Add(new GameRoomMember
            {
                GameRoomId = roomId,
                UserId = userId,
                Role = RoomMemberRole.Player,
                PlayerPosition = 2,
                JoinedAtUtc = DateTime.UtcNow
            });

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                // A competing request may have filled the last player slot.
                // Other database failures will be handled more precisely later.
                return false;
            }
        }

        public async Task<bool> RoomExistsAsync(int roomId)
        {
            return await _context.GameRooms
                .AnyAsync(r => r.Id == roomId);
        }
    }
}
