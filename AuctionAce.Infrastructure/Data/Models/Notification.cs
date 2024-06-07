using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public int? IdAuctionItems { get; set; }

    public virtual AuctionItem? IdAuctionItemsNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
