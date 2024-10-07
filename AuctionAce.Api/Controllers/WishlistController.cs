using AuctionAce.Api.Controllers.Helpers;
using AuctionAce.Api.Models.DTO.Wishlist.Request;
using AuctionAce.Api.Models.ViewModels.WishlistViewModel;
using AuctionAce.Application.Middleware;
using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class WishlistController : Controller
    {
        public readonly WishlistService _wishlistService;
        public readonly AuctionService _auctionService;

        public WishlistController(WishlistService wishlistService, AuctionService auctionService)
        {
            _wishlistService = wishlistService;
            _auctionService = auctionService;
        }

        public IActionResult Index()
        {
            WishlistViewModel model = new WishlistViewModel();
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            var likedAuctions = _wishlistService.GetWishlistForUser(userId).Result;
            var likedAuctionsData = _auctionService.GetAuctionById(likedAuctions).Result;
            model.AuctionDataDomain = likedAuctionsData;
            return View(model);
        }

        [HttpPost]
        [JwtAuthentication("1", "2")]
        public IActionResult AddToWishlist([FromBody]WishlistRequest request)
        {
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            var auctionWishlist = _wishlistService.AddAuctionToWishlist(request.AuctionId, userId);
            return Ok(new { success = true, message = "Auction added to wishlist", auctionId = request.AuctionId });
        }

        [HttpPost]
        [JwtAuthentication("1", "2")]
        public IActionResult RemoveFromWishlist([FromBody]WishlistRequest request)
        {
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            var auctionWishlist = _wishlistService.RemoveAuctionFromWishlist(request.AuctionId, userId);
            return Ok(new { success = true, message = "Auction added to wishlist", auctionId = request.AuctionId });
        }
    }
}
