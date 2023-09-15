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
using webapi.DTO;

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
        [Fact]
        public async Task GetUserByTokenAsync_ShouldReturnUser_WhenTokenIsValid()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserByTokenAsync(It.IsAny<int>()))
                .ReturnsAsync(new User { ID = 1, UserToken = 1 });

            var userService = new UserService(mockRepo.Object);
            var user = await userService.GetUserByTokenAsync(1);

            Assert.NotNull(user);
            Assert.Equal(1, user.ID);
            Assert.Equal(1, user.UserToken);
        }
        [Fact]
        public async Task GetUserByTokenAsync_ShouldReturnNull_WhenTokenIsInvalid()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserByTokenAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            var userService = new UserService(mockRepo.Object);
            var user = await userService.GetUserByTokenAsync(999);

            Assert.Null(user);
        }
        [Fact]
        public async Task CreateUserAsync_ShouldCreateUser_WhenTokenIsUnique()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserByTokenAsync(It.IsAny<int>())).ReturnsAsync((User)null);
            mockRepo.Setup(repo => repo.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(new User { ID = 1, UserToken = 1 });

            var userService = new UserService(mockRepo.Object);
            var userDto = new UserDto { UserToken = 1 };
            var user = await userService.CreateUserAsync(userDto);

            Assert.NotNull(user);
            Assert.Equal(1, user.ID);
            Assert.Equal(1, user.UserToken);
        }
        [Fact]
        public async Task CreateUserAsync_ShouldThrowException_WhenTokenIsAlreadyRegistered()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserByTokenAsync(It.IsAny<int>())).ReturnsAsync(new User { ID = 1, UserToken = 1 });

            var userService = new UserService(mockRepo.Object);
            var userDto = new UserDto { UserToken = 1 };

            await Assert.ThrowsAsync<Exception>(() => userService.CreateUserAsync(userDto));
        }

        [Fact]
        public async Task UpdateUserAndCharacterRelationship_ShouldReturnTrue_WhenUpdateIsSuccessful()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.UpdateUserAndCharacterRelationship(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            var userService = new UserService(mockRepo.Object);
            var result = await userService.UpdateUserAndCharacterRelationship(1, 1);

            Assert.True(result);
        }
        [Fact]
        public async Task UpdateUserAndCharacterRelationship_ShouldReturnFalse_WhenUpdateIsUnsuccessful()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.UpdateUserAndCharacterRelationship(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);

            var userService = new UserService(mockRepo.Object);
            var result = await userService.UpdateUserAndCharacterRelationship(1, 1);

            Assert.False(result);
        }
        [Fact]
        public async Task UpdateUserAndCharacterRelationship_ShouldReturnFalse_WhenUserOrCharacterDoesNotExist()
        {
            var mockRepo = new Mock<IUserRepository>();

            // Since the method returns Task<bool>, set up the mock to return either true or false.
            mockRepo.Setup(repo => repo.UpdateUserAndCharacterRelationship(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            var userService = new UserService(mockRepo.Object);

            // Invoke the method we want to test
            var result = await userService.UpdateUserAndCharacterRelationship(1, 999);

            // Assert that the result is what we expect
            Assert.False(result);
        }




    }
}
