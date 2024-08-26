using AuctionAce.Application.Interfaces.ChatHistory;
using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.Repositories;

namespace AuctionAce.Application.Services
{
    public class ChatHistoryService : IChatHistoryService
    {
        public readonly ChatHistoryRepostiory _chatHistoryRespository;

        public ChatHistoryService(ChatHistoryRepostiory chatHistoryRespository)
        {
            _chatHistoryRespository = chatHistoryRespository;
        }

        public async Task AddChatHistory(ChatHistoryDomain request)
        {
            var data = new ChatHistory()
            {
                AuctionItemId = request.AuctionItemId,
                Date = request.Date,
                UserId = request.UserId,
                Message = request.Message,
            };

            var response = _chatHistoryRespository.AddChatHistory(data);
        }
    }
}
