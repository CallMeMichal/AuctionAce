using AuctionAce.Api.Controllers.Helpers;
using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuctionAce.Api.Controllers
{
    public class CalendarController : Controller
    {
        private readonly CalendarService _calendarService;

        public CalendarController(CalendarService calendarService)
        {
            _calendarService = calendarService;
        }
        [HttpGet]
        public IActionResult GetCalendarData()
        {
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            var data = _calendarService.GetCalendarData(userId).Result;
            ViewBag.EventsJson = JsonConvert.SerializeObject(data);

            return View();
        }
    }
}