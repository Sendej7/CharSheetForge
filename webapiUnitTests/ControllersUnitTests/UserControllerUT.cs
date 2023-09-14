using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Controllers;
using webapi.Data;
using webapi.Interfaces;
using webapi.Models;
using webapi.Models.DND.Enums.DND;
using webapi.Models.DND.Enums;
using webapi.Models.DND;
using webapi.Services;
using Xunit;

namespace webapiUnitTests.ControllersUnitTests
{
    public class UserControllerUT
    {
        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenIdIsValid()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "BaseUsers")
            .Options;

            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => new User { ID = id, UserToken = 1 });

            var UserController = new UserController(mockRepo.Object);
            var actionResult = await UserController.GetUserByIdAsync(1);
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);
            var user = okResult.Value as User;
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(user);
            Assert.Equal(1, user.ID);
            Assert.Equal(1, user.UserToken);

        }
        [Fact]
        public async Task GetUserByIdAsync_MustReturnException_WhenIdIsNotValidOrThereIsNoUser()
        {
            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((int id) => null);

            var UserController = new UserController(mockRepo.Object);

            var actionResult = await UserController.GetUserByIdAsync(1);
            var notFound = actionResult as NotFoundResult;

            Assert.NotNull(notFound);
            Assert.Equal(404, notFound.StatusCode);
        }
        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenIdIsValidAndReturnAllCharacterSheetsConnectedToUser()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "User")
            .Options;

            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => new User
            {
                ID = id,
                UserToken = 1,
                DNDCharacters = Helpers.Characters()
            });
            var UserController = new UserController(mockRepo.Object);

            var actionResult = await UserController.GetUserByIdAsync(1);
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);
            var user = okResult.Value as User;

            Assert.NotNull(user);
            Assert.Equal(1, user.ID);
            Assert.NotNull(user.DNDCharacters);
            Assert.Equal(3, user.DNDCharacters.Count);
            Assert.Equal(1, user.DNDCharacters.First().ID);
            Assert.Equal(1, user.DNDCharacters.First().UserToken);

        }
    }
}
