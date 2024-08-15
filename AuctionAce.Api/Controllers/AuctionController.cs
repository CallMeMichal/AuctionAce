using AuctionAce.Api.Controllers.Helpers;
using AuctionAce.Api.Models.DTO.Auctions.Request;
using AuctionAce.Api.Models.DTO.Auctions.Response;
using AuctionAce.Api.Models.ViewModels.Auctions;
using AuctionAce.Application.Middleware;
using AuctionAce.Application.Services;
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
        public async Task<IActionResult> AddAuction()
        {
            AuctionViewModel model = new AuctionViewModel();
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            model.User = await _loginService.UserLogin(userId);
            var auctions = await _auctionService.GetAuctionsByIdUserAsync(userId);
            model.Auctions = auctions;

            return View(model);
        }

        [JwtAuthentication("1", "2")]
        [HttpPost]
        public async Task<AuctionResponse> AddAuction(AddAuctionRequest request)
        {
            AuctionResponse response = new AuctionResponse();
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            response.Success = await _auctionService.AddAuctionAsync(request.AuctionName, request.Description, request.StartDate, request.EndDate, userId, request.Items);
            return response;
        }

        [JwtAuthentication("1", "2")]
        [HttpGet]
        public async Task<IActionResult> AllAuctionsById()
        {
            AuctionViewModel model = new AuctionViewModel();
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            model.Auctions = await _auctionService.GetAuctionsByIdUserAsync(userId);
            model.User = await _loginService.UserLogin(userId);

            return View("AllAuctions", model);
        }

        [JwtAuthentication("1", "2")]
        [HttpGet]
        public async Task<IActionResult> AllAuctions()
        {
            AuctionViewModel model = new AuctionViewModel();
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            var auctions = await _auctionService.GetAuctionsAsync();

            model.Auctions = auctions;
            model.User = await _loginService.UserLogin(userId);

            return View(model);
        }

        [JwtAuthentication("1", "2")]
        [HttpGet]
        public async Task<IActionResult> AuctionItemList(int auctionId)
        {
            AuctionViewModel model = new AuctionViewModel();
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            var auction = await _auctionService.GetAuctionAsync(auctionId);
            model.AuctionItems = auction;
            model.User = await _loginService.UserLogin(userId);
            return View(model);
        }
    }
}