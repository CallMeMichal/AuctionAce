using System;
using System.Collections.Generic;

namespace AuctionAce.Api;

public partial class AuctionItem
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdAuctions { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    public string? Photo { get; set; }

    public string? StartingPrice { get; set; }

    public string? BuyNowPrice { get; set; }

    public string? Amount { get; set; }

    public bool? NewUsed { get; set; }

    public int? IdStatus { get; set; }

    public virtual Auction? IdAuctionsNavigation { get; set; }
}
