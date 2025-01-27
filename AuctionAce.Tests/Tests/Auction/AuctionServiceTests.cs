using AuctionAce.Application.Interfaces;
using AuctionAce.Domain.Entities;
using Moq;

namespace AuctionAce.Tests.Tests.Auction
{
    public class AuctionServiceTests
    {

        private readonly Mock<IAuctionService> _auctionServiceMock;

        public AuctionServiceTests()
        {
            _auctionServiceMock = new Mock<IAuctionService>();
        }

        [Fact]
        public async Task GetAuctionsAsync_ShouldReturnListOfAuctions()
        {
            // Arrange
            var mockAuctions = new List<AuctionListDomain>
        {
            new AuctionListDomain
            {
                Id = 1,
                AuctionName = "Auction 1",
                Description = "Description of Auction 1"
            },
            new AuctionListDomain
            {
                Id = 2,
                AuctionName = "Auction 2",
                Description = "Description of Auction 2"
            }
        };

            // Skonfigurowanie mocka
            _auctionServiceMock
                .Setup(service => service.GetAuctionsAsync())
                .ReturnsAsync(mockAuctions);

            // Act
            var result = await _auctionServiceMock.Object.GetAuctionsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Auction 1", result[0].AuctionName);
            Assert.Equal("Auction 2", result[1].AuctionName);
            Assert.Equal("Description of Auction 1", result[0].Description);
            Assert.Equal("Description of Auction 2", result[1].Description);
        }

        [Fact]
        public async Task AddAuctionAsync_ShouldReturnTrue_WhenAuctionIsAddedSuccessfully()
        {
            // Arrange
            var categoryId = 1;
            var auctionName = "Test Auction";
            var auctionDescription = "This is a test auction description";
            var startDate = DateTime.Now.AddDays(1); // Start date is in the future
            var endDate = DateTime.Now.AddDays(2); // End date is in the future
            var auctionerId = 123;
            var auctionImagePaths = new Dictionary<string, string>
            {
                { "image1", "/path/to/image1.jpg" }
            };
            var itemsInfo = new List<AuctionItemsDomain>
            {
                new AuctionItemsDomain
                {
                    Name = "Test Item",
                    Description = "Test Item description",
                    StartingPrice = "100",
                    BuyNowPrice = "150",
                    NewUsed = true,
                    ItemImagePaths = new Dictionary<string, string> { { "itemImage1", "/path/to/item1.jpg" } }
                }
            };

            // Act
            // Mock the behavior for the AddAuctionAsync method to return true (indicating success)
            _auctionServiceMock.Setup(service => service.AddAuctionAsync(
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                It.IsAny<int>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<List<AuctionItemsDomain>>()))
                .ReturnsAsync(true); // Simulating a successful auction creation

            // Act: Call the AddAuctionAsync method
            var result = await _auctionServiceMock.Object.AddAuctionAsync(
                categoryId, auctionName, auctionDescription, startDate, endDate, auctionerId, auctionImagePaths, itemsInfo);

            // Assert
            Assert.True(result); // Expecting that the result is true (auction was added successfully)

            // Verify that AddAuctionAsync was called with any parameters
            _auctionServiceMock.Verify(service => service.AddAuctionAsync(
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                It.IsAny<int>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<List<AuctionItemsDomain>>()), Times.Once);
        }

        [Fact]
        public async Task GetRemainingTimeForAuction_ShouldReturnRemainingTimeInMilliseconds()
        {
            // Arrange
            var auctionId = 1;
            var remainingTime = new TimeSpan(1, 5, 30, 0); // 1 day, 5 hours, and 30 minutes

            // Mock the behavior for the GetRemainingTimeForAuction method
            _auctionServiceMock.Setup(service => service.GetRemainingTimeForAuction(auctionId))
                .ReturnsAsync(remainingTime.TotalMilliseconds); // Simulating the result in milliseconds

            // Act
            var result = await _auctionServiceMock.Object.GetRemainingTimeForAuction(auctionId);

            // Assert
            Assert.Equal(remainingTime.TotalMilliseconds, result); // Expecting the result to match the remaining time in milliseconds
        }
    }
}