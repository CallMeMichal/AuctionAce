namespace AuctionAce.Api.Models.DTO.Auctions.Request
{
    public class AddAuctionRequest
    {
        public List<IFormFile> AuctionImages { get; set; }
        public string? AuctionName { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AuctionItemRequest> Items { get; set; }

    }
}