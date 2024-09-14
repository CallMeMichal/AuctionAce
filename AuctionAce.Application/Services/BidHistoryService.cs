using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.Repositories;

namespace AuctionAce.Application.Services
{
    public class BidHistoryService
    {
        public readonly BidHistoryRepostiory _bidRepostiory;

        public BidHistoryService(BidHistoryRepostiory bidRepostiory)
        {
            _bidRepostiory = bidRepostiory;
        }

        public async Task AddBidHistory(BidHistoryDomain request)
        {
            var data = new BidHistory()
            {
                Date = request.DateTime,
                UserEmail = request.UserEmail,
                Price = request.Price,
                IdUsers = request.UserId,
                IdAuctionItems = request.AuctionItemId
            };


            await _bidRepostiory.AddBidHistory(data);
        }

        public async Task<List<BidHistoryDomain>> GetBidHistory(int auctionItemId)
        {
            var bidHistories = await _bidRepostiory.GetBidHistory(auctionItemId);

            List<BidHistoryDomain> bidHistoryDomains = new List<BidHistoryDomain>();

            foreach (var bidHistoryDomain in bidHistories)
            {
                bidHistoryDomains.Add(new BidHistoryDomain
                {
                    DateTime = bidHistoryDomain.Date,
                    Price = bidHistoryDomain.Price,
                    UserEmail = bidHistoryDomain.UserEmail,
                });
            }

            return bidHistoryDomains;
        }

        public async Task<int> GetHighestBidForItem(int itemId)
        {
            var highestBid = await _bidRepostiory.GetHighestBidForItem(itemId);
            
            return highestBid;
        }

        public async Task<int> GetHighestBidForItemAndSave(int itemId)
        {
            var highestBid = await _bidRepostiory.GetHighestBidForItemAndSave(itemId);

            return highestBid;
        }


    }
}
