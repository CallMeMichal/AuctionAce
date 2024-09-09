
using AuctionAce.Application.Services;
using AuctionAce.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace AuctionAce.Api.Hubs
{
    public class Hub : Microsoft.AspNetCore.SignalR.Hub
    {

        public readonly ChatHistoryService _chatHistoryService;
        public readonly BidHistoryService _bidHistoryService;
        public readonly AuctionService _auctionService;

        public Hub(ChatHistoryService chatHistoryService, BidHistoryService bidHistoryService, AuctionService auctionService)
        {
            _chatHistoryService = chatHistoryService;
            _bidHistoryService = bidHistoryService;
            _auctionService = auctionService;
        }

        public async Task JoinGroup(string groupName, string auctionItemId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var auctionIdItem = Convert.ToInt32(auctionItemId);
            var chatHistories = await _chatHistoryService.GetChatHistory(auctionIdItem);
            var bidHistories = await _bidHistoryService.GetBidHistory(auctionIdItem);
            var auctionId = await _auctionService.GetAuctionIdByItemId(auctionIdItem);
            var remainingTime = await _auctionService.GetRemainingTimeForAuction(auctionId);
            var parsedRemainingTime = ParsedRemainingTime(remainingTime.ToString());

            chatHistories = chatHistories.OrderBy(chat => chat.Date).ToList();

            var formattedChatHistories = chatHistories.Select(chat => new
            {
                chat.Message,
                chat.UserEmail,
                DateFormatted = chat.Date.Value.ToString("(MM/dd HH:mm)"),
            }).ToList();

            var formattedBidHistories = bidHistories.Select(bids => new
            {
                bids.Price,
                bids.UserEmail,
                bids.DateTime,
            }).OrderByDescending(x=>x.Price).ToList();

            var highestBid = formattedBidHistories.FirstOrDefault();
                
            await Clients.Group(groupName).SendAsync("ReceiveHistoryMessages", formattedChatHistories);
            await Clients.Group(groupName).SendAsync("ReceiveHistoryBids", formattedBidHistories,highestBid);
            await Clients.Group(groupName).SendAsync("ReceiveRemainingAuctionTime", parsedRemainingTime);
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

        public async Task SendBidToGroup(string groupName, long bid, string userEmail, string userId,string auctionItemId)
        {
            var date = DateTime.Now.ToString("(MM/dd HH:mm)");
            var decimalPrice = Convert.ToDecimal(bid);
            var dataToDatabase = DateTime.Now;
            var auctionItemIdToInt = Convert.ToInt32(auctionItemId);
            var userIdToInt = Convert.ToInt32(userId);
            BidHistoryDomain request = new BidHistoryDomain()
            {
                Price = decimalPrice,
                DateTime =dataToDatabase,
                AuctionItemId = auctionItemIdToInt,
                UserId = userIdToInt,
                UserEmail= userEmail,
            };
            await _bidHistoryService.AddBidHistory(request);
            await Clients.Group(groupName).SendAsync("ReceiveBid", bid, userEmail, date);

        }


        private object ParsedRemainingTime(string time)
        {
            var parts = time.Split(new[] {'.',':'},StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 5) throw new FormatException("Invalid remaining time format");
            var days = int.Parse(parts[0]);
            var hours = int.Parse(parts[1]);
            var minutes = int.Parse(parts[2]);
            var seconds = int.Parse(parts[3]);
            var milliseconds = int.Parse(parts[4].PadRight(3, '0')); // Pad to milliseconds

            return new
            {
                days,
                hours,
                minutes,
                seconds,
                milliseconds
            };
        }
    }
}