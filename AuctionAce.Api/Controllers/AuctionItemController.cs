using AuctionAce.Api.Models.ViewModels.ItemViewModel;
using AuctionAce.Application.Services;
using AuctionAce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class AuctionItemController : Controller
    {
        private readonly AuctionService _auctionService;
        private readonly BidHistoryService _bidHistoryService;

        public AuctionItemController(AuctionService auctionService, BidHistoryService bidHistoryService)
        {
            _auctionService = auctionService;
            _bidHistoryService = bidHistoryService;
        }

        public IActionResult Index(int itemId, string itemGuid)
        {
            var buyNowPrice = _auctionService.GetBuyNowPriceForItem(itemId).Result;
            var startPrice = _auctionService.GetStartPriceForItem(itemId).Result;
            var highestPrice = _bidHistoryService.GetHighestBidForItem(itemId).Result;
            if (highestPrice == null || highestPrice == 0)
            {
                highestPrice = startPrice;
            }
            List<PhotosItemDomain> photosItemDomains = _auctionService.GetPhotosForOneItem(itemId).Result;

            ItemViewModel item = new ItemViewModel();
            item.Id = itemId;
            item.Guid = itemGuid;
            item.ItemDomain = photosItemDomains;
            item.StartPrice = startPrice;
            item.BuyNowPrice = buyNowPrice;
            item.ActualHighestPrice = highestPrice;

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
