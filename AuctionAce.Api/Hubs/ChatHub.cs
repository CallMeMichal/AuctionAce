using AuctionAce.Application.Services;
using Microsoft.AspNetCore.SignalR;

namespace AuctionAce.Api.Hubs
{
    public class ChatHub : Hub
    {
        public readonly AuctionService _auctionService;

        public ChatHub(AuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, string message, string userEmail)
        {
            var date = DateTime.Now.ToString("(MM/dd HH:mm)");

            await Clients.Group(groupName).SendAsync("ReceiveMessage", message, userEmail, date);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}