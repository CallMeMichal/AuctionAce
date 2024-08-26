using Microsoft.AspNetCore.SignalR;

namespace AuctionAce.Api.Hubs
{
    public class BidHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        }
        public async Task SendBidToGroup(string groupName, long bid, string userEmail)
        {
            var date = DateTime.Now.ToString("(MM/dd HH:mm)");

            await Clients.Group(groupName).SendAsync("ReceiveBid", bid, userEmail, date);

        }
    }
}
