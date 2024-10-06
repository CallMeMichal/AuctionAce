using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class AuctionItem
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdAuctions { get; set; }

    public string? Description { get; set; }

    public string? StartingPrice { get; set; }

    public string? BuyNowPrice { get; set; }

    public bool? NewUsed { get; set; }

    public int? IdStatus { get; set; }

    public string? Guid { get; set; }

    public bool? IsBought { get; set; }

    public virtual ICollection<AuctionsItemsPhoto> AuctionsItemsPhotos { get; set; } = new List<AuctionsItemsPhoto>();

    public virtual ICollection<BidHistory> BidHistories { get; set; } = new List<BidHistory>();

    public virtual ICollection<ChatHistory> ChatHistories { get; set; } = new List<ChatHistory>();

    public virtual Auction? IdAuctionsNavigation { get; set; }

    public virtual ICollection<Leaderboard> Leaderboards { get; set; } = new List<Leaderboard>();

    public virtual ICollection<UserBoughtItem> UserBoughtItems { get; set; } = new List<UserBoughtItem>();
}
