using AuctionAce.Api.Models;
using AuctionAce.Api.Models.Login;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuctionAce.Api.Controllers
{
    public class HomeController : Controller
    {


        //get
        public IActionResult Index()
        {
            //var loginStatus = await _userService.UserLogin(email, password);

            LoginResult loginResult = new LoginResult();
            loginResult.Role = "auctioner";

            return View(loginResult);
        }

       


        [HttpGet]
        public IActionResult AllAuctions()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
