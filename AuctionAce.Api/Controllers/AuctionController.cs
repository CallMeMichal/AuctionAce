using AuctionAce.Api.Controllers.Helpers;
using AuctionAce.Api.Models.DTO.Auctions.Request;
using AuctionAce.Api.Models.ViewModels.Auctions;
using AuctionAce.Application.Middleware;
using AuctionAce.Application.Services;
using AuctionAce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class AuctionController : Controller
    {
        private readonly AuctionService _auctionService;
        private readonly LoginService _loginService;

        public AuctionController(AuctionService auctionService, LoginService loginService)
        {
            _auctionService = auctionService;
            _loginService = loginService;
        }

        [HttpGet]
        [JwtAuthentication("1", "2")]
        public async Task<IActionResult> Index()
        {
            AuctionViewModel model = new AuctionViewModel();
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            model.User = await _loginService.UserLogin(userId);
            var auctions = await _auctionService.GetAuctionsByIdUserAsync(userId);
            model.Auctions = auctions;
            return View(model);
        }

        [HttpGet]
        [JwtAuthentication("1", "2")]
        public IActionResult AddAuction()
        {
            AuctionViewModel model = new AuctionViewModel();
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            model.User = _loginService.UserLogin(userId).Result;
            var auctions = _auctionService.GetAuctionsByIdUserAsync(userId).Result;
            model.Auctions = auctions;

            return View(model);
        }

        [JwtAuthentication("1", "2")]
        [HttpPost]
        public IActionResult AddAuction(AddAuctionRequest request)
        {
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);

            var auctionImagesPaths = SessionHelper.SaveFilesAsync(request.AuctionImages, request.AuctionName, true).Result;

            var itemsDomain = new List<AuctionItemsDomain>();

            foreach (var item in request.Items)
            {
                var itemImagePaths = SessionHelper.SaveFilesAsync(item.ItemImages, request.AuctionName, false).Result;
                item.ItemImagePaths = itemImagePaths;

                var auctionItemDomain = new AuctionItemsDomain
                {
                    Name = item.Name,
                    Description = item.Description,
                    Category = item.Category,
                    StartingPrice = item.StartingPrice,
                    BuyNowPrice = item.BuyNowPrice,
                    NewUsed = item.NewUsed,
                    ItemImagePaths = itemImagePaths,
                };

                itemsDomain.Add(auctionItemDomain);
            }

            var auction = _auctionService.AddAuctionAsync(
                request.AuctionName,
                request.Description,
                request.StartDate,
                request.EndDate,
                userId,
                auctionImagesPaths,
                itemsDomain
            ).Result;

            if (auction == true)
            {
                return Json(new { success = true, message = "Auction succesfully added" });
            }
            else
            {
                return Json(new { success = false, message = "Auction unsuccessfully added" });
            }
        }

        [JwtAuthentication("1", "2")]
        [HttpGet]
        public IActionResult AllAuctionsById()
        {
            AuctionViewModel model = new AuctionViewModel();
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            var auctions = _auctionService.GetAuctionsAsync(userId).Result;
            var allAuctionsPhoto = new List<AuctionListDomain>();
            foreach (var auction in auctions)
            {
                var photos = _auctionService.GetPhotos(auction.Id).Result;

                var auctionStatus = new AuctionListDomain
                {
                    Id = auction.Id,
                    Description = auction.Description,
                    AuctionName = auction.AuctionName,
                    IdUsers = auction.IdUsers,
                    StartDate = auction.StartDate,
                    EndDate = auction.EndDate,
                    Status = auction.Status,
                    AllAuctionsPhotos = photos,
                    AuctionsListItem = auction.AuctionsListItem
                };

                allAuctionsPhoto.Add(auctionStatus);
            }

            model.AuctionStatus = allAuctionsPhoto;
            return View("AllAuctions", model);
        }

        [JwtAuthentication("1", "2")]
        [HttpGet]
        public IActionResult AllAuctions()
        {
            AuctionViewModel model = new AuctionViewModel();
            var auctions = _auctionService.GetAuctionsAsync().Result;
            var allAuctionsPhoto = new List<AuctionListDomain>();
            foreach (var auction in auctions)
            {
                var photos = _auctionService.GetPhotos(auction.Id).Result;

                var auctionStatus = new AuctionListDomain
                {
                    Id = auction.Id,
                    Description = auction.Description,
                    AuctionName = auction.AuctionName,
                    IdUsers = auction.IdUsers,
                    StartDate = auction.StartDate,
                    EndDate = auction.EndDate,
                    Status = auction.Status,
                    AllAuctionsPhotos = photos,
                    AuctionsListItem = auction.AuctionsListItem
                };

                allAuctionsPhoto.Add(auctionStatus);
            }
            model.AuctionStatus = allAuctionsPhoto;

            return View(model);
        }

        [JwtAuthentication("1", "2")]
        [HttpGet]
        public IActionResult AuctionItemList(int auctionId)
        {
            AuctionViewModel model = new AuctionViewModel();
            var auction = _auctionService.GetAuctionAsync(auctionId).Result;
            var photos = _auctionService.GetPhotos(auctionId).Result;
            model.SingleItemImages = photos;
            model.AuctionItems = auction;
            return View(model);
        }
    }
}