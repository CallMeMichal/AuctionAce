using AuctionAce.Infrastructure.Data;
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

        public async Task<bool> AddAuctionItemPhoto(Dictionary<string, string> file, int auctionId, int? auctionItemId)
        {
            try
            {
                foreach (var item in file)
                {
                    string filePath = item.Key;
                    string fileName = item.Value;

                    var newPhoto = new AuctionsItemsPhotos
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

        public async Task<List<AuctionsItemsPhotos>> GetPhotosForAuction(int auctionId)
        {
            return await _context.AuctionsItemsPhotos.Where(x => x.AuctionsId == auctionId && x.AuctionItemId == null).ToListAsync();
        }

        public async Task<List<List<AuctionsItemsPhotos>>> GetPhotosForItems(int auctionId)
        {
            var photos = await _context.AuctionsItemsPhotos.Where(x => x.AuctionsId == auctionId && x.AuctionItemId != null).ToListAsync();
            return photos.GroupBy(photo => photo.AuctionItemId).Select(group => group.ToList()).ToList();
        }

        public async Task<List<List<AuctionsItemsPhotos>>> GetPhotosForOneItem(int itemId)
        {
            var photos = await _context.AuctionsItemsPhotos.Where(x => x.AuctionItemId == itemId).ToListAsync();
            return photos.GroupBy(photo => photo.AuctionItemId).Select(group => group.ToList()).ToList();
        }
    }
}