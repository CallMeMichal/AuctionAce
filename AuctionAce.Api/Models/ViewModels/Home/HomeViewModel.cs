using AuctionAce.Api.Models.ViewModels.Base;
using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Data.Models;

namespace AuctionAce.Api.Models.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        public List<Auction>? Auctions { get; set; }
        public List<AuctionListDomain> AuctionStatus { get; set; }
    }
}