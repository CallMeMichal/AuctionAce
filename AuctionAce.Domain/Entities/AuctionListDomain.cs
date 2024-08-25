namespace AuctionAce.Domain.Entities
{
    public class AuctionListDomain
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string AuctionName { get; set; }

        public int IdUsers { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public PhotosAuctionItemDomain AllAuctionsPhotos { get; set; }
        public List<AuctionsItemList> AuctionsListItem { get; set; }
    }

    public class AuctionsItemList
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int? IdAuctions { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? StartingPrice { get; set; }

        public string? BuyNowPrice { get; set; }

        public string? Amount { get; set; }

        public bool? NewUsed { get; set; }
    }
}