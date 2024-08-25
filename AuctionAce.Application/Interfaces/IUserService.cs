namespace AuctionAce.Application.Interfaces
{
    public interface IUserService
    {
        public Task<string> UserLogin(string email, string password);
    }
}