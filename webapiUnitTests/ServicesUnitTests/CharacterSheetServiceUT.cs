using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Data;
using webapi.Interfaces;
using webapi.Models;
using webapi.Models.DND.Enums.DND;
using webapi.Models.DND.Enums;
using webapi.Models.DND;
using webapi.Services;
using Xunit;

namespace webapiUnitTests.ServicesUnitTests
{
    public class CharacterSheetServiceUT
    {

        [Fact]
        public async Task GetDNDCardByIdAsync_ShouldReturnCharacter_WhenIdIsValid()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "DNDCharacter")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            var mockRepo2 = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetDNDCardByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Helpers.Characters()[0]);
            var characterService = new CharacterSheetService(mockRepo.Object, mockRepo2.Object);
            var character = await characterService.GetDNDCardByIdAsync(1);

            Assert.NotNull(character);
            Assert.Equal(1, character.ID);
            Assert.Equal(1, character.UserToken);
        }
        [Fact]
        public void GetDNDCardByIdAsync_MustReturnException_WhenIdIsNotValidOrThereIsNoCharacter()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "BaseCharacters")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            var mockRepo2 = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetDNDCardByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => null);
            var characterService = new CharacterSheetService(mockRepo.Object, mockRepo2.Object);
            var character = characterService.GetDNDCardByIdAsync(1);

            Assert.ThrowsAsync<Exception>(() => characterService.GetDNDCardByIdAsync(0));
        }
        [Fact]
        public async Task GetAllDNDCharactersAsync_ShouldReturnAllCharacterSheetsAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "DNDCharacter")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            var mockRepo2 = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetAllDNDCharactersAsync())
            .ReturnsAsync(Helpers.Characters());
            var characterService = new CharacterSheetService(mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllDNDCharactersAsync();

            Assert.NotNull(characters);
            Assert.Equal(3, characters.Count());
            Assert.Equal(1, characters.First().ID);
        }
        [Fact]
        public void GetAllDNDCharactersAsync_ReturnsEmptyList_WhenNoCharactersExist()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "DNDCharacter")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            var mockRepo2 = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetDNDCardByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            var characterService = new CharacterSheetService(mockRepo.Object, mockRepo2.Object);
            var characters = characterService.GetAllDNDCharactersAsync();

            Assert.NotNull(characters);
            Assert.Empty(characters.Result);
        }
        [Fact]
        public async Task GetAllDNDCharactersByFiltersAsync_ShouldReturnAllCharacterSheetsAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "DNDCharacter")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            var mockRepo2 = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetAllDNDCharactersByFiltersAsync(It.IsAny<int>(), It.IsAny<SystemType>()))
            .ReturnsAsync((int id, SystemType systemType) => Helpers.Characters());
            var characterService = new CharacterSheetService(mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllDNDCharactersByFiltersAsync(It.IsAny<int>(), It.IsAny<SystemType>());

            Assert.NotNull(characters);
            Assert.Equal(3, characters.Count());
            Assert.Equal(1, characters.First().ID);
        }
        [Fact]
        public async Task GetAllDNDCharactersByUserToken_ShouldReturnAllMatchingCharacterSheetsAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "DNDCharacter")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            var mockRepo2 = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetAllDNDCharactersByFiltersAsync(It.IsAny<int>(), null))
            .ReturnsAsync((int UserToken, SystemType systemType) => Helpers.Characters().Where(c => c.UserToken == 1));
            var characterService = new CharacterSheetService(mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllDNDCharactersByFiltersAsync(1);

            Assert.NotNull(characters);
            Assert.Single(characters);
            Assert.Equal(1, characters.First().ID);
        }
        [Fact]
        public async Task GetAllDNDCharactersByUserTokenAndSystemType_ShouldReturnAllMatchingCharacterSheetsAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "DNDCharacter")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            var mockRepo2 = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetAllDNDCharactersByFiltersAsync(It.IsAny<int>(), It.IsAny<SystemType>()))
            .ReturnsAsync((int UserToken, SystemType systemType) => Helpers.Characters().Where(c => c.UserToken == 1 && c.SystemType == SystemType.DND));
            var characterService = new CharacterSheetService(mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllDNDCharactersByFiltersAsync(1, SystemType.DND);

            Assert.NotNull(characters);
            Assert.Single(characters);
            Assert.Equal(1, characters.First().ID);
        }
        [Fact]
        public async Task GetAllDNDCharactersByUserTokenAndSystemType_ShouldReturnEmptyListWhenNoMatchesAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "DNDCharacter")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            var mockRepo2 = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetAllDNDCharactersByFiltersAsync(It.IsAny<int>(), null))
            .ReturnsAsync((int UserToken, SystemType systemType) => Helpers.Characters().Where(c => c.UserToken == 10));
            var characterService = new CharacterSheetService(mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllDNDCharactersByFiltersAsync(10);

            Assert.NotNull(characters);
            Assert.Empty(characters);
        }
        [Fact]
        public async Task GetAllDNDCharactersByUserTokenAndNoSystemType_ShouldReturnEmptyListAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "DNDCharacter")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            var mockRepo2 = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetAllDNDCharactersByFiltersAsync(It.IsAny<int>(), It.IsAny<SystemType>()))
            .ReturnsAsync((int UserToken, SystemType systemType) => Helpers.Characters().Where(c => c.UserToken == 1 && c.SystemType == SystemType.Cthulu));
            var characterService = new CharacterSheetService(mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllDNDCharactersByFiltersAsync(1, SystemType.Cthulu);

            Assert.NotNull(characters);
            Assert.Empty(characters);
        }
    }

}
