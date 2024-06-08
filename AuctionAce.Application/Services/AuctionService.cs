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

            if (auctions == null)
            {
                return auctions;
            }

            return null;
        }

        public async Task AddAuctionAsync(string auctionName, string description, string startDate, string endDate)
        {
            Auction auction = new Auction();
            auction.AuctionName = auctionName;
            auction.IdCategory = 1;
            auction.StartDate = DateTime.Now;
            auction.IdStatus = 1;
            auction.IdPayments = 1;
            auction.IdUsers = 3;

            await _auctionRespository.AddAuctionAsync(auction);
        }

    }
}
