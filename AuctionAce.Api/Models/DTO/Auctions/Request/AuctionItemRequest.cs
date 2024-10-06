namespace AuctionAce.Api.Models.DTO.Auctions.Request
{
    public class AuctionItemRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Category { get; set; }
        public string? StartingPrice { get; set; }

        public string? BuyNowPrice { get; set; }
        public bool NewUsed { get; set; }
        public List<IFormFile> ItemImages { get; set; }

        public Dictionary<string, string> ItemImagePaths { get; set; }
    }
}