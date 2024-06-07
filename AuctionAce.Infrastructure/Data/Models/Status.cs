using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class Status
{
    public int Id { get; set; }

    public string? StatusName { get; set; }

    public string? StatusType { get; set; }

    public virtual ICollection<AuctionItem> AuctionItems { get; set; } = new List<AuctionItem>();

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

    public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
