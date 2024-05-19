using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class ItemHistory
{
    public int Id { get; set; }

    public DateTime? Data { get; set; }

    public decimal? Price { get; set; }

    public int? IdAuctionItems { get; set; }

    public virtual AuctionItem? IdAuctionItemsNavigation { get; set; }
}
