using Microsoft.EntityFrameworkCore;
using Moq;

using webapi.Data;
using webapi.Interfaces;
using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums.DND;
using webapi.Models.DND.Enums;
using webapi.Services;
using Xunit;

namespace webapiUnitTests.ServicesUnitTests
{
    public class UserServiceUT
    {
        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenIdIsValid()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "BaseUsers")
            .Options;

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => new User { ID = id, UserToken = 323 });
            var UserService = new UserService(mockRepo.Object);
            var User = await UserService.GetUserByIdAsync(1);
            Assert.NotNull(User);
            Assert.Equal(1, User.ID);
            Assert.Equal(323, User.UserToken);
        }
        [Fact]
        public void GetUserByIdAsync_MustReturnException_WhenIdIsNotValidOrThereIsNoUser()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "User")
            .Options;

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => null);

            var UserService = new UserService(mockRepo.Object);
            var User = UserService.GetUserByIdAsync(1);

            Assert.ThrowsAsync<Exception>(() => UserService.GetUserByIdAsync(0));
        }
        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenIdIsValidAndReturnAllCharacterSheetsConnectedToUser()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "User")
            .Options;

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => new User
            {
                ID = id,
                UserToken = 1,
                DNDCharacters = Helpers.Characters()
            });

            var UserService = new UserService(mockRepo.Object);
            var User = await UserService.GetUserByIdAsync(1);

            Assert.NotNull(User);
            Assert.Equal(1, User.ID);
            Assert.NotNull(User.DNDCharacters);
            Assert.Equal(3, User.DNDCharacters.Count);
            Assert.Equal(1, User.DNDCharacters.First().ID);
            Assert.Equal(1, User.DNDCharacters.First().UserToken);
        }
    }
}
