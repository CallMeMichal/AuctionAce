using AuctionAce.Infrastructure.Data;

namespace AuctionAce.Api.Models.ViewModels.Base
{
    public class BaseViewModel
    {
        public User? User { get; set; }
        public string Token { get; set; }
    }
}