using AuctionAce.Api.Models.DTO.Login;
using AuctionAce.Api.Models.ViewModels.Home;
using AuctionAce.Application.Services;
using AuctionAce.Infrastructure.JwtAuthentication;
using AuctionAce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AuctionAce.Api.Controllers
{
    public class UserController : Controller
    {
        //private readonly IUserService _userService;
        private readonly UserService _userService;

        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public UserController(UserService userService, JwtTokenGenerator jwtTokenGenerator)
        {
            _userService = userService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        //działa
        [HttpPost]
        public async Task<IActionResult> LoginAction(LoginRequest loginRequest)
        {
            var user = await _userService.UserLogin(loginRequest.Email, loginRequest.Password);

            var token = _jwtTokenGenerator.GenerateToken(user);

            /*HttpContext.Session.SetString("UserID", user.Id.ToString());
            HttpContext.Session.SetString("Token", user..ToString());*/

            HomeViewModel model = new HomeViewModel();

            model.User = user;
            model.Token = token;

            if (user != null)
            {
                return View(model);
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
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }
    }
}