using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual Auction IdNavigation { get; set; } = null!;
}
