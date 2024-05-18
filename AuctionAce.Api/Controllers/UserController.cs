using AuctionAce.Application.Interfaces;
using AuctionAce.Application.Services;
using AuctionAce.Infrastructure.Repositories;
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
        public IActionResult LoginAction(string email, string password)
        {
            var loginStatus = _userService.UserLogin(email, password);

            //service
            //userRepository
            /*UserRepository userRepository = new();*/

            Console.WriteLine(email + ":" + password);
            return View();
        }

        //działa
        [HttpPost]
        public IActionResult RegisterAction(string email, string password, string confirmPassword, int role)
        {
            Console.WriteLine(email + ":" + password);
            return View();
        }
    }
}
