using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.Repositories;
using System;
using System.Data;

namespace AuctionAce.Application.Services
{
    public class AuctionService
    {
        public readonly AuctionRespository _auctionRespository;

        public AuctionService(AuctionRespository auctionRespository)
        {
            _auctionRespository = auctionRespository;
        }

        public async Task<List<Auction>> GetAuctionsAsync()
        {
            var auctions = await _auctionRespository.GetAuctionsAsync();

            if (auctions != null)
            {
                return auctions;
            }

            return null;
        }

        public async Task<List<Auction>> GetAuctionsByIdUserAsync(int idUser)
        {
            var auctions = await _auctionRespository.GetAuctionsByIdUserAsync(idUser);

            if (auctions != null)
            {
                return auctions;
            }

            return null;
        }

        public async Task<bool> AddAuctionAsync(string auctionName, string description, DateTime startDate, DateTime endDate,int auctinerId,List<AuctionItem> items)
        {
            Auction auction = new Auction();
            auction.AuctionName = auctionName;
            auction.IdCategory = 1;
            auction.StartDate = startDate;
            auction.EndDate = endDate;
            auction.IdStatus = 1;
            auction.IdPayments = 1;
            auction.IdUsers = auctinerId;
            auction.IdData = 1;
            auction.AuctionItems = items;

            var response = await _auctionRespository.AddAuctionAsync(auction);
            return response;
        }

        
        public async Task<List<AuctionItem>> GetAuctionAsync(int auctionId)
        {
            var auctions = await _auctionRespository.GetAuctionAsync(auctionId);

            if (auctions != null)
            {
                return auctions;
            }
            return null;
        }
    }
}
