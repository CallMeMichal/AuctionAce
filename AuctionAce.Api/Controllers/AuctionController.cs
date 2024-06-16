using AuctionAce.Api.Models.DTO.Auctions.Request;
using AuctionAce.Api.Models.DTO.Auctions.Response;
using AuctionAce.Api.Models.ViewModels.Auctions;
using AuctionAce.Api.Models.ViewModels.Home;
using AuctionAce.Application.Services;
using AuctionAce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Index()
        {
            AuctionViewModel model = new AuctionViewModel();
            var cookie = Request.Cookies["Id"];
            int.TryParse(cookie, out int idUser);
            model.User =await _loginService.UserLogin(idUser);
            var auctions = await _auctionService.GetAuctionsByIdUserAsync(idUser);
            model.Auctions = auctions;
            return View(model);
        }

        [HttpGet] 
        public async Task<IActionResult> AddAuction()
        {
            AuctionViewModel model = new AuctionViewModel();
            var cookie = Request.Cookies["Id"];
            int.TryParse(cookie, out int idUser);
            model.User = await _loginService.UserLogin(idUser);
            var auctions = await _auctionService.GetAuctionsByIdUserAsync(idUser);
            model.Auctions = auctions;

            return View(model);
        }
        [HttpPost]
        public async Task<AuctionResponse> AddAuction(AddAuctionRequest request)
        {
            AuctionResponse response = new AuctionResponse();
            var cookie = Request.Cookies["Id"];
            int idUser = Int32.Parse(cookie);
            response.Success = await _auctionService.AddAuctionAsync(request.AuctionName, request.Description, request.StartDate, request.EndDate, idUser, request.Items);
            return response;
        }

        [HttpGet]
        public async Task<IActionResult> AllAuctionsById()
        {
            AuctionViewModel model = new AuctionViewModel();
            var cookie = Request.Cookies["Id"];
            int.TryParse(cookie, out int idUser);
            model.Auctions = await _auctionService.GetAuctionsByIdUserAsync(idUser);
            model.User = await _loginService.UserLogin(idUser);

            return View("AllAuctions",model);
        }
        [HttpGet]
        public async Task<IActionResult> AllAuctions()
        {
            AuctionViewModel model = new AuctionViewModel();
            var cookie = Request.Cookies["Id"];
            int.TryParse(cookie, out int idUser);
            var auctions = await _auctionService.GetAuctionsAsync();

            model.Auctions = auctions;
            model.User = await _loginService.UserLogin(idUser);

            return View(model);
        }

        

        [HttpGet]
        public async Task<IActionResult> AuctionItemList(int auctionId)
        {
            AuctionViewModel model = new AuctionViewModel();
            var cookie = Request.Cookies["Id"];
            int.TryParse(cookie, out int idUser);
            var auction = await _auctionService.GetAuctionAsync(auctionId);
            model.AuctionItems = auction;
            model.User = await _loginService.UserLogin(idUser);
            return View(model);
        }
    }
}
