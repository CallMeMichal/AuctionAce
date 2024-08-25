using AuctionAce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AuctionAce.Infrastructure.Repositories
{
    public class ChatHistoryRepostiory
    {
        private readonly ApplicationDbContext _context;

        public ChatHistoryRepostiory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveMessage()
        {
            var z = await _context.ChatHistories.ToListAsync();
            var yyy = "";
            return true;
        }
    }
}