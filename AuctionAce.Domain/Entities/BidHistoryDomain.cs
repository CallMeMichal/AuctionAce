namespace AuctionAce.Domain.Entities
{
    public class BidHistoryDomain
    {
        public decimal? Price { get; set; }
        public bool? IsWin { get; set; }
        public DateTime? DateTime { get; set; }
        public int? UserId { get; set; }
        public int? AuctionItemId { get; set; }
        public string? UserEmail { get; set; }
    }
}
