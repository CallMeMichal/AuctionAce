﻿using AuctionAce.Application.Interfaces;
using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.Repositories;

namespace AuctionAce.Application.Services
{
    public class UserService : IUserService
    {
        public readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> UserLogin(string email, string password)
        {
            var userLoginStatus = await _userRepository.UserLoginAsync(email, password);
            return userLoginStatus;
        }

        /*public async Task<bool> UserLogout(int idUser)
        {
            var logoutUser = await _userRepository.LogoutUserAsync(idUser);
            if (logoutUser == true)
            {
                return true;
            }

            return false;
        }*/

        public async Task<bool> UserRegister(User user)
        {
            var response = await _userRepository.CreateUser(user);
            return response;
        }
    }
}