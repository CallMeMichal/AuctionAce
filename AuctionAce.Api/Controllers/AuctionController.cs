using AuctionAce.Api.Models.DTO.Auctions.Request;
using AuctionAce.Api.Models.DTO.Auctions.Response;
using AuctionAce.Api.Models.ViewModels.Auctions;
using AuctionAce.Api.Models.ViewModels.Home;
using AuctionAce.Application.Services;
using AuctionAce.Infrastructure.Data.Models;
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

        public async Task<IActionResult> Index()
        {
            var model = new AuctionViewModel();
            var cookie = Request.Cookies["Id"];

            if (cookie != null && int.TryParse(cookie, out int idUser))
            {
                var isLogged = await _userRepository.isLoginedAsync(idUser);
                if (isLogged)
                {
                    var auctions = await _auctionService.GetAuctionsByIdUserAsync(idUser);
                    var user = _userRepository.GetUserByIdAsync(idUser).Result;
                    model.User = user;
                    model.Auctions = auctions;
                }
            }

            return View(model);
        }

        [HttpGet] 
        public async Task<IActionResult> AddAuction()
        {
            var cookie = Request.Cookies["Id"];
            var model = new HomeViewModel();

            if (cookie != null)
            {
                int idUser = Int32.Parse(cookie);
                var isLogged = await _userRepository.isLoginedAsync(idUser);
                if (isLogged)
                {
                    var user = await _userRepository.GetUserByIdAsync(idUser);
                    model.User = user;
                    return View(model);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<AuctionResponse> AddAuction(AddAuctionRequest request)
        {
            AuctionResponse response = new AuctionResponse();
            var cookie = Request.Cookies["Id"];
            int idUser = Int32.Parse(cookie);
            response.Success = await _auctionService.AddAuctionAsync(request.AuctionName, request.Description, request.StartDate, request.EndDate,idUser);
            return response;
        }

        /*[HttpGet]
        public async Task<IActionResult> MyAuctions()
        {
            AuctionViewModel model = new AuctionViewModel();
            var cookie = Request.Cookies["Id"];

            if (cookie != null)
            {
                int idUser = Int32.Parse(cookie);                
                var isLogged = await _userRepository.isLoginedAsync(idUser);// do poprawy
                if (isLogged)
                {
                    List<Auction> auctions = await _auctionService.GetAuctionsByIdUserAsync(idUser);
                    model.Auctions = auctions;
                }
            }

            return View(model);



        }*/
    }
}
