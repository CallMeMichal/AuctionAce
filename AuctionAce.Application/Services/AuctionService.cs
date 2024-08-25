using AuctionAce.Infrastructure.Data;
using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Repositories;
using AuctionAce.Infrastructure;
using AuctionAce.Infrastructure.Data.Models;

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
                        StartingPrice = item.StartingPrice ?? string.Empty,
                        BuyNowPrice = item.BuyNowPrice ?? string.Empty,
                        Amount = item.Amount ?? string.Empty,
                        NewUsed = item.NewUsed,
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
                        StartingPrice = item.StartingPrice ?? string.Empty,
                        BuyNowPrice = item.BuyNowPrice ?? string.Empty,
                        Amount = item.Amount,
                        NewUsed = item.NewUsed ,
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
            // Tworzenie rekordu dla aukcji
            Auction auction = new Auction()
            {
                AuctionName = auctionName,
                Description = auctionDescription,
                StartDate = startDate,
                EndDate = endDate,
                IdUsers = auctionerId,
            };

            // Zapis aukcji i uzyskanie jej ID
            var auctionId = await _auctionRespository.AddAuctionAsync(auction);

            // Dodanie zdjęć do aukcji
            var auctionImageResult = await _auctionRespository.AddAuctionItemPhoto(auctionImagePaths, auctionId, null);

            // Przetwarzanie zdjęć dla poszczególnych przedmiotów
            foreach (var item in itemsInfo)
            {
                var auctionItem = new AuctionItem
                {
                    Name = item.Name,
                    Description = item.Description,
                    StartingPrice = item.StartingPrice,
                    BuyNowPrice = item.BuyNowPrice,
                    NewUsed = item.NewUsed,
                    Guid = Helpers.Helpers.GetGuid(),
                    IdAuctions = auctionId
                };

                // Zapis przedmiotu i uzyskanie jego ID
                var auctionItemId = await _auctionRespository.AddAuctionItemAsync(auctionItem);

                // Dodanie zdjęć do przedmiotu
                var itemImageResult = await _auctionRespository.AddAuctionItemPhoto(item.ItemImagePaths, auctionId, auctionItemId);
            }

            return auctionImageResult;
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

        public async Task<bool> AddItemToAuction(List<AuctionItemsDomain> request, int idAuction)
        {
            var items = request.Select(r => new AuctionItem
            {
                Name = r.Name,
                Description = r.Description,
                StartingPrice = r.StartingPrice,
                BuyNowPrice = r.BuyNowPrice,
                NewUsed = r.NewUsed,
                Guid = Helpers.Helpers.GetGuid(),
                IdAuctions = idAuction
            }).ToList();
            var response = await _auctionRespository.AddItemsToAuction(items);

            if (response)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// pobieranie wszystkich itemów i wszystkich zdjec dla aukcji oraz itemu
        /// </summary>
        /// <param name="auctionId"></param>
        /// <returns></returns>
        public async Task<PhotosAuctionItemDomain> GetPhotos(int auctionId)
        {
            var auctionPhoto = await _auctionRespository.GetPhotosForAuction(auctionId);
            var itemPhoto = await _auctionRespository.GetPhotosForItems(auctionId);

            var photosDomain = Helpers.Helpers.ProcessPhotos(auctionPhoto, itemPhoto);
            return photosDomain;
        }

        /// <summary>
        /// pobieranie zdjec dla okreslonego itemId
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<List<PhotosItemDomain>> GetPhotosForOneItem(int itemId)
        {
            var itemPhoto = await _auctionRespository.GetPhotosForOneItem(itemId);
            var photosDomain = Helpers.Helpers.ProcessPhotos(itemPhoto);
            if (itemPhoto != null)
            {
                var itemImages = photosDomain.ItemImages[itemId];

                return new List<PhotosItemDomain>
                {
                    new PhotosItemDomain
                    {
                        Id = itemId,
                        ItemImageBase64 = itemImages.Select(image => image.ItemImageBase64).ToList()
                    }
                };
            }
            return null;
        }
    }
}