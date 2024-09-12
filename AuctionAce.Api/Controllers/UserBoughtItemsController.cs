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
			UserBoughtItemsModel model = new UserBoughtItemsModel();

			if (itemId != 0 && buyNowPrice != 0)
			{
				string status = "inactive";
				var setButtonVisibility = _boughtItemsService.SetAuctionItemStatus(itemId, status).Result;
				var setItemToUser = _boughtItemsService.AddItemToBought(itemId, userId, buyNowPrice).Result;

			}

			var boughtItemsData = _boughtItemsService.GetUserItems(userId).Result;
			model.itemPhotos = new Dictionary<int?, List<UserBoughtItemsPhotoModel>>();
			foreach (var item in boughtItemsData)
			{
				model.YourPrize = item.YourPrize;
				model.Category = item.Category;
				model.ItemName = item.ItemName;
				model.NewUsed = item.NewUsed;
				model.Description = item.Description;
				foreach (var group in item.GroupedItemPhotos)
				{
					// Dodajemy do słownika pogrupowane zdjęcia według AuctionItemId
					model.itemPhotos.Add(group.Key, group.Value.Select(photoItem => new UserBoughtItemsPhotoModel
					{
						Id = photoItem.Id,
						PhotoBase64 = photoItem.PhotoBase64
					}).ToList());
				}
			}

			return View(model);
		}
	}
}
