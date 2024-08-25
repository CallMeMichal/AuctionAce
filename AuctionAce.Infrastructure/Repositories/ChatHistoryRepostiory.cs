using AuctionAce.Infrastructure.Data;
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
        }

        public async Task<List<ChatHistory>> GetListChatHistoryByAuctionItemId(int id)
        {
            var chatHistory = await _context.ChatHistories.Where(x => x.Id == id).ToListAsync();
            return chatHistory;
        }
    } 
}