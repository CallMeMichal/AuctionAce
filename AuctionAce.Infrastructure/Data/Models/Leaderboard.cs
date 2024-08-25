using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class Leaderboard
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public int? AuctionItemId { get; set; }

    public int? Price { get; set; }

    public bool? IsFinal { get; set; }

    public DateOnly? Date { get; set; }

    public virtual AuctionItem? AuctionItem { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
