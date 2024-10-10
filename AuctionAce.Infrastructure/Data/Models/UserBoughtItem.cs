namespace AuctionAce.Infrastructure.Data.Models;

public partial class UserBoughtItem
{
    public int Id { get; set; }

    public int? IdAuctionItem { get; set; }

    public int? IdUser { get; set; }

    public int? Prize { get; set; }

    public virtual AuctionItem? IdAuctionItemNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
