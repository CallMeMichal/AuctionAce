﻿
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
            await Clients.Group(groupName).SendAsync("ReceiveRemainingAuctionTime", remainingTime);
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

        public async Task SetActiveItemsInAuction(int auctionId)
        {
           await _auctionService.SetActiveItemsInAuction(auctionId);
        }

        public async Task SetInactiveItemsInAuctionWithoutBids(int auctionId)
        {
            await _auctionService.SetInactiveItemsInAuctionWithoutBids(auctionId);
        }

        public async Task GetHighestBid(string auctionItemId,string groupName)
        {

            var highestBid = await _bidHistoryService.GetHighestBidForItemAndSave(Convert.ToInt32(auctionItemId));
            var auctionId = await _auctionService.GetAuctionIdByItemId(Convert.ToInt32(auctionItemId));
            await _auctionService.CloseAuction(auctionId);
            await Clients.Group(groupName).SendAsync("HighestBid", highestBid);
        }
        
        
    }
}