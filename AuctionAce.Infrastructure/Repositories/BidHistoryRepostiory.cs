using AuctionAce.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionAce.Infrastructure.Repositories
{
    public class BidHistoryRepostiory
    {
        private readonly ApplicationDbContext _context;

        public BidHistoryRepostiory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Task> AddBidHistory(BidHistory data)
        {
            await _context.AddAsync(data);
            await _context.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<List<BidHistory>> GetBidHistory(int auctionItemId)
        {
            var bidHistory = await _context.BidHistories.Where(x => x.IdAuctionItems == auctionItemId).ToListAsync();
            return bidHistory;
        }
        public async Task<int> GetHighestBidForItem(int itemId)
        {
            var allBids = await GetBidHistory(itemId);
            var highestBid = allBids.Max(x => x.Price);
            return Convert.ToInt32(highestBid);
        }

        public async Task<int> GetHighestBidForItemAndSave(int itemId)
        {
            var allBids = await GetBidHistory(itemId);
            var highestBid = allBids.Max(x => x.Price);

            var userId = allBids.FirstOrDefault(z => z.Price == highestBid).IdUsers;

            if (userId != null)
            {
                var item = _context.AuctionItems.FirstOrDefault(x=>x.Id == itemId);
                if (item != null)
                {
                    item.IsBought = true;
                }
                var a = allBids.FirstOrDefault(a => a.IdUsers == userId);
                if(a != null)
                {
                    a.IsWin = true;
                }

                UserBoughtItem userBoughtItem = new UserBoughtItem()
                {
                    Prize = Convert.ToInt32(highestBid),
                    IdUser = userId,
                    IdAuctionItem = itemId,
                    
                };
                _context.UserBoughtItems.Add(userBoughtItem);
                await _context.SaveChangesAsync();
            }

            return Convert.ToInt32(highestBid);
        }
    }
}
