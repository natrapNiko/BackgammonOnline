using BackgammonOnline.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackgammonOnline.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult Create(CreateGameRoomViewModel model) 
        { 
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            TempData["SuccessMessage"] =
                "Form validated successfully. Room creation will be implemented next.";
            return RedirectToAction(nameof(Index));
        }
    }
}
