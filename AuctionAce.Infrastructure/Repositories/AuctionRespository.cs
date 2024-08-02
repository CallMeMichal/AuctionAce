using AuctionAce.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionAce.Infrastructure.Repositories
{
    public class AuctionRespository
    {
        private readonly ApplicationDbContext _context;

        public AuctionRespository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Auction>> GetAuctionsAsync()
        {
            var auctionList = new List<Auction>();
            var auctions = _context.Auctions.Where(x => x.IdStatus == 1);
            auctionList.AddRange(auctions);
            return auctionList;
        }

        public async Task<List<Auction>> GetAuctionsByIdUserAsync(int idUser)
        {
            var auctions = await _context.Auctions.Where(x => x.IdUsers == idUser).ToListAsync();
            return auctions;
        }

        public async Task<bool> AddAuctionAsync(Auction auction)
        {
            try
            {
                await _context.Auctions.AddAsync(auction);
                await _context.SaveChangesAsync(); // Ensure SaveChangesAsync is awaited
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<AuctionItem>> GetAuctionAsync(int auctionId)
        {
            var auction = await _context.AuctionItems.Where(x => x.IdAuctions == auctionId).ToListAsync();

            return auction;
        }
    }
}