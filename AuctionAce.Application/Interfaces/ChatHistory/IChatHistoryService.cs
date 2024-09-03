using AuctionAce.Domain.Entities;

namespace AuctionAce.Application.Interfaces.ChatHistory
{
    public interface IChatHistoryService
    {
        Task AddChatHistory(ChatHistoryDomain request);
    }
}