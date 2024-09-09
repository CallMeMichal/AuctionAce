

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
    }
}
