using AuctionAce.Api.Models.Login;
using AuctionAce.Application.Interfaces;
using AuctionAce.Application.Services;
using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Web;

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
        public  IActionResult LoginAction(LoginRequest loginRequest)
        {
            var user =  _userService.UserLogin(loginRequest.Email, loginRequest.Password);
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
