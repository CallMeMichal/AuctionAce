namespace AuctionAce.Api.Models.DTO.Auctions.Request
{
    public class AuctionItems
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemCategory { get; set; }
        public string ItemPhoto { get; set; }
        public decimal StartPrice { get; set; }
        public decimal BuyNowPrice { get; set; }
        public string Condition { get; set; }
    }
}
