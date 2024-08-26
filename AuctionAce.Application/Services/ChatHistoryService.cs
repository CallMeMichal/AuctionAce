using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.Repositories;

namespace AuctionAce.Application.Services
{
    public class ChatHistoryService
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
                UserEmail = request.UserEmail,

            };

            await _chatHistoryRespository.AddChatHistory(data);
        }

        public async Task<List<ChatHistoryDomain>> GetChatHistory(int itemId)
        {
            var response = await _chatHistoryRespository.GetListChatHistoryByAuctionItemId(itemId);
            List<ChatHistoryDomain> chatHistory = new List<ChatHistoryDomain>();
            if (response.Count > 0)
            {
                foreach (var chatHistoryItem in response)
                {
                    chatHistory.Add(new ChatHistoryDomain()
                    {
                        AuctionItemId = chatHistoryItem.AuctionItemId,
                        Date = chatHistoryItem.Date,
                        Message = chatHistoryItem.Message,
                        UserId = chatHistoryItem.UserId,
                        UserEmail = chatHistoryItem.UserEmail
                    });
                }
                return chatHistory;
            }
            else
                return new List<ChatHistoryDomain>();
        }
    }
}
