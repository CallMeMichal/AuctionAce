namespace AuctionAce.Api.Models.ViewModels.UserBoughtItems
{
    public class UserBoughtItemsModel
    {
        public int? YourPrize { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public string NewUsed { get; set; }
        public string Description { get; set; }
        public List<UserBoughtItemsPhotosGroupModel> ItemPhotos { get; set; }
    }

    public class UserBoughtItemsPhotoModel
    {
        public int Id { get; set; }
        public string PhotoBase64 { get; set; }
    }

    public class UserBoughtItemsPhotosGroupModel
    {
        public int? AuctionItemId { get; set; }
        public List<UserBoughtItemsPhotoModel> Photos { get; set; }
    }
}
