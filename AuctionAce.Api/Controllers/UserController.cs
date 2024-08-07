﻿using AuctionAce.Api.Models.DTO.Login;
using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class UserController : Controller
    {
        //private readonly IUserService _userService;
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        //działa
        [HttpPost]
        public  async Task<IActionResult> LoginAction(LoginRequest loginRequest)
        {
            var user =  await _userService.UserLogin(loginRequest.Email, loginRequest.Password);
            if (user != null)
            {
                return Json(new { type = "auctioner", Id = user.Id });
            }
            else
            {
                return Json(new { type = "error", message = "Invalid login credentials" });
            }
        }

        //działa
        [HttpPost]
        public IActionResult RegisterAction(string email, string password, string confirmPassword, int role)
        {
            Console.WriteLine(email + ":" + password);
            return View();
        }

        [HttpPost]
        public IActionResult LogoutAction()
        {
            var cookie = Request.Cookies["Id"];

            if (cookie != null)
            {
                int idUser = Int32.Parse(cookie);
                var logoutUser = _userService.UserLogout(idUser).Result;
                if (logoutUser == true)
                {
                    return Json(new { success = true});
                }
            }
            return Json(new { success = false });
        }
    }
}
