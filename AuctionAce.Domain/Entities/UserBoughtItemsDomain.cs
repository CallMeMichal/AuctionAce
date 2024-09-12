namespace AuctionAce.Domain.Entities
{
	public class UserBoughtItemsDomain
	{
		public int? YourPrize { get; set; }
		public string ItemName { get; set; }
		public string Category { get; set; }
		public string NewUsed { get; set; }
		public string Description { get; set; }
		public Dictionary<int?, List<UserBoughtItemsPhotoDomain>> GroupedItemPhotos { get; set; }
	}
	public class UserBoughtItemsPhotoDomain
	{
		public int Id { get; set; }
		public string PhotoBase64 { get; set; }
	}
}
