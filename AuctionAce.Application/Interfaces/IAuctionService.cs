using AuctionAce.Domain.Entities;

namespace AuctionAce.Application.Interfaces
{
    public interface IAuctionService
    {
        Task<List<AuctionListDomain>> GetAuctionsAsync();
        Task<bool> AddAuctionAsync(int categoryId, string auctionName, string auctionDescription, DateTime startDate, DateTime endDate, int auctionerId, Dictionary<string, string> auctionImagePaths, List<AuctionItemsDomain> itemsInfo);
        Task<double> GetRemainingTimeForAuction(int auctionId);
    }
}
