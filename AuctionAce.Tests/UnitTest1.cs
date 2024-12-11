using AuctionAce.Application.Services;

namespace AuctionAce.Tests
{
    public class UnitTest1
    {

        private readonly AuctionService service = new AuctionService(null,null);

        [Fact]
        public async Task Test1()
        {
            await service.CloseAuction(12);
        }
    }
}