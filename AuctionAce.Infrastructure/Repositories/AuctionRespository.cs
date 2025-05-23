﻿using AuctionAce.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

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
            var auctions = await _context.Auctions.ToListAsync();
            return auctions;
        }

        public async Task<List<AuctionItem>> GetAuctionsItemsAsync()
        {
            var auctions = await _context.AuctionItems.ToListAsync();
            return auctions;
        }

        public async Task<List<Auction>> GetAuctionsByIdUserAsync(int userId)
        {
            var auctions = await _context.Auctions.Where(x => x.IdUsers == userId).ToListAsync();
            return auctions;
        }

        public async Task<int> AddAuctionAsync(Auction auction)
        {
            try
            {
                await _context.Auctions.AddAsync(auction);
                await _context.SaveChangesAsync();
                return auction.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<AuctionItem>> GetAuctionAsync(int auctionId)
        {
            var auction = await _context.AuctionItems.Where(x => x.IdAuctions == auctionId).ToListAsync();

            return auction;
        }

        public async Task<Auction> GetAuctionById(int? auctionId)
        {
            var auction = await _context.Auctions.FirstOrDefaultAsync(x => x.Id == auctionId);
            return auction;
        }

        public async Task<bool> AddAuctionItemPhoto(Dictionary<string, string> file, int auctionId, int? auctionItemId)
        {
            try
            {
                foreach (var item in file)
                {
                    string filePath = item.Key;
                    string fileName = item.Value;

                    var newPhoto = new AuctionsItemsPhoto
                    {
                        Path = filePath,
                        FileName = fileName,
                        AuctionsId = auctionId,
                        AuctionItemId = auctionItemId
                    };
                    await _context.AuctionsItemsPhotos.AddAsync(newPhoto);
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> AddAuctionItemAsync(AuctionItem auctionItem)
        {
            try
            {
                await _context.AuctionItems.AddAsync(auctionItem);
                await _context.SaveChangesAsync();
                return auctionItem.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<bool> AddItemsToAuction(List<AuctionItem> items)
        {
            foreach (var item in items)
            {
                await _context.AuctionItems.AddAsync(item);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AuctionsItemsPhoto>> GetPhotosForAuction(int auctionId)
        {
            return await _context.AuctionsItemsPhotos.Where(x => x.AuctionsId == auctionId && x.AuctionItemId == null).ToListAsync();
        }

        public async Task<List<List<AuctionsItemsPhoto>>> GetPhotosForItems(int auctionId)
        {
            var photos = await _context.AuctionsItemsPhotos.Where(x => x.AuctionsId == auctionId && x.AuctionItemId != null).ToListAsync();
            return photos.GroupBy(photo => photo.AuctionItemId).Select(group => group.ToList()).ToList();
        }

        public async Task<List<List<AuctionsItemsPhoto>>> GetPhotosForOneItem(int itemId)
        {
            var photos = await _context.AuctionsItemsPhotos.Where(x => x.AuctionItemId == itemId).ToListAsync();
            return photos.GroupBy(photo => photo.AuctionItemId).Select(group => group.ToList()).ToList();
        }

        public async Task<TimeSpan> GetTimeForAuction(int auctionId)
        {
            var auction = _context.Auctions.FirstOrDefault(x => x.Id == auctionId);
            var now = DateTime.Now;
            var timeRemaining = auction.EndDate - now;
            return (TimeSpan)timeRemaining;
        }

        public async Task<int> GetAutionId(int itemId)
        {
            var auction = await _context.AuctionItems.FirstOrDefaultAsync(x => x.Id == itemId);
            return (int)auction.IdAuctions;
        }

        public async Task<int> GetStartPriceForItem(int itemId)
        {
            var startPrice = await _context.AuctionItems.FirstOrDefaultAsync(x => x.Id == itemId);
            var convertedPrice = Convert.ToInt32(startPrice.StartingPrice);
            return convertedPrice;
        }

        public async Task<int> GetBuyNowPriceForItem(int itemId)
        {
            var buyNowPrice = await _context.AuctionItems.FirstOrDefaultAsync(x => x.Id == itemId);
            var convertedPrice = Convert.ToInt32(buyNowPrice.BuyNowPrice);
            return convertedPrice;
        }

        public async Task<bool> UpdateAuctionItemStatus(int itemId, int status)
        {
            var item = await _context.AuctionItems.FirstOrDefaultAsync(x => x.Id == itemId);
            item.IdStatus = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SetActiveItemsInAuction(int auctionId)
        {
            var itemsAuction = await _context.AuctionItems.Where(x => x.IdAuctions == auctionId).ToListAsync();
            foreach(var item in itemsAuction)
            {
                if (item.IdStatus == 0 && item.IsBought == false)
                {
                    item.IdStatus = 1;
                }

            }
            await _context.SaveChangesAsync();
        }
        public async Task SetInactiveItemsInAuctionWithoutBids(int auctionId)
        {
            var itemsAuction = await _context.AuctionItems.Where(x => x.IdAuctions == auctionId).ToListAsync();
            foreach (var item in itemsAuction)
            {
                var hasBidHistory = await _context.BidHistories.AnyAsync(b => b.IdAuctionItems == item.Id);

                if (!hasBidHistory && item.IsBought == false)
                {
                    item.IdStatus = 0;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task CloseAuction(int auctionId)
        {
            var auction = await _context.Auctions.FirstOrDefaultAsync(x => x.Id == auctionId);
            
            if (auction != null)
            {
                foreach (var item in auction.AuctionItems)
                {
                    item.IdStatus = 0;
                }
                await _context.SaveChangesAsync();
            }

        }

        public async Task<(string,string)> GetAuctionName(int auctionId)
        {
            var auctionName = await _context.Auctions.FirstOrDefaultAsync(x => x.Id == auctionId);
            return (auctionName.AuctionName,auctionName.Description);
        }

        public async Task<int> GetAuctionOwnerId(int auctionId)
        {
            
            var ownerObject = _context.Auctions.FirstOrDefault(x=>x.Id == auctionId);
            return (int)ownerObject.IdUsers;
        }
    }
}