using AuctionAce.Application.Middleware;
using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class BoughtItemsController : Controller
    {

        public BoughtItemsService _boughtItemsService { get; set; }

        public BoughtItemsController(BoughtItemsService boughtItemsService)
        {
            _boughtItemsService = boughtItemsService;
        }

        [HttpGet]
        [JwtAuthentication("1", "2")]
        public IActionResult Index(string itemGuid)
        {
            string status = "inactive";
            var setButtonVisibility = _boughtItemsService.SetAuctionItemStatus(itemGuid, status).Result;
            return View();
        }
    }
}
