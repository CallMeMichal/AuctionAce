using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class Status
{
    public int Id { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
}
