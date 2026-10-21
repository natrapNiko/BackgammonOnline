using BackgammonOnline.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
