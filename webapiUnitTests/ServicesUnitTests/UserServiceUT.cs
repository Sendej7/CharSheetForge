using Microsoft.EntityFrameworkCore;
using Moq;

using webapi.Data;
using webapi.Interfaces;
using webapi.Models;
using webapi.Services;
using Xunit;

namespace webapiUnitTests.ServicesUnitTests
{
    public class UserServiceUT
    {
        [Fact]
        public void GetUserByIdAsync_ShouldReturnUser_WhenIdIsValid()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "BaseUsers")
            .Options;

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => new User { ID = id, UserToken = 323 });
            var UserService = new UserService(mockRepo.Object);
            var User = UserService.GetUserByIdAsync(1);

            Assert.NotNull(User);
            Assert.Equal(1, User.Result.ID);
            Assert.Equal(323, User.Result.UserToken);
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
    }
}
