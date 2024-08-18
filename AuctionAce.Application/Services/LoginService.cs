using AuctionAce.Api;
using AuctionAce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity.Data;

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

        public async Task<bool> UserLogout(int idUser)
        {
            var logoutUser = await _userRepository.LogoutUserAsync(idUser);
            if (logoutUser == true)
            {
                return true;
            }

            return false;
        }
    }
}