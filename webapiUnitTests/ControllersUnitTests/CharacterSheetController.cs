using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Controllers;
using webapi.DTO;
using webapi.Interfaces;
using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;
using Xunit;

namespace webapiUnitTests.ControllersUnitTests
{
    public class CharacterSheetControllerUT
    {
        [Fact]
        public async Task GetDNDCardById_ShouldReturnOk_WhenIdIsValid()
        {
            var mockService = new Mock<ICharacterSheetService>();
            var mockService2 = new Mock<IUserService>();
            mockService.Setup(s => s.GetDNDCardByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync(Helpers.Characters()[0]);

            var controller = new CharacterSheetController(mockService.Object, mockService2.Object);

            var result = await controller.GetDNDCardById(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<DndCharacter>(okResult.Value);

            Assert.Equal(1, returnValue.ID);
        }

        [Fact]
        public async Task GetDNDCardById_ShouldReturnNotFound_WhenIdIsInvalid()
        {
            var mockService = new Mock<ICharacterSheetService>();
            var mockService2 = new Mock<IUserService>();
            mockService.Setup(s => s.GetDNDCardByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync((DndCharacter?) null);

            var controller = new CharacterSheetController(mockService.Object, mockService2.Object);

            var result = await controller.GetDNDCardById(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllDNDCharacters_ShouldReturnOk()
        {
            var mockService = new Mock<ICharacterSheetService>();
            var mockService2 = new Mock<IUserService>();

            mockService.Setup(s => s.GetAllDNDCharactersAsync())
                       .ReturnsAsync(Helpers.Characters());

            var controller = new CharacterSheetController(mockService.Object, mockService2.Object);

            var result = await controller.GetAllDNDCharacters();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<DndCharacter>>(okResult.Value);

            Assert.Equal(3, returnValue.Count);
        }

        [Fact]
        public async Task GetAllDNDCharactersByFilters_ShouldReturnOk()
        {
            var mockService = new Mock<ICharacterSheetService>(); 
            var mockService2 = new Mock<IUserService>();

            mockService.Setup(s => s.GetAllDNDCharactersByFiltersAsync(It.IsAny<int>(), It.IsAny<SystemType>()))
                       .ReturnsAsync(Helpers.Characters());

            var controller = new CharacterSheetController(mockService.Object, mockService2.Object);

            var result = await controller.GetAllDNDCharactersByFilters(1, SystemType.DND);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<DndCharacter>>(okResult.Value);

            Assert.Equal(3, returnValue.Count);
        }
        [Fact]
        public async Task GetAllDNDCharacters_ShouldReturnOkWithEmptyList_WhenNoCharactersExist()
        {
            var mockService = new Mock<ICharacterSheetService>();
            var mockService2 = new Mock<IUserService>();

            mockService.Setup(s => s.GetAllDNDCharactersAsync())
                       .ReturnsAsync(new List<DndCharacter>());

            var controller = new CharacterSheetController(mockService.Object, mockService2.Object);

            var result = await controller.GetAllDNDCharacters();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<DndCharacter>>(okResult.Value);

            Assert.Empty(returnValue);
        }

        [Fact]
        public async Task GetAllDNDCharactersByFilters_ShouldReturnOkWithEmptyList_WhenNoMatches()
        {
            var mockService = new Mock<ICharacterSheetService>();
            var mockService2 = new Mock<IUserService>();

            mockService.Setup(s => s.GetAllDNDCharactersByFiltersAsync(It.IsAny<int>(), It.IsAny<SystemType?>()))
                       .ReturnsAsync(new List<DndCharacter>());

            var controller = new CharacterSheetController(mockService.Object, mockService2.Object);

            var result = await controller.GetAllDNDCharactersByFilters(1, SystemType.DND);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<DndCharacter>>(okResult.Value);

            Assert.Empty(returnValue);
        }
        [Fact]
        public async Task CreateNewDndCharacter_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockCharacterService = new Mock<ICharacterSheetService>();
            mockUserService.Setup(s => s.GetUserByTokenAsync(It.IsAny<int>()))
                           .ReturnsAsync((User?)null);

            var controller = new CharacterSheetController(mockCharacterService.Object, mockUserService.Object);

            // Act
            var result = await controller.CreateNewDndCharacter(1, new DndCharacterDto());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateNewDndCharacter_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockCharacterService = new Mock<ICharacterSheetService>();
            mockUserService.Setup(s => s.GetUserByTokenAsync(It.IsAny<int>()))
                           .ThrowsAsync(new Exception("An error occurred"));

            var controller = new CharacterSheetController(mockCharacterService.Object, mockUserService.Object);

            // Act
            var result = await controller.CreateNewDndCharacter(1, new DndCharacterDto());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("An error occurred", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateNewDndCharacter_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockCharacterService = new Mock<ICharacterSheetService>();
            var user = new User { ID = 1, UserToken = 1 };
            var character = new DndCharacter { ID = 1, UserToken = 1 };

            mockUserService.Setup(s => s.GetUserByTokenAsync(It.IsAny<int>()))
                           .ReturnsAsync(user);
            mockCharacterService.Setup(s => s.CreateCharacterAsync(It.IsAny<int>(), It.IsAny<DndCharacterDto>()))
                                .ReturnsAsync(character);

            var controller = new CharacterSheetController(mockCharacterService.Object, mockUserService.Object);

            // Act
            var result = await controller.CreateNewDndCharacter(1, new DndCharacterDto());

            // Assert
            Assert.IsType<OkResult>(result);
            mockUserService.Verify(m => m.UpdateUserAndCharacterRelationship(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

    }
}
