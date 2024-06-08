using AuctionAce.Api.Models;
using AuctionAce.Api.Models.Auctions;
using AuctionAce.Application.Services;
using AuctionAce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuctionAce.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly AuctionService _auctionService;

        public HomeController(UserRepository userRepository, AuctionService auctionService)
        {
            _userRepository = userRepository;
            _auctionService = auctionService;
        }







        //get
        public IActionResult Index()
        {
            var cookie = Request.Cookies["Id"];
            var model = new HomeViewModel();
            var auctions = _auctionService.GetAuctionsAsync().Result;
            if (cookie != null) 
            {
                int idUser = Int32.Parse(cookie);
                var isLogged = _userRepository.isLoginedAsync(idUser).Result;
                if (isLogged)
                {
                    var user = _userRepository.GetUserByIdAsync(idUser).Result;
                    model.User = user;
                    model.Auctions = auctions;
                    return View(model);
                }
            }

            
            model.Auctions = auctions;

        return View(model);
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
