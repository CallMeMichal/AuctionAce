using AuctionAce.Infrastructure.Data;
using System;
using System.Collections.Generic;
namespace AuctionAce.Infrastructure;

public partial class Address
{
    public int Id { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? ZipCode { get; set; }

    public int? BuildingNumber { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
