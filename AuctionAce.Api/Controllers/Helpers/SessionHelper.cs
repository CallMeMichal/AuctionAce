namespace AuctionAce.Api.Controllers.Helpers
{
    public static class SessionHelper
    {
        public static int GetUserIdFromSession(HttpContext httpContext)
        {
            var userIdString = httpContext.Session.GetString("UserId");
            if (int.TryParse(userIdString, out var userId))
            {
                return userId;
            }
            throw new InvalidOperationException("UserId is not in the session or is not a valid integer.");
        }

        public static async Task<Dictionary<string, string>> SaveFilesAsync(List<IFormFile> files, string auctionName, bool isAuctionPhoto)
        {
            var filePaths = new Dictionary<string, string>();

            string baseFolder = "AuctionsData";
            var auctionFolder = Path.Combine(baseFolder, auctionName);

            if (!Directory.Exists(auctionFolder))
            {
                Directory.CreateDirectory(auctionFolder);
            }

            string targetFolder;

            if (isAuctionPhoto)
            {
                targetFolder = Path.Combine(auctionFolder, "AuctionPhoto");
            }
            else
            {
                targetFolder = Path.Combine(auctionFolder, "AuctionItems");
            }

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                string filePath;
                var uniqueFileName = "";
                if (!isAuctionPhoto)
                {
                    uniqueFileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + i + Path.GetExtension(file.FileName);
                    filePath = Path.Combine(targetFolder, uniqueFileName);
                }
                else
                {
                    uniqueFileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + i + Path.GetExtension(file.FileName);
                    filePath = Path.Combine(targetFolder, uniqueFileName);
                }

                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    filePaths.Add(filePath, uniqueFileName);
                }
            }

            return filePaths;
        }




    }
}