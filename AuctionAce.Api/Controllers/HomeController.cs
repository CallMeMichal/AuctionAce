using AuctionAce.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuctionAce.Api.Controllers
{
    public class HomeController : Controller
    {
        //get
        public IActionResult Index()
        {
            return View();
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
