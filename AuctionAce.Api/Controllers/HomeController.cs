using AuctionAce.Api.Models;
using AuctionAce.Api.Models.ViewModels.Auctions;
using AuctionAce.Api.Models.ViewModels.Home;
using AuctionAce.Api.Models.ViewModels.UserBoughtItems;
using AuctionAce.Application.Services;
using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuctionService _auctionService;
        private readonly LoginService _loginService;
        private readonly UserBoughtItemsService _boughtItemsService;

        public HomeController(AuctionService auctionService, LoginService loginService, UserBoughtItemsService userBoughtItemsService)
        {
            _auctionService = auctionService;
            _loginService = loginService;
            _boughtItemsService = userBoughtItemsService;
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
            List<UserBoughtItemsModel> modelItems = new List<UserBoughtItemsModel>();
            var boughtItemsData = _boughtItemsService.GetUserItems().Result;
            var itemPhotosGroups = new List<UserBoughtItemsPhotosGroupModel>();
            foreach (var item in boughtItemsData)
            {
                foreach (var photoGroup in item.GroupedItemPhotos)
                {
                    var groupItemId = photoGroup.AuctionItemId;
                    var photos = photoGroup.Photos;

                    itemPhotosGroups.Add(new UserBoughtItemsPhotosGroupModel
                    {
                        AuctionItemId = groupItemId,
                        Photos = photos.Select(p => new UserBoughtItemsPhotoModel
                        {
                            Id = p.Id,
                            PhotoBase64 = p.PhotoBase64
                        }).ToList()
                    });
                }
                var boughtItem = new UserBoughtItemsModel
                {
                    YourPrize = item.YourPrize,
                    Category = item.Category,
                    ItemName = item.ItemName,
                    NewUsed = item.NewUsed,
                    Description = item.Description,
                    ItemPhotos = itemPhotosGroups
                        .Where(p => p.AuctionItemId == item.AuctionItemId)
                        .ToList()
                };
                modelItems.Add(boughtItem);
            }
            
            model.UserBoughtItemsModels = modelItems;
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