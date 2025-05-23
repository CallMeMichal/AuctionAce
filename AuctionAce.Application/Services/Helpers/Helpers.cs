﻿using AuctionAce.Domain.Entities;
using AuctionAce.Infrastructure.Data.Models;

namespace AuctionAce.Application.Services.Helpers
{
    public static class Helpers
    {
        public static PhotosAuctionItemDomain ProcessPhotos(List<AuctionsItemsPhoto> auctionPhoto, List<List<AuctionsItemsPhoto>> itemPhoto)
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

        public static PhotosAuctionItemDomain ProcessPhotos(List<List<AuctionsItemsPhoto>> itemPhoto)
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