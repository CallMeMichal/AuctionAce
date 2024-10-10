using AuctionAce.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionAce.Infrastructure.Repositories
{
    public class WishlistRepository
    {
        private readonly ApplicationDbContext _context;

        public WishlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAuctionToWishlist(Wishlist wishlist)
        {

            await _context.Wishlists.AddAsync(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAuctionFromWishlist(Wishlist wishlist)
        {

            var auctionLiked = _context.Wishlists.FirstOrDefaultAsync(x=>x.IdAuction == wishlist.IdAuction && x.IdUser == wishlist.IdUser && x.IsLiked==true).Result;
            _context.Wishlists.Remove(auctionLiked);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Wishlist>> GetWishlistForUser(int userId)
        {
            var userWishlist = await _context.Wishlists.Where(x=>x.IdUser == userId).ToListAsync();
            return userWishlist;
        }
    }
}
