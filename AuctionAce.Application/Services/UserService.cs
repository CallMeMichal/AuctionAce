

using AuctionAce.Infrastructure.Repositories;

namespace AuctionAce.Application.Services
{
    public class UserService
    {
        /* public UserService() { }*/

        public readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> UserLogin(string email, string password)
        {
            var userLoginStatus = await _userRepository.UserLogin(email, password);

            if (userLoginStatus != null)
                return "User";
            else if (userLoginStatus != null)
                return "Auctioner";
            else
                return "unknow";
        }
    }
}


