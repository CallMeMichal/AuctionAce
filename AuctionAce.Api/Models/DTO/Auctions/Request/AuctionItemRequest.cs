namespace AuctionAce.Api.Models.DTO.Auctions.Request
{
    public class AuctionItemRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? IdAuctions { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }
        public string? StartingPrice { get; set; }

        public string? BuyNowPrice { get; set; }
        public bool? NewUsed { get; set; }
        public List<IFormFile> ItemImages { get; set; }
    }
}