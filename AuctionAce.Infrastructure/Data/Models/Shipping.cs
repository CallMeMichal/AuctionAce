using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class Shipping
{
    public int Id { get; set; }

    public int? IdAuctionItems { get; set; }

    public int? IdAddress { get; set; }

    public int? IdStatus { get; set; }

    public virtual Address? IdAddressNavigation { get; set; }

    public virtual AuctionItem? IdAuctionItemsNavigation { get; set; }

    public virtual Status? IdStatusNavigation { get; set; }
}
