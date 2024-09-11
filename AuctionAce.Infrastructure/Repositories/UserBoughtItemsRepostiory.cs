using AuctionAce.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var savedResponse = await _context.SaveChangesAsync();
            if (savedResponse > 0)
            {
                return true;
            }
            return false;
        }

    }
}
