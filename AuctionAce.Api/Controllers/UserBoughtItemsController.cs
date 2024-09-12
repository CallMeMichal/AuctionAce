using AuctionAce.Api.Controllers.Helpers;
using AuctionAce.Api.Models.ViewModels.UserBoughtItems;
using AuctionAce.Application.Middleware;
using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
	public class UserBoughtItemsController : Controller
	{

		public UserBoughtItemsService _boughtItemsService { get; set; }

		public UserBoughtItemsController(UserBoughtItemsService boughtItemsService)
		{
			_boughtItemsService = boughtItemsService;
		}

        [HttpGet]
        [JwtAuthentication("1", "2")]
        public IActionResult Index(int itemId, int buyNowPrice)
        {
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            List<UserBoughtItemsModel> model = new List<UserBoughtItemsModel>();

            if (itemId != 0 && buyNowPrice != 0)
            {
                string status = "inactive";
                var setButtonVisibility = _boughtItemsService.SetAuctionItemStatus(itemId, status).Result;
                var setItemToUser = _boughtItemsService.AddItemToBought(itemId, userId, buyNowPrice).Result;
            }

            var boughtItemsData = _boughtItemsService.GetUserItems(userId).Result;
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
            }

            foreach (var item in boughtItemsData)
            {
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

                model.Add(boughtItem);
            }

            return View(model);
        }



    }
}
