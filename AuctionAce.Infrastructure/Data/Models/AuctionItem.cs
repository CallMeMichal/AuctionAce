﻿using AuctionAce.Infrastructure.Data.Models;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class AuctionItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Category { get; set; }
    public string? Amount { get; set; }
    public string StartingPrice { get; set; }
    public string BuyNowPrice { get; set; }
    public bool NewUsed { get; set; }
    public string Guid { get; set; }
    public int IdAuctions { get; set; }
    public int IdStatus { get; set; }

    public virtual Auction IdAuctionsNavigation { get; set; }
    public virtual ICollection<ChatHistory> ChatHistories { get; set; }
    public virtual ICollection<Leaderboards> Leaderboards { get; set; }
    public virtual ICollection<AuctionsItemsPhotos> AuctionsItemsPhotos { get; set; }
}

