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
using webapi.DTO;

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
            var mockRepo2 = new Mock<ICharacterSheetService>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => new User { ID = id, UserToken = 1 });

            var UserController = new UserController(mockRepo.Object, It.IsAny<ICharacterSheetService>());
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
            var mockRepo2 = new Mock<ICharacterSheetService>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((int id) => null);

            var UserController = new UserController(mockRepo.Object, It.IsAny<ICharacterSheetService>());

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
            var mockRepo2 = new Mock<ICharacterSheetService>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => new User
            {
                ID = id,
                UserToken = 1,
                DNDCharacters = Helpers.Characters()
            });
            var UserController = new UserController(mockRepo.Object, It.IsAny<ICharacterSheetService>());


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
        [Fact]
        public async Task GetUserByUserTokenAsync_ShouldReturnUser_WhenTokenIsValid()
        {
            var mockRepo = new Mock<IUserService>();
            var mockRepo2 = new Mock<ICharacterSheetService>();
            mockRepo.Setup(repo => repo.GetUserByTokenAsync(It.IsAny<int>()))
                    .ReturnsAsync((int token) => new User { ID = 1, UserToken = token });

            var UserController = new UserController(mockRepo.Object, mockRepo2.Object);
            var actionResult = await UserController.GetUserByUserTokenAsync(1);
            var okResult = actionResult as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var user = okResult.Value as User;
            Assert.NotNull(user);
            Assert.Equal(1, user.UserToken);
        }

        [Fact]
        public async Task GetUserByUserTokenAsync_ShouldReturnNotFound_WhenTokenIsInvalid()
        {
            var mockRepo = new Mock<IUserService>();
            var mockRepo2 = new Mock<ICharacterSheetService>();
            mockRepo.Setup(repo => repo.GetUserByTokenAsync(It.IsAny<int>()))
                    .ReturnsAsync((User?)null);

            var UserController = new UserController(mockRepo.Object, mockRepo2.Object);
            var actionResult = await UserController.GetUserByUserTokenAsync(1);
            var notFoundResult = actionResult as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetUserByUserTokenAsync_ShouldReturnNotFound_WhenTokenIsZero()
        {
            var mockRepo = new Mock<IUserService>();
            var mockRepo2 = new Mock<ICharacterSheetService>();
            mockRepo.Setup(repo => repo.GetUserByTokenAsync(0))
                    .ReturnsAsync((User?)null);

            var UserController = new UserController(mockRepo.Object, mockRepo2.Object);
            var actionResult = await UserController.GetUserByUserTokenAsync(0);
            var notFoundResult = actionResult as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        [Fact]
        public async Task CreateNewUser_ShouldReturnOk_WhenUserAndCharacterCreatedSuccessfully()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockCharacterSheetService = new Mock<ICharacterSheetService>();
            var controller = new UserController(mockUserService.Object, mockCharacterSheetService.Object);

            var userRequest = new CreateUserRequest
            {
                User = new UserDto { UserToken = 1 },
                DndCharacter = new DndCharacterDto { CharacterName = "Frodo" }
            };

            mockUserService.Setup(s => s.CreateUserAsync(It.IsAny<UserDto>()))
                           .ReturnsAsync(new User { UserToken = 1 });

            mockCharacterSheetService.Setup(s => s.CreateCharacterAsync(It.IsAny<int>(), It.IsAny<DndCharacterDto>()))
                                     .ReturnsAsync(new DndCharacter { UserToken = 1 });

            mockUserService.Setup(s => s.UpdateUserAndCharacterRelationship(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(true);

            // Act
            var result = await controller.CreateNewUser(userRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(1, returnValue.UserToken);
        }

        [Fact]
        public async Task CreateNewUser_ShouldReturnBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockCharacterSheetService = new Mock<ICharacterSheetService>();
            var controller = new UserController(mockUserService.Object, mockCharacterSheetService.Object);

            var userRequest = new CreateUserRequest
            {
                User = new UserDto { UserToken = 1 }
            };

            mockUserService.Setup(s => s.CreateUserAsync(It.IsAny<UserDto>()))
                           .Throws(new System.Exception("Some error"));

            // Act
            var result = await controller.CreateNewUser(userRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Some error", badRequestResult.Value);
        }
        [Fact]
        public async Task CreateNewUser_ShouldReturnOk_WhenUserCreatedButCharacterIsNull()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockCharacterSheetService = new Mock<ICharacterSheetService>();
            var controller = new UserController(mockUserService.Object, mockCharacterSheetService.Object);

            var userRequest = new CreateUserRequest
            {
                User = new UserDto { UserToken = 1 },
                DndCharacter = null
            };

            mockUserService.Setup(s => s.CreateUserAsync(It.IsAny<UserDto>()))
                           .ReturnsAsync(new User { UserToken = 1 });

            // Act
            var result = await controller.CreateNewUser(userRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(1, returnValue.UserToken);

            // Ensure CreateCharacterAsync was never called
            mockCharacterSheetService.Verify(s => s.CreateCharacterAsync(It.IsAny<int>(), It.IsAny<DndCharacterDto>()), Times.Never());

            // Ensure UpdateUserAndCharacterRelationship was never called
            mockUserService.Verify(s => s.UpdateUserAndCharacterRelationship(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
        }
        [Fact]
        public async Task CreateNewUser_ShouldUpdateUserAndCharacterRelationship_WhenBothUserAndCharacterAreCreated()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockCharacterSheetService = new Mock<ICharacterSheetService>();
            var controller = new UserController(mockUserService.Object, mockCharacterSheetService.Object);

            var userRequest = new CreateUserRequest
            {
                User = new UserDto { UserToken = 1 },
                DndCharacter = new DndCharacterDto { CharacterName = "Frodo" }
            };

            mockUserService.Setup(s => s.CreateUserAsync(It.IsAny<UserDto>()))
                           .ReturnsAsync(new User { UserToken = 1 });

            mockCharacterSheetService.Setup(s => s.CreateCharacterAsync(It.IsAny<int>(), It.IsAny<DndCharacterDto>()))
                                     .ReturnsAsync(new DndCharacter { UserToken = 1 });

            // Act
            var result = await controller.CreateNewUser(userRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(1, returnValue.UserToken);

            // Verify that UpdateUserAndCharacterRelationship was called once
            mockUserService.Verify(s => s.UpdateUserAndCharacterRelationship(1, 1), Times.Once());
        }
        [Fact]
        public async Task CreateNewUser_ShouldNotCallUpdateUserAndCharacterRelationship_WhenEitherUserOrCharacterIsNull()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockCharacterSheetService = new Mock<ICharacterSheetService>();
            var controller = new UserController(mockUserService.Object, mockCharacterSheetService.Object);

            var userRequest = new CreateUserRequest
            {
                User = new UserDto { UserToken = 1 },
                DndCharacter = new DndCharacterDto { CharacterName = "Frodo" }
            };

            mockUserService.Setup(s => s.CreateUserAsync(It.IsAny<UserDto>()))
                           .ReturnsAsync((User?)null); // Return null user

            mockCharacterSheetService.Setup(s => s.CreateCharacterAsync(It.IsAny<int>(), It.IsAny<DndCharacterDto>()))
                                     .ReturnsAsync(new DndCharacter { UserToken = 1 });

            // Act
            var result = await controller.CreateNewUser(userRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Null(okResult.Value);

            // Verify that UpdateUserAndCharacterRelationship was never called
            mockUserService.Verify(s => s.UpdateUserAndCharacterRelationship(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
        }


    }
}
