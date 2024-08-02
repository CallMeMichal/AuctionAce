namespace AuctionAce.Infrastructure.JwtAuthentication
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string? Secret { get; init; }
        public string? Issuer { get; init; }
        public string? Audience { get; init; }
        public int ExpirationTimeInMinutes { get; init; }
    }
}
