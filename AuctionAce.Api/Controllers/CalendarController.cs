using AuctionAce.Api.Controllers.Helpers;
using AuctionAce.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuctionAce.Api.Controllers
{
    public class CalendarController : Controller
    {
        private readonly CalendarService _calendarService;

        public CalendarController(CalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        public IActionResult GetCalendarData()
        {
            var userId = SessionHelper.GetUserIdFromSession(HttpContext);
            var data = _calendarService.GetCalendarData(userId).Result;
            ViewBag.EventsJson = JsonConvert.SerializeObject(data);

            return View();
        }
    }
}