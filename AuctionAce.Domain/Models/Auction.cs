using System;
using System.Collections.Generic;

namespace AuctionAce.Api;

public partial class Auction
{
    public int Id { get; set; }

    public string? AuctionName { get; set; }

    public int? IdUsers { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<AuctionItem> AuctionItems { get; set; } = new List<AuctionItem>();

    public virtual User? IdUsersNavigation { get; set; }
}
