using AuctionAce.Infrastructure.Data;
using AuctionAce.Domain.Entities;

namespace AuctionAce.Application.Services.Helpers
{
    public static class Helpers
    {
        public static PhotosAuctionItemDomain ProcessPhotos(List<AuctionsItemsPhotos> auctionPhoto, List<List<AuctionsItemsPhotos>> itemPhoto)
        {
            var auctionImages = auctionPhoto.Select(photo => new AuctionImage
            {
                AuctionImageBase64 = GetImageDataBase64(photo.Path)
            }).ToList();

            var itemImages = itemPhoto.
                SelectMany(group => group)
                .GroupBy(photo => photo.AuctionItemId.Value)
                .ToDictionary(
                group => group.Key,
                group => group.Select(photo => new ItemImages
                {
                    Id = photo.Id,
                    ItemImageBase64 = GetImageDataBase64(photo.Path)
                }).ToList()
                );
            return new PhotosAuctionItemDomain()
            {
                AuctionImages = auctionImages,
                ItemImages = itemImages
            };
        }

        public static PhotosAuctionItemDomain ProcessPhotos(List<List<AuctionsItemsPhotos>> itemPhoto)
        {
            var itemImages = itemPhoto.
                SelectMany(group => group)
                .GroupBy(photo => photo.AuctionItemId.Value)
                .ToDictionary(
                group => group.Key,
                group => group.Select(photo => new ItemImages
                {
                    Id = photo.Id,
                    ItemImageBase64 = GetImageDataBase64(photo.Path)
                }).ToList()
                );
            return new PhotosAuctionItemDomain()
            {
                ItemImages = itemImages
            };
        }

        public static string GetImageDataBase64(string path)
        {
            var data = File.ReadAllBytes(path);
            return Convert.ToBase64String(data);
            //return File.ReadAllBytes(path);
        }

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}