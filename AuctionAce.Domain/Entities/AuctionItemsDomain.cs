namespace AuctionAce.Domain.Entities
{
    public class AuctionItemsDomain
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public string StartingPrice { get; set; }
        public string BuyNowPrice { get; set; }
        public bool? NewUsed { get; set; }
        public Dictionary<string, string> ItemImagePaths { get; set; }
    }

}
