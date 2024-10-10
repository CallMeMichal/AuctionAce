namespace AuctionAce.Infrastructure.Data.Models;

public partial class BidHistory
{
    public int Id { get; set; }

    public decimal? Price { get; set; }

    public bool? IsWin { get; set; }

    public int? IdAuctionItems { get; set; }

    public int? IdUsers { get; set; }

    public DateTime? Date { get; set; }

    public string? UserEmail { get; set; }

    public virtual AuctionItem? IdAuctionItemsNavigation { get; set; }

    public virtual User? IdUsersNavigation { get; set; }
}
