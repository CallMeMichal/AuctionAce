using AuctionAce.Api.Controllers.Helpers;
using AuctionAce.Api.Models.DTO.Login;
using AuctionAce.Api.Models.ViewModels.Home;
using AuctionAce.Application.Middleware;
using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly AuthenticationService _authenticationService;

        public UserController(UserService userService, AuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
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
        public IActionResult RegisterAction(string email, string password, string confirmPassword, int role)
        {
            Console.WriteLine(email + ":" + password);
            return View();
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

            var logoutUser = _userService.UserLogout(userId).Result;

            if (logoutUser == true)
            {
                HttpContext.Session.Clear();
                return Json(new { success = true, message = "Logout successful" });
            }
            return Json(new { success = false, message = "Logout unsuccessful" });
        }
    }
}