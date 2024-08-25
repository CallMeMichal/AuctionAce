using System;
using System.Collections.Generic;
namespace AuctionAce.Infrastructure.Data.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Descripton { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
}
