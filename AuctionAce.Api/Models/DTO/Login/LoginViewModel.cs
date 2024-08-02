using AuctionAce.Infrastructure.Data.Models;

namespace AuctionAce.Api.Models.DTO.Login
{
    public class LoginViewModel
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}