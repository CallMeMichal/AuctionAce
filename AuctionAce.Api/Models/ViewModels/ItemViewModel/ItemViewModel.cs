using AuctionAce.Domain.Entities;

namespace AuctionAce.Api.Models.ViewModels.ItemViewModel
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Message { get; set; }
        public int StartPrice { get; set; }
        public int BuyNowPrice { get; set; }
        public int ActualHighestPrice { get; set; }
        public List<PhotosItemDomain> ItemDomain { get; set; }
    }
}