using AuctionAce.Infrastructure.Data.AuctionAceDbContext;
using AuctionAce.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionAce.Infrastructure.Repositories
{
    public class AuctionRespository
    {
        private readonly AuctionAceContext _context;

        public AuctionRespository(AuctionAceContext context)
        {
            _context = context;
        }


        public async Task<List<Auction>> GetAuctionsAsync()
        {
            var auctionList = new List<Auction>();
            //aktywna aukcja
            var auctions = _context.Auctions.Where(x=>x.IdStatus == 1);
            auctionList.AddRange(auctions);
            return auctionList;
        }

        public async Task<List<Auction>> GetAuctionsByIdUserAsync(int idUser)
        {
            var auctionList = new List<Auction>();
            //aktywna aukcja
            var auctions = _context.Auctions.Where(x => x.IdStatus == 1 && x.IdUsers == idUser);
            auctionList.AddRange(auctions);
            return auctionList;
        }

        public async Task AddAuctionAsync(Auction auction)
        {
            _context.Auctions.Add(auction);
            _context.SaveChanges();
        }

        

    }
}
