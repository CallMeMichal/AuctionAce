using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.Repositories;

namespace AuctionAce.Application.Services
{
    public class LoginService
    {
        public readonly UserRepository _userRepository;

        public LoginService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> UserLogin(int idUser)
        {
            User user = new User();

            if (idUser != 0)
            {
                var isLogged = await _userRepository.isLoginedAsync(idUser);
                if (isLogged)
                {
                    user = await _userRepository.GetUserByIdAsync(idUser);
                }
            }
            return user;
        }
    }
}