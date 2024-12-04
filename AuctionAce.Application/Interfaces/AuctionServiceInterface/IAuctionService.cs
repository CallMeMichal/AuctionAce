using AuctionAce.Domain.Entities;

namespace AuctionAce.Application.Interfaces.AuctionServiceInterface
{
    public interface IAuctionService
    {

        Task<List<AuctionListDomain>> GetAuctionsAsync();

    }
}
