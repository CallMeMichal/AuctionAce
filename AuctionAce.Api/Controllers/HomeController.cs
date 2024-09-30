using AuctionAce.Api.Models;
using AuctionAce.Api.Models.ViewModels.Auctions;
using AuctionAce.Api.Models.ViewModels.Home;
using AuctionAce.Application.Services;
using AuctionAce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuctionService _auctionService;
        private readonly LoginService _loginService;
        private readonly UserBoughtItemsService _userBoughtItemsService;

        public HomeController(AuctionService auctionService, LoginService loginService, UserBoughtItemsService userBoughtItemsService)
        {
            _auctionService = auctionService;
            _loginService = loginService;
            _userBoughtItemsService = userBoughtItemsService;
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
            var items = await _userBoughtItemsService.GetUserItems(); /// todo zrobic wyswietlanie tych kupionych rzeczy pod przycisk w widoku
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
            model.AuctionStatus = allAuctionsPhoto.OrderBy(a => Guid.NewGuid()).ToList();
            return View(model);
        }
    }
}