using AuctionAce.Infrastructure.Data.Models;

namespace AuctionAce.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> UserLogin(string email, string password);
        Task<bool> UserRegister(User user);
    }
}
