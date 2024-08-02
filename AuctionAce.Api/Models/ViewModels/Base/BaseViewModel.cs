using AuctionAce.Infrastructure.Data.Models;

namespace AuctionAce.Api.Models.ViewModels.Base
{
    public class BaseViewModel
    {
        public User? User { get; set; }
        public string Token { get; set; }
    }
}
