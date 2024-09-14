using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Repositories;

namespace AuctionAce.Application.Services
{
	public class UserBoughtItemsService
	{
		private readonly AuctionRespository _auctionRespository;
		private readonly UserBoughtItemsRepostiory _userBoughtItemsRepostiory;

		public UserBoughtItemsService(AuctionRespository auctionRespository, UserBoughtItemsRepostiory userBoughtItemsRepostiory)
		{
			_auctionRespository = auctionRespository;
			_userBoughtItemsRepostiory = userBoughtItemsRepostiory;
		}

		public async Task<bool> SetAuctionItemStatus(int itemId, string status)
		{
			var respone = false;

			if (status.Equals("active"))
			{
				respone = await _auctionRespository.UpdateAuctionItemStatus(itemId, 1);
			}
			else
			{
				respone = await _auctionRespository.UpdateAuctionItemStatus(itemId, 0);
			}

			return respone;
		}

		public async Task<bool> AddItemToBought(int itemId, int userId, int price)
		{
			var response = await _userBoughtItemsRepostiory.AddItemToUser(itemId, price, userId);

			return response;
		}

        public async Task<List<UserBoughtItemsDomain>> GetUserItems(int userId)
        {
            var data = await _userBoughtItemsRepostiory.GetUserBoughtItems(userId);
            string newUsed = "";
            if (data.Count > 0)
            {
                List<UserBoughtItemsDomain> userBoughtItemsDomains = new List<UserBoughtItemsDomain>();
                foreach (var item in data)
                {
                    var groupedPhotos = item.IdAuctionItemNavigation.AuctionsItemsPhotos
                        .GroupBy(photo => photo.AuctionItemId)
                        .Select(group => new UserBoughtItemsPhotosGroup
                        {
                            AuctionItemId = group.Key,
                            Photos = group.Select(photo => new UserBoughtItemsPhotoDomain
                            {
                                Id = photo.Id,
                                PhotoBase64 = Helpers.Helpers.GetImageDataBase64(photo.Path)
                            }).ToList()
                        }).ToList();

                    newUsed = item.IdAuctionItemNavigation.NewUsed == true ? "new" : "used";
                    userBoughtItemsDomains.Add(new UserBoughtItemsDomain
                    {
                        AuctionItemId = item.IdAuctionItemNavigation.Id,
                        YourPrize = item.Prize,
                        Category = item.IdAuctionItemNavigation.Category,
                        Description = item.IdAuctionItemNavigation.Description,
                        ItemName = item.IdAuctionItemNavigation.Name,
                        NewUsed = newUsed,
                        GroupedItemPhotos = groupedPhotos,
                    });
                }
                return userBoughtItemsDomains;
            }
            return new List<UserBoughtItemsDomain>();
        }

       



    }
}
