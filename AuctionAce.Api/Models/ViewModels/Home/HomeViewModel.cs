using AuctionAce.Api.Models.ViewModels.Base;

namespace AuctionAce.Api.Models.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        public List<Auction>? Auctions { get; set; }
    }
}