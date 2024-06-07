using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

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

    public virtual Status? IdStatusNavigation { get; set; }

    public virtual ICollection<ItemHistory> ItemHistories { get; set; } = new List<ItemHistory>();

    public virtual ICollection<MessageHistory> MessageHistories { get; set; } = new List<MessageHistory>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
}
