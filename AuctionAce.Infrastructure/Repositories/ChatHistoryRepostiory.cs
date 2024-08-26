using AuctionAce.Infrastructure.Data.Models;
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

        public async Task AddChatHistory(ChatHistory chat)
        {
            await _context.ChatHistories.AddAsync(chat);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChatHistory>> GetListChatHistoryByAuctionItemId(int id)
        {
            var chatHistory = await _context.ChatHistories.Where(x => x.AuctionItemId == id).ToListAsync();
            return chatHistory;
        }
    }
}