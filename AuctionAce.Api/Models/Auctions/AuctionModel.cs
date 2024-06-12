namespace AuctionAce.Api.Models.Auctions
{
    public class AuctionModel
    {
        public int AuctionerId { get; set; }

        public string AuctionPhoto { get; set; }
        public string AuctionName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
