using AuctionAce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

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

        public async Task<bool> AddItemToBought(int itemId,int userId,int price)
        {
            var response = await _userBoughtItemsRepostiory.AddItemToUser(itemId, price, userId);
        
            return response;
        }

        public async Task GetUserItems(int userId)
        {
            await _userBoughtItemsRepostiory.GetUserBoughtItems(userId);
        }

       
    }
}
