using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.Repositories;

namespace AuctionAce.Application.Services
{
	public class AuctionService
	{
		public readonly AuctionRespository _auctionRespository;
		public readonly CategoryService _categoryService;

        public AuctionService(AuctionRespository auctionRespository, CategoryService categoryService)
        {
            _auctionRespository = auctionRespository;
            _categoryService = categoryService;
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
				var open = auction.AuctionItems.Count(x => x.IdStatus == 1);
				var closed = auction.AuctionItems.Count(x => x.IdStatus == 0);
				if (auction.StartDate.HasValue && auction.EndDate.HasValue)
				{
					if (today < auction.StartDate.Value && closed > 0)
					{
						status = "Pending";
					}
					else if (today >= auction.StartDate.Value && today <= auction.EndDate.Value && open >= 1)
					{
						status = "Active";
					}
					else if (open < 1)
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
						StartingPrice = item.StartingPrice ?? string.Empty,
						BuyNowPrice = item.BuyNowPrice ?? string.Empty,
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
				var open = auction.AuctionItems.Count(x => x.IdStatus == 1);
				var closed = auction.AuctionItems.Count(x => x.IdStatus == 0);
				if (auction.StartDate.HasValue && auction.EndDate.HasValue)
				{
					if (today < auction.StartDate.Value && closed > 0)
					{
						status = "Pending";
					}
					else if (today >= auction.StartDate.Value && today <= auction.EndDate.Value && open >= 1)
					{
						status = "Active";
					}
					else if (open < 1)
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
						StartingPrice = item.StartingPrice ?? string.Empty,
						BuyNowPrice = item.BuyNowPrice ?? string.Empty,
						NewUsed = item.NewUsed,
					}).ToList()
				});
			}
			return auctionList;
		}

        public async Task<List<AuctionListDomain>> GetAllAuctionsAsync()
        {
            var auctions = await _auctionRespository.GetAuctionsAsync();
            var auctionsItem = await _auctionRespository.GetAuctionsItemsAsync();
			
            List<AuctionListDomain> auctionList = new List<AuctionListDomain>();

            var today = DateTime.Now;

            foreach (var auction in auctions)
            {
                var status = "Pending";
                var open = auction.AuctionItems.Count(x => x.IdStatus == 1);
                var closed = auction.AuctionItems.Count(x => x.IdStatus == 0);
                if (auction.StartDate.HasValue && auction.EndDate.HasValue)
                {
                    if (today < auction.StartDate.Value && closed > 0)
                    {
                        status = "Pending";
                    }
                    else if (today >= auction.StartDate.Value && today <= auction.EndDate.Value && open >= 1)
                    {
                        status = "Active";
                    }
                    else if (open < 1)
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
                        //Category = item.IdCategory ?? string.Empty,
                        StartingPrice = item.StartingPrice ?? string.Empty,
                        BuyNowPrice = item.BuyNowPrice ?? string.Empty,
                        NewUsed = item.NewUsed,
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

		public async Task<bool> AddAuctionAsync(int categoryId,string auctionName, string auctionDescription, DateTime startDate, DateTime endDate, int auctionerId, Dictionary<string, string> auctionImagePaths, List<AuctionItemsDomain> itemsInfo)
		{
			var status = 0;
			// Tworzenie rekordu dla aukcji
			Auction auction = new Auction()
			{
				AuctionName = auctionName,
				Description = auctionDescription,
				StartDate = startDate,
				EndDate = endDate,
				IdUsers = auctionerId,
				IdCategory= categoryId,

            };
			DateTime dateNow = DateTime.Now;
			if(dateNow>startDate && dateNow < endDate)
			{
			     status = 1;
			}
			else
			{
				status = 0;
			}
			// Zapis aukcji i uzyskanie jej ID
			var auctionId = await _auctionRespository.AddAuctionAsync(auction);

			// Dodanie zdjęć do aukcji
			var auctionImageResult = await _auctionRespository.AddAuctionItemPhoto(auctionImagePaths, auctionId, null);

			// Przetwarzanie zdjęć dla poszczególnych przedmiotów
			foreach (var item in itemsInfo)
			{
				/*var categoryId = await _categoryService.GetCategoryIdByName(item.Category);*/

                var auctionItem = new AuctionItem
				{
					Name = item.Name,
					Description = item.Description,
					StartingPrice = item.StartingPrice,
					BuyNowPrice = item.BuyNowPrice,
					NewUsed = item.NewUsed,
					/*IdCategory = categoryId,*/
					Guid = Helpers.Helpers.GetGuid(),
					IdAuctions = auctionId,
					IdStatus = status,
					IsBought = false,
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
						ItemImageBase64 = itemImages.Select(image => image.ItemImageBase64).ToList(),
					}
				};
			}
			return null;
		}

		public async Task<double> GetRemainingTimeForAuction(int auctionId)
		{
			var remainingTime = await _auctionRespository.GetTimeForAuction(auctionId);
            var remainingTimeMilliseconds = remainingTime.TotalMilliseconds;
			return remainingTimeMilliseconds;
        }

		public async Task<int> GetAuctionIdByItemId(int itemId)
		{
			return await _auctionRespository.GetAutionId(itemId);
		}

		public async Task<int> GetStartPriceForItem(int itemId)
		{
			return await _auctionRespository.GetStartPriceForItem(itemId);
		}
		public async Task<int> GetBuyNowPriceForItem(int itemId)
		{
			return await _auctionRespository.GetBuyNowPriceForItem(itemId);
		}

		public async Task SetActiveItemsInAuction(int auctionId)
		{
			await _auctionRespository.SetActiveItemsInAuction(auctionId);
		}
		public async Task SetInactiveItemsInAuctionWithoutBids(int auctionId)
		{
			await _auctionRespository.SetInactiveItemsInAuctionWithoutBids(auctionId);
		}

		public async Task CloseAuction(int auctionId)
		{
			await _auctionRespository.CloseAuction(auctionId);
		}

    }
}