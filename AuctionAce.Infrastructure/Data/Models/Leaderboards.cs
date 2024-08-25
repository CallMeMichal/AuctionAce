using AuctionAce.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace AuctionAce.Infrastructure.Data.Models
{
    public partial class Leaderboards
    {
        public int Id { get; set; }
        public int AuctionItemId { get; set; }
        public int IdUser { get; set; }
        public decimal Price { get; set; }
        public bool IsFinal { get; set; }
        public DateTime Date { get; set; }

        public virtual AuctionItem AuctionItem { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
