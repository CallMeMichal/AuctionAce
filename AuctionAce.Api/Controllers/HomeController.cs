using AuctionAce.Api.Models;
using AuctionAce.Api.Models.ViewModels.Auctions;
using AuctionAce.Api.Models.ViewModels.Home;
using AuctionAce.Application.Services;
using AuctionAce.Domain.Entities;
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

            var allAuctionsPhoto = new List<AuctionListDomain>();
            var auctionss = await _auctionService.GetAllAuctionsAsync();
            foreach (var auction in auctionss)
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
    }
}