﻿

using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.Repositories;
using Azure;
using Azure.Core;
using System.Globalization;

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

        public async  Task<User> UserLogin(string email, string password)
        {
            var userLoginStatus = await _userRepository.UserLoginAsync(email, password);
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


