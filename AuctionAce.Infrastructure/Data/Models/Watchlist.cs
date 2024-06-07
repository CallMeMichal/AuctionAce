using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class Watchlist
{
    public int Id { get; set; }

    public int? IdAuction { get; set; }

    public DateOnly? WatchDate { get; set; }

    public int? IdUser { get; set; }

    public virtual Auction? IdAuctionNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
