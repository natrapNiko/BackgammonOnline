using BackgammonOnline.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackgammonOnline.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackgammonOnline.Web.Controllers
{
    public class GameRoomController : Controller
    {
        private readonly IGameRoomService _gameRoomService;

        public GameRoomController(IGameRoomService gameRoomService)
        {
            _gameRoomService = gameRoomService;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _gameRoomService.GetPublicRoomsAsync();
            return View(rooms);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateGameRoomViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameRoomViewModel model) 
        { 
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            string? ownerId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(ownerId)) 
            {
                return Challenge();
            }

            try
            {
                await _gameRoomService.CreateRoomAsync(model, ownerId);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            TempData["SuccessMessage"] =
                "Form validated successfully. Room creation will be implemented next.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            string? userId = User.FindFirstValue
                (ClaimTypes.NameIdentifier);

            var room = await _gameRoomService
                .GetPublicRoomDetailsAsync(id, userId);

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JoinAsPlayer(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            bool joined = await _gameRoomService
                .JoinPublicRoomAsPlayerAsync(id, userId);

            if(!joined)
            {
                TempData["ErrorMessage"] = "Unable to join this room. It may be full or unavailable.";
            }
            else
            {
                TempData["SuccessMessage"] = "You joined the room as a player.";
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JoinAsSpectator(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            bool joined = await _gameRoomService.JoinPublicRoomAsSpectatorAsync(id, userId);

            if(joined)
            {
                TempData["SuccessMessage"] = "You joined the room as a spectator";
            }
            else 
            {
                TempData["ErrorMessage"] = "Unable to join as a spectator. The room may be full or unavailable.";
            }

            return RedirectToAction(nameof(Details), new {id});
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveAsSpectator(int id)
        {
            string? userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            bool left = await _gameRoomService
                .LeaveRoomAsSpectatorAsync(id, userId);

            if (left)
            {
                TempData["SuccessMessage"] =
                    "You have left the room as a spectator.";
            }
            else
            {
                TempData["ErrorMessage"] =
                    "You are not a spectator in this room.";
            }

            return RedirectToAction(nameof(Details), new { id });
        }

    }
}
