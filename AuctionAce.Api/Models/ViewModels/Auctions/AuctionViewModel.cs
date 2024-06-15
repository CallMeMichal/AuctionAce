using AuctionAce.Api.Models.ViewModels.Base;
using AuctionAce.Infrastructure.Data.Models;

namespace AuctionAce.Api.Models.ViewModels.Auctions
{
    public class AuctionViewModel : BaseViewModel
    {
        public List<Auction>? Auctions { get; set; }
        public List<AuctionItem>?  AuctionItems { get; set; }
        public Auction? Auction { get; set; }

    }
}
