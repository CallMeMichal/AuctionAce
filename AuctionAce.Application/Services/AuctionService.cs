using AuctionAce.Api;
using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Repositories;

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
                    /*ImagePath = auction.ImagePath ?? string.Empty,*/
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
                        /*ImagePath = item.ImagePath ?? string.Empty,*/
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
                    /*ImagePath = auction.ImagePath ?? string.Empty,*/
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
                        /*ImagePath = item.ImagePath ?? string.Empty,*/
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

        public async Task<bool> AddAuctionAsync(string auctionName, string auctionDescription, DateTime startDate, DateTime endDate, int auctionerId, Dictionary<string, string> auctionImagePaths, List<AuctionItemsDomain> itemsInfo)
        {
            Auction auction = new Auction()
            {
                AuctionName = auctionName,
                Description = auctionDescription,
                StartDate = startDate,
                EndDate = endDate,
                IdUsers = auctionerId,
            };
            var auctionId = await _auctionRespository.AddAuctionAsync(auction);
            var itemsImagePath = new Dictionary<string, string>();
            foreach (var item in itemsInfo)
            {
                foreach (var imagePath in item.ItemImagePaths)
                {
                    itemsImagePath.Add(imagePath.Key, imagePath.Value);
                }
            }

            var auctionImagePath = await _auctionRespository.AddAuctionItemPhoto(auctionImagePaths, auctionId);
            var itemImagePath = await _auctionRespository.AddAuctionItemPhoto(itemsImagePath, auctionId);

            if (auctionImagePath == true && itemImagePath == true)
            {
                return true;
            }
            else
            {
                return false;
            }
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