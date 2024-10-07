using AuctionAce.Api.Controllers.Helpers;
using AuctionAce.Api.Models.DTO.Wishlist.Request;
using AuctionAce.Application.Middleware;
using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class WishlistController : Controller
    {
        public readonly WishlistService _wishlistService;

        public WishlistController(WishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public IActionResult Index()
        {
            return View();
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
