using AuctionAce.Infrastructure.Data;
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

    public int? IdCategory { get; set; }

    public string? Description { get; set; }
    public virtual ICollection<AuctionItem> AuctionItems { get; set; } = new List<AuctionItem>();

    public virtual ICollection<AuctionsItemsPhotos> AuctionsItemsPhotoss { get; set; } = new List<AuctionsItemsPhotos>();

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual User? IdUsersNavigation { get; set; }
}
