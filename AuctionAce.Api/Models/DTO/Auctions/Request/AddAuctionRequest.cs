using AuctionAce.Infrastructure.Data.Models;
namespace AuctionAce.Api.Models.DTO.Auctions.Request
{
    public class AddAuctionRequest
    {
        public int AuctionerId { get; set; }
        public string? AuctionPhoto { get; set; }
        public string? AuctionName { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AuctionItem> Items { get; set; }
        public bool Success { get; set; }
    }
}
