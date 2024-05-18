using System;
using System.Collections.Generic;

namespace AuctionAce.Api;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? IdRoles { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

    public virtual Role? IdRolesNavigation { get; set; }
}
