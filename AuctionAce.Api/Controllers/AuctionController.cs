using AuctionAce.Api.Controllers.Helpers;
using AuctionAce.Api.Models.DTO.Auctions.Request;
using AuctionAce.Api.Models.DTO.Auctions.Response;
using AuctionAce.Api.Models.ViewModels.Auctions;
using AuctionAce.Application.Middleware;
using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        /*[JwtAuthentication("1", "2")]
        [HttpPost]
        public IActionResult AddAuction([FromForm] AddAuctionRequest request)
        {
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);

            var auction = _auctionService.AddAuctionAsync(
                request.AuctionName,
                request.Description,
                request.StartDate,
                request.EndDate,
                userId,
                request.Items
                request.AuctionImage,
                request.ItemImages
                ).Result;

            if (auction == true)
            {
                return Json(new { success = true, message = "Auction succesfully added" });
            }
            else
            {
                return Json(new { success = false, message = "Auction unsuccesfully added" });
            }
        }*/

        [JwtAuthentication("1", "2")]
        [HttpGet]
        public IActionResult AllAuctionsById()
        {
            AuctionViewModel model = new AuctionViewModel();
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            model.AuctionStatus = _auctionService.GetAuctionsAsync(userId).Result;
            return View("AllAuctions", model);
        }

        [JwtAuthentication("1", "2")]
        [HttpGet]
        public IActionResult AllAuctions()
        {
            AuctionViewModel model = new AuctionViewModel();
            var auctions = _auctionService.GetAuctionsAsync().Result;
            model.AuctionStatus = auctions;

            return View(model);

        }
        

        [JwtAuthentication("1", "2")]
        [HttpGet]
        public IActionResult AuctionItemList(int auctionId)
        {
            AuctionViewModel model = new AuctionViewModel();
            var auction = _auctionService.GetAuctionAsync(auctionId).Result;
            model.AuctionItems = auction;
            return View(model);
        }
    }
}