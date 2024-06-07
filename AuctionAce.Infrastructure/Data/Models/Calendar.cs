using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class Calendar
{
    public int Id { get; set; }

    public string? EventDate { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
}
