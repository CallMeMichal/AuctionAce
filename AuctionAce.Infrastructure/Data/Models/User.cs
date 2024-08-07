﻿using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Surname { get; set; }
    
    public string? Email { get; set; }

    public string?  Password { get; set; }

    public int? IdRoles { get; set; }

    public int? IdStatus { get; set; }

    public int? IdAddress { get; set; }

    public bool? IsLogined { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
    public virtual Address? IdAddressNavigation { get; set; }
    
    public virtual Role? IdRolesNavigation { get; set; }

    public virtual Status? IdStatusNavigation { get; set; }

    public virtual ICollection<MessageHistory> MessageHistories { get; set; } = new List<MessageHistory>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
}
