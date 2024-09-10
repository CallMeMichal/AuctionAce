using AuctionAce.Infrastructure.Repositories;

namespace AuctionAce.Application.Services
{
    public class BoughtItemsService
    {
        private readonly AuctionRespository _auctionRespository;

        public BoughtItemsService(AuctionRespository auctionRespository)
        {
            _auctionRespository = auctionRespository;
        }

        public async Task<bool> SetAuctionItemStatus(string guid, string status)
        {
            var respone = false;

            if (status.Equals("active"))
            {
                respone = await _auctionRespository.UpdateAuctionItemStatus(guid, 1);
            }
            else
            {
                respone = await _auctionRespository.UpdateAuctionItemStatus(guid, 0);
            }

            return respone;
        }
    }
}
