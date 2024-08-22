using AuctionAce.Api.Models.ViewModels.ItemViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class AuctionItemController : Controller
    {
        public IActionResult Index(int itemId, string itemGuid)
        {
            ItemViewModel item = new ItemViewModel();
            item.Id = itemId;
            item.Guid = itemGuid;

            return View(item);
        }

        [HttpPost]
        public IActionResult SendMessageToGroup(string groupName, string message)
        {
            return View("Index");
        }

        /*public IActionResult GetHistoryMessages(string groupName)
        {
            return View();
        }

        public IActionResult GetActiveUsersForGroup()
        {
            return View();
        }*/
    }
}
