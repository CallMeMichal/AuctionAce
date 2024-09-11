using AuctionAce.Application.Middleware;
using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class BoughtItemsController : Controller
    {

        public UserBoughtItemsService _boughtItemsService { get; set; }

        public BoughtItemsController(UserBoughtItemsService boughtItemsService)
        {
            _boughtItemsService = boughtItemsService;
        }

        [HttpGet]
        [JwtAuthentication("1", "2")]
        public IActionResult Index(int itemId,int buyNowPrice,int userId)
        {
            if (itemId != 0 && buyNowPrice != 0)
            {
                string status = "inactive";
                var setButtonVisibility = _boughtItemsService.SetAuctionItemStatus(itemId, status).Result;
                var setItemToUser = _boughtItemsService.AddItemToBought(itemId, userId, buyNowPrice).Result;
                var a = _boughtItemsService.GetUserItems(userId);
            }
            


            return View();
        }
    }
}
