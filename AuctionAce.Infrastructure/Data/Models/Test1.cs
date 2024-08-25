using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class Test1
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public virtual User? User { get; set; }
}
