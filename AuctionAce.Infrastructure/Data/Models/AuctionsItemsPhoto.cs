using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class AuctionsItemsPhoto
{
    public int Id { get; set; }

    public int? AuctionItemId { get; set; }

    public int? AuctionsId { get; set; }

    public string? Path { get; set; }

    public string? FileName { get; set; }

    public virtual AuctionItem? AuctionItem { get; set; }

    public virtual Auction? Auctions { get; set; }
}
