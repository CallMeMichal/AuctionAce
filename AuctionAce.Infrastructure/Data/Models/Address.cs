using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class Address
{
    public int Id { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? ZipCode { get; set; }

    public int? BuildingNumber { get; set; }

    public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
