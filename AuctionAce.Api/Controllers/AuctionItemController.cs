using AuctionAce.Api.Models.ViewModels.ItemViewModel;
using AuctionAce.Application.Services;
using AuctionAce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class AuctionItemController : Controller
    {
        private readonly AuctionService _auctionService;

        public AuctionItemController(AuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public IActionResult Index(int itemId, string itemGuid)
        {

            List<PhotosItemDomain> photosItemDomains = _auctionService.GetPhotosForOneItem(itemId).Result;


            ItemViewModel item = new ItemViewModel();
            item.Id = itemId;
            item.Guid = itemGuid;
            item.ItemDomain = photosItemDomains;


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
