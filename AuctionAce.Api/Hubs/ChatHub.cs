
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

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
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