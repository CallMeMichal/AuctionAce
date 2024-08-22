using Microsoft.AspNetCore.SignalR;

namespace AuctionAce.Api.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        }

        public async Task SendMessageToGroup(string groupName, string message,string userEmail)
        {
            var date = DateTime.Now.ToString("(MM/dd HH:mm)");
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message,userEmail,date);

        }

    }
}
