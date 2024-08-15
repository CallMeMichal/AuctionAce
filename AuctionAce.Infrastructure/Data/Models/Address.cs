using System;
using System.Collections.Generic;

namespace AuctionAce.Api;

public partial class Address
{
    public int Id { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? ZipCode { get; set; }

    public int? BuildingNumber { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
