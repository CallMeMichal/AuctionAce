using AuctionAce.Api.Controllers.Helpers;
using AuctionAce.Api.Models.DTO.Login;
using AuctionAce.Api.Models.DTO.Register;
using AuctionAce.Api.Models.ViewModels.Home;
using AuctionAce.Application.Middleware;
using AuctionAce.Application.Services;
using AuctionAce.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly AuthenticationService _authenticationService;
        private readonly LoginService _loginService;

        public UserController(UserService userService, AuthenticationService authenticationService, LoginService loginService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult LoginAction(LoginRequest loginRequest)
        {
            var user = _userService.UserLogin(loginRequest.Email, loginRequest.Password).Result;

            HomeViewModel model = new HomeViewModel();

            if (user != null)
            {
                model.User = user;
                model.User.Id = user.Id;
                var role = user.IdRoles;
                var jwtToken = _authenticationService.GenerateJWTAuthentication(user.Email, role.ToString());

                Response.Cookies.Append("jwt", jwtToken, new CookieOptions
                {
                    HttpOnly = true,
                    // Secure = true, // Odkomentuj, jeśli aplikacja działa przez HTTPS
                });

                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserEmail", user.Email);

                return Json(new { success = true, message = "Login successful" });
            }
            else
            {
                return Json(new { success = false, message = "Invalid login credentials" });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RegisterAction(UserRegisterRequest request)
        {
            User user = new User()
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                IdRoles = request.IdRoles,
                Password = request.Password,
            };

            if (request.Password != request.PasswordConfirmation)
            {
                return Json(new { success = false, message = "Password are different" });
            }

            var result = _userService.UserRegister(user).Result;

            if (result == true)
            {
                var jwtToken = _authenticationService.GenerateJWTAuthentication(user.Email, request.IdRoles.ToString());
                Response.Cookies.Append("jwt", jwtToken, new CookieOptions
                {
                    HttpOnly = true,
                    // Secure = true, // Odkomentuj, jeśli aplikacja działa przez HTTPS
                });

                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserEmail", user.Email);
                return Json(new { success = true, message = "User create successful" });
            }
            else
            {
                return Json(new { success = false, message = "User create unsuccessful" });
            }
        }

        [JwtAuthentication("1", "2")]
        [HttpPost]
        public IActionResult LogoutAction()
        {
            if (Request.Cookies.ContainsKey("jwt"))
            {
                Response.Cookies.Delete("jwt");
            }

            var userId = SessionHelper.GetUserIdFromSession(HttpContext);

            var logoutUser = _loginService.UserLogout(userId).Result;

            if (logoutUser == true)
            {
                HttpContext.Session.Clear();
                return Json(new { success = true, message = "Logout successful" });
            }
            return Json(new { success = false, message = "Logout unsuccessful" });
        }
    }
}