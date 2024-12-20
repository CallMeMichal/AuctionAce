using AuctionAce.Application.Interfaces;
using AuctionAce.Infrastructure.Data.Models;
using Moq;
using Xunit;

namespace AuctionAce.Tests.Tests.User
{
    public class UserServiceTests
    {
        private readonly Mock<IUserService> _userServiceMock;

        // Zainicjalizuj mocka bezpośrednio w konstruktorze bez potrzeby wstrzykiwania go.
        public UserServiceTests()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [Fact]
        public async Task UserLogin_WithValidCredentials_ShouldReturnUser()
        {
            // Arrange
            var email = "test@example.com";
            var password = "testpassword";
            var expectedUser = new Infrastructure.Data.Models.User
            {
                Id = 25,
                Email = email,
                Name = "Test",
                Surname = "User"
            };

            _userServiceMock
                .Setup(x => x.UserLogin(email, password))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _userServiceMock.Object.UserLogin(email, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
            Assert.Equal("Test", result.Name);
            Assert.Equal("User", result.Surname);
        }

        [Fact]
        public async Task UserRegister_WithValidUser_ShouldReturnTrue()
        {
            // Arrange
            var user = new Infrastructure.Data.Models.User
            {
                Email = "newuser@example.com",
                Password = "strongpassword",
                Name = "New",
                Surname = "User",
                IdRoles = 2,
                IdStatus = 1
            };

            // Ustawienie mocka, aby metoda UserRegister zwracała true
            _userServiceMock
                .Setup(x => x.UserRegister(It.Is<Infrastructure.Data.Models.User>(u =>
                    u.Email == user.Email &&
                    u.Password == user.Password &&
                    u.Name == user.Name &&
                    u.Surname == user.Surname &&
                    u.IdRoles == user.IdRoles &&
                    u.IdStatus == user.IdStatus)))
                .ReturnsAsync(true);

            // Act
            var result = await _userServiceMock.Object.UserRegister(user);

            // Assert
            Assert.True(result); // Sprawdzenie, czy rejestracja zakończyła się sukcesem
        }
    }
}
