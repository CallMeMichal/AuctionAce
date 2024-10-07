using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionAce.Application.Services
{
    public class WishlistService
    {
        public readonly WishlistRepository _wishlistRepository;

        public WishlistService(WishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task AddAuctionToWishlist(int auctionId,int userId)
        {
            Wishlist wishlist = new Wishlist()
            {
                IsLiked = true,
                IdAuction = auctionId,
                IdUser = userId
            };
            await _wishlistRepository.AddAuctionToWishlist(wishlist);
        }

        public async Task RemoveAuctionFromWishlist(int auctionId, int userId)
        {
            Wishlist wishlist = new Wishlist()
            {
                IsLiked = false,
                IdAuction = auctionId,
                IdUser = userId
            };
            await _wishlistRepository.RemoveAuctionFromWishlist(wishlist);
        }

        public async Task<List<WishlistDomain>> GetWishlistForUser(int userId)
        {
            List<WishlistDomain> wishlistDomains = new List<WishlistDomain>();
            var userWishlist = await _wishlistRepository.GetWishlistForUser(userId);
            foreach (var wish in userWishlist)
            {
                var wishItem = new WishlistDomain()
                {
                    AuctionId = wish.IdAuction,
                    UserId = wish.IdUser
                };
                wishlistDomains.Add(wishItem);
            }
            return wishlistDomains;
        }
    }
}
