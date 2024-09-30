using AuctionAce.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionAce.Infrastructure.Repositories
{

    public class UserBoughtItemsRepostiory
    {
        private readonly ApplicationDbContext _context;

        public UserBoughtItemsRepostiory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItemToUser(int itemId, int price, int userId)
        {

            await _context.UserBoughtItems.AddAsync(new UserBoughtItem
            {
                IdAuctionItem = itemId,
                IdUser = userId,
                Prize = price,
            });
            var item = await _context.AuctionItems.FirstOrDefaultAsync(x => x.Id == itemId);
            if (item != null)
            {
                item.IsBought = true;
                _context.AuctionItems.Update(item);
            }
            
            var savedResponse = await _context.SaveChangesAsync();
            if (savedResponse > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<UserBoughtItem>> GetUserBoughtItems(int userId)
        { 
            IQueryable<UserBoughtItem> query = _context.UserBoughtItems.Include(x => x.IdAuctionItemNavigation)
                .ThenInclude(x => x.AuctionsItemsPhotos)
                .Include(x => x.IdUserNavigation);

            if (userId != 0)
            {
                query = query.Where(x => x.IdUser == userId);
            }

            return await query.ToListAsync();
        }


        public async Task<List<UserBoughtItem>> GetBoughtItems()
        {

            var response = await _context.UserBoughtItems.Include(x => x.IdAuctionItemNavigation).ThenInclude(x => x.AuctionsItemsPhotos).Include(x => x.IdUserNavigation).ToListAsync();
            return response;

        }
    }
}
