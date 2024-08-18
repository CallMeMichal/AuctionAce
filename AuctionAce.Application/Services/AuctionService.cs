using AuctionAce.Api;
using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using System.Net.NetworkInformation;

namespace AuctionAce.Application.Services
{
    public class AuctionService
    {
        public readonly AuctionRespository _auctionRespository;

        public AuctionService(AuctionRespository auctionRespository)
        {
            _auctionRespository = auctionRespository;
        }

        public async Task<List<AuctionListDomain>> GetAuctionsAsync()
        {
            var auctions = await _auctionRespository.GetAuctionsAsync();
            var auctionsItem = await _auctionRespository.GetAuctionsItemsAsync();
            List<AuctionListDomain> auctionList = new List<AuctionListDomain>();

            var today = DateTime.Now;

            foreach (var auction in auctions)
            {
                var status = "Pending";

                if (auction.StartDate.HasValue && auction.EndDate.HasValue)
                {
                    if (today < auction.StartDate.Value.Date)
                    {
                        status = "Pending";
                    }
                    else if (today >= auction.StartDate.Value.Date && today <= auction.EndDate.Value.Date)
                    {
                        status = "Active";
                    }
                    else
                    {
                        status = "Inactive";
                    }
                }
                auctionList.Add(new AuctionListDomain
                {
                    Id = auction.Id,
                    Description = auction.Description ?? string.Empty,
                    ImagePath = auction.ImagePath ?? string.Empty,
                    AuctionName = auction.AuctionName ?? string.Empty,
                    IdUsers = auction.IdUsers ?? 0,
                    StartDate = auction.StartDate ?? DateTime.MinValue,
                    EndDate = auction.EndDate ?? DateTime.MinValue,
                    Status = status,
                    AuctionsListItem = auctionsItem.Where(x => x.IdAuctions == auction.Id).Select(item => new AuctionsItemList
                    {
                        Id = item.Id,
                        Name = item.Name ?? string.Empty,
                        Description = item.Description ?? string.Empty,
                        Category = item.Category ?? string.Empty,
                        ImagePath = item.ImagePath ?? string.Empty,
                        StartingPrice = item.StartingPrice ?? string.Empty,
                        BuyNowPrice = item.BuyNowPrice ?? string.Empty,
                        Amount = item.Amount ?? string.Empty,
                        NewUsed = item.NewUsed ?? false,
                    }).ToList()
                });
            }
            return auctionList;
        }

        public async Task<List<AuctionListDomain>> GetAuctionsAsync(int userId)
        {
            var auctions = await _auctionRespository.GetAuctionsAsync();
            var auctionsItem = await _auctionRespository.GetAuctionsItemsAsync();
            List<AuctionListDomain> auctionList = new List<AuctionListDomain>();

            var today = DateTime.Now;

            var userAuctions = auctions.Where(a => a.IdUsers == userId);

            foreach (var auction in userAuctions)
            {
                var status = "Pending";

                if (auction.StartDate.HasValue && auction.EndDate.HasValue)
                {
                    if (today < auction.StartDate.Value.Date)
                    {
                        status = "Pending";
                    }
                    else if (today >= auction.StartDate.Value.Date && today <= auction.EndDate.Value.Date)
                    {
                        status = "Active";
                    }
                    else
                    {
                        status = "Inactive";
                    }
                }

                auctionList.Add(new AuctionListDomain
                {
                    Id = auction.Id,
                    Description = auction.Description ?? string.Empty,
                    ImagePath = auction.ImagePath ?? string.Empty,
                    AuctionName = auction.AuctionName ?? string.Empty,
                    IdUsers = auction.IdUsers ?? 0,
                    StartDate = auction.StartDate ?? DateTime.MinValue,
                    EndDate = auction.EndDate ?? DateTime.MinValue,
                    Status = status,
                    AuctionsListItem = auctionsItem.Where(x => x.IdAuctions == auction.Id).Select(item => new AuctionsItemList
                    {
                        Id = item.Id,
                        Name = item.Name ?? string.Empty,
                        Description = item.Description ?? string.Empty,
                        Category = item.Category ?? string.Empty,
                        ImagePath = item.ImagePath ?? string.Empty,
                        StartingPrice = item.StartingPrice ?? string.Empty,
                        BuyNowPrice = item.BuyNowPrice ?? string.Empty,
                        Amount = item.Amount ?? string.Empty,
                        NewUsed = item.NewUsed ?? false,
                    }).ToList()
                });
            }
            return auctionList;
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

        public async Task<bool> AddAuctionAsync(string auctionName, string description, DateTime startDate, DateTime endDate, int auctionerId /*List<AuctionItem> items, IFormFile auctionImage, List<IFormFile> itemImages*/)
        {
            Auction auction = new Auction();
            auction.AuctionName = auctionName;
            auction.Description = description;
            auction.StartDate = startDate;
            auction.EndDate = endDate;
            auction.IdUsers = auctionerId;
            /*auction.AuctionItems = items;*/

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