namespace AuctionAce.Domain.Entities
{
    public class PhotosAuctionItemDomain
    {
        public List<AuctionImage> AuctionImages { get; set; }
        public Dictionary<int, List<ItemImages>> ItemImages { get; set; }
    }

    public class AuctionImage
    {
        public string AuctionImageBase64 { get; set; }
    }

    public class ItemImages
    {
        public int Id { get; set; }
        public string ItemImageBase64 { get; set; }
    }
}