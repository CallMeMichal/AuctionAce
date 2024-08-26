
using AuctionAce.Application.Services;
using AuctionAce.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace AuctionAce.Api.Hubs
{
    public class ChatHub : Hub
    {

        public readonly ChatHistoryService _chatHistoryService;

        public ChatHub(ChatHistoryService chatHistoryService)
        {
            _chatHistoryService = chatHistoryService;
        }

        public async Task JoinGroup(string groupName, string auctionItemId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var auctionIdItem = Convert.ToInt32(auctionItemId);
            var chatHistories = await _chatHistoryService.GetChatHistory(auctionIdItem);

            chatHistories = chatHistories.OrderBy(chat => chat.Date).ToList();

            var formattedChatHistories = chatHistories.Select(chat => new
            {
                chat.Message,
                chat.UserEmail,
                DateFormatted = chat.Date.Value.ToString("(MM/dd HH:mm)"),
            }).ToList();

            await Clients.Group(groupName).SendAsync("ReceiveHistoryMessages", formattedChatHistories);
        }

        public async Task SendMessageToGroup(string groupName, string message, string userEmail, string auctionItemId, string userId)
        {
            var dataToDatabase = DateTime.Now;
            var date = dataToDatabase.ToString("(MM/dd HH:mm)");

            var auctionIdItem = Convert.ToInt32(auctionItemId);
            var idUser = Convert.ToInt32(userId);

            ChatHistoryDomain request = new ChatHistoryDomain()
            {
                AuctionItemId = auctionIdItem,
                Message = message,
                UserEmail = userEmail,
                Date = dataToDatabase,
                UserId = idUser
            };
            await _chatHistoryService.AddChatHistory(request);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message, userEmail, date);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}