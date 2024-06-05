using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class AuctionController : Controller
    {
        public IActionResult AddAuction()
        {
            return View();
        }
    }
}
