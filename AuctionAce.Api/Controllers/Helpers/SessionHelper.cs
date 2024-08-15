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
    }
}