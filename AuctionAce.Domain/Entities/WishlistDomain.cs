using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionAce.Domain.Entities
{
    public class WishlistDomain
    {
        public int? UserId { get; set; }
        public int? AuctionId { get; set; }
    }
}
