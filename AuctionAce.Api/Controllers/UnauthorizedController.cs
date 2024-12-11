using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class UnauthorizedController : Controller
    {
/*        public IActionResult Index()
        {
            return View();
        }*/
        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}
