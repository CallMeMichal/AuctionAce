using AuctionAce.Api.Models.Auctions;
using AuctionAce.Application.Services;
using AuctionAce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAce.Api.Controllers
{
    public class AuctionController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly AuctionService _auctionService;

        public AuctionController(UserRepository userRepository, AuctionService auctionService)
        {
            _userRepository = userRepository;
            _auctionService = auctionService;
        }

        [HttpGet]
        public IActionResult AddAuction()
        {
            var cookie = Request.Cookies["Id"];
            var model = new HomeViewModel();

            if (cookie != null)
            {
                int idUser = Int32.Parse(cookie);
                var isLogged = _userRepository.isLoginedAsync(idUser).Result;
                if (isLogged)
                {
                    var user = _userRepository.GetUserByIdAsync(idUser).Result;
                    model.User = user;
                    return View(model);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddAuction(AuctionModel model)
        {
            _auctionService.AddAuctionAsync(model.auctionName, model.description, model.startDate, model.endDate);
            
            return View(AddAuction());
        }


    }
}
