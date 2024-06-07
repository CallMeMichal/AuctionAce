using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class MessageHistory
{
    public int Id { get; set; }

    public int? IdAuctionItems { get; set; }

    public int? IdUser { get; set; }

    public DateOnly? Data { get; set; }

    public string? Message { get; set; }

    public virtual AuctionItem? IdAuctionItemsNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
