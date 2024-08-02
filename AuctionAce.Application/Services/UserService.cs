using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.JwtAuthentication;
using AuctionAce.Infrastructure.Repositories;
using Azure;
using Azure.Core;
using System.Globalization;

namespace AuctionAce.Application.Services
{
    public class UserService
    {
        public readonly UserRepository _userRepository;
        public readonly JwtTokenGenerator _jwtTokenGenerator;

        public UserService(UserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<User> UserLogin(string email, string password)
        {
            var userLoginStatus = await _userRepository.UserLoginAsync(email, password);
            var token = _jwtTokenGenerator.GenerateToken(userLoginStatus);
            return userLoginStatus;
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