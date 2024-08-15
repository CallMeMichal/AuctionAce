using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Repositories;

namespace AuctionAce.Application.Services
{
    public class CalendarService
    {
        public readonly AuctionRespository _auctionRespository;

        public CalendarService(AuctionRespository auctionRespository)
        {
            _auctionRespository = auctionRespository;
        }

        public async  Task<List<AuctionCalendarData>> GetCalendarData(int userId)
        {
            var currentDate = DateTime.Now;
            var auctions = await _auctionRespository.GetAuctionsByIdUserAsync(userId);

            // Przekształć aukcje na obiekty CalendarEvent
            var calendarData = auctions.Select(a => new AuctionCalendarData
            {
                Title = a.AuctionName,
                Start = a.StartDate.HasValue ? a.StartDate.Value.ToString("yyyy-MM-ddTHH:mm:ss") : null,
                End = a.EndDate.HasValue ? a.EndDate.Value.ToString("yyyy-MM-ddTHH:mm:ss") : null,
                Color = a.EndDate.HasValue && a.EndDate.Value < currentDate ? "red"
                        : a.StartDate.HasValue && a.StartDate.Value > currentDate ? "gray"
                        : "green",
            }).ToList();

            return calendarData;
        }
    }
}
