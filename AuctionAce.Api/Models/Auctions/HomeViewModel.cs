using AuctionAce.Infrastructure.Data.Models;

namespace AuctionAce.Api.Models.Auctions
{
    public class HomeViewModel
    {
        public User User { get; set; }
        public List<Auction> Auctions { get; set;}
    }
}
