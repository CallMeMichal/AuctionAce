using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class Auction
{
    public int Id { get; set; }

    public string? AuctionName { get; set; }

    public int? IdUsers { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? IdData { get; set; }

    public int? IdStatus { get; set; }

    public int? IdCategory { get; set; }

    public int? IdPayments { get; set; }

    public virtual ICollection<AuctionItem> AuctionItems { get; set; } = new List<AuctionItem>();

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual Calendar? IdDataNavigation { get; set; }

    public virtual Status? IdStatusNavigation { get; set; }

    public virtual User? IdUsersNavigation { get; set; }

    public virtual ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
}
