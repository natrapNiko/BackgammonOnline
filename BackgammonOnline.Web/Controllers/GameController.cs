using Microsoft.AspNetCore.Mvc;

namespace BackgammonOnline.Web.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
