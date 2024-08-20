using AuctionAce.Api.Models;
using AuctionAce.Api.Models.ViewModels.Home;
using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuctionAce.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuctionService _auctionService;
        private readonly LoginService _loginService;

        public HomeController(AuctionService auctionService, LoginService loginService)
        {
            _auctionService = auctionService;
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel();
            var cookie = Request.Cookies["Id"];
            int.TryParse(cookie, out int idUser);

            model.User = await _loginService.UserLogin(idUser);
            var auctions = await _auctionService.GetAuctionsByIdUserAsync(idUser);
            model.Auctions = auctions;
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}