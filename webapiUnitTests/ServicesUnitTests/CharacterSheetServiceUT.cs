﻿using Microsoft.EntityFrameworkCore;
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
using webapi.Models.DND;
using webapi.Services;
using Xunit;
using webapi.DTO;
using AutoMapper;

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
            var mockIMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetCharacterSheetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Helpers.Characters()[0]);
            var characterService = new CharacterSheetService(mockIMapper.Object, mockRepo.Object, mockRepo2.Object);
            var character = await characterService.GetCharacterSheetByIdAsync(1);

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
            var mockIMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetCharacterSheetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => null);
            var characterService = new CharacterSheetService(mockIMapper.Object, mockRepo.Object, mockRepo2.Object);
            var character = characterService.GetCharacterSheetByIdAsync(1);

            Assert.ThrowsAsync<Exception>(() => characterService.GetCharacterSheetByIdAsync(0));
        }
        [Fact]
        public async Task GetAllDNDCharactersAsync_ShouldReturnAllCharacterSheetsAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "DNDCharacter")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            var mockRepo2 = new Mock<IUserRepository>();
            var mockIMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetAllCharacterSheetsAsync())
            .ReturnsAsync(Helpers.Characters());
            var characterService = new CharacterSheetService(mockIMapper.Object, mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllCharacterSheetsAsync();

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
            var mockIMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetCharacterSheetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            var characterService = new CharacterSheetService(mockIMapper.Object, mockRepo.Object, mockRepo2.Object);
            var characters = characterService.GetAllCharacterSheetsAsync();

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
            var mockIMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetAllCharacterSheetsByFiltersAsync(It.IsAny<int>(), It.IsAny<SystemType>()))
            .ReturnsAsync((int id, SystemType systemType) => Helpers.Characters());
            var characterService = new CharacterSheetService(mockIMapper.Object, mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllCharacterSheetsByFiltersAsync(It.IsAny<int>(), It.IsAny<SystemType>());

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
            var mockIMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetAllCharacterSheetsByFiltersAsync(It.IsAny<int>(), null))
            .ReturnsAsync((int UserToken, SystemType systemType) => Helpers.Characters().Where(c => c.UserToken == 1));
            var characterService = new CharacterSheetService(mockIMapper.Object, mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllCharacterSheetsByFiltersAsync(1);

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
            var mockIMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetAllCharacterSheetsByFiltersAsync(It.IsAny<int>(), It.IsAny<SystemType>()))
            .ReturnsAsync((int UserToken, SystemType systemType) => Helpers.Characters().Where(c => c.UserToken == 1 && c.SystemType == SystemType.DND));
            var characterService = new CharacterSheetService(mockIMapper.Object, mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllCharacterSheetsByFiltersAsync(1, SystemType.DND);

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
            var mockIMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetAllCharacterSheetsByFiltersAsync(It.IsAny<int>(), null))
            .ReturnsAsync((int UserToken, SystemType systemType) => Helpers.Characters().Where(c => c.UserToken == 10));
            var characterService = new CharacterSheetService(mockIMapper.Object, mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllCharacterSheetsByFiltersAsync(10);

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
            var mockIMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetAllCharacterSheetsByFiltersAsync(It.IsAny<int>(), It.IsAny<SystemType>()))
            .ReturnsAsync((int UserToken, SystemType systemType) => Helpers.Characters().Where(c => c.UserToken == 1 && c.SystemType == SystemType.Cthulu));
            var characterService = new CharacterSheetService(mockIMapper.Object, mockRepo.Object, mockRepo2.Object);
            var characters = await characterService.GetAllCharacterSheetsByFiltersAsync(1, SystemType.Cthulu);

            Assert.NotNull(characters);
            Assert.Empty(characters);
        }
        [Fact]
        public async Task CreateCharacterAsync_ShouldThrowException_WhenUserNotFound()
        {
            var mockUserRepo = new Mock<IUserRepository>();
            var mockCharacterRepo = new Mock<ICharacterSheetRepository>();
            var mockIMapper = new Mock<IMapper>();

            mockUserRepo.Setup(repo => repo.GetUserByTokenAsync(It.IsAny<int>()))
                .ReturnsAsync((User?)null);

            var characterService = new CharacterSheetService(mockIMapper.Object, mockCharacterRepo.Object, mockUserRepo.Object);

            await Assert.ThrowsAsync<Exception>(() => characterService.CreateCharacterAsync<BaseCharacter, DndCharacterDto>(1, new DndCharacterDto()));
        }
        [Fact]
        public async Task CreateCharacterAsync_ShouldCreateCharacter_WhenUserExists()
        {
            var mockUserRepo = new Mock<IUserRepository>();
            var mockCharacterRepo = new Mock<ICharacterSheetRepository>();
            var mockIMapper = new Mock<IMapper>();

            var testUser = new User() { UserToken = 1 };
            var testDndCharacterDto = new DndCharacterDto() { CharacterName = "TestCharacter" };
            var testDndCharacter = new DndCharacter() { CharacterName = "TestCharacter" };

            mockUserRepo.Setup(repo => repo.GetUserByTokenAsync(It.IsAny<int>()))
                       .ReturnsAsync(testUser);

            mockCharacterRepo.Setup(repo => repo.CreateCharacterAsync(It.IsAny<DndCharacter>()))
                             .ReturnsAsync(testDndCharacter);

            mockIMapper.Setup(m => m.Map<DndCharacterDto, DndCharacter>(It.IsAny<DndCharacterDto>(), It.IsAny<DndCharacter>()))
                       .Callback((DndCharacterDto s, DndCharacter d) => d.CharacterName = s.CharacterName);

            var characterService = new CharacterSheetService(mockIMapper.Object, mockCharacterRepo.Object, mockUserRepo.Object);

            var result = await characterService.CreateCharacterAsync<DndCharacter, DndCharacterDto>(1, testDndCharacterDto);

            Assert.NotNull(result);
            Assert.Equal("TestCharacter", result.CharacterName);
        }

        [Fact]
        public async Task CreateCharacterAsync_ShouldFillAllFieldsCorrectly()
        {
            var mockUserRepo = new Mock<IUserRepository>();
            var mockCharacterRepo = new Mock<ICharacterSheetRepository>();
            var mockIMapper = new Mock<IMapper>();
            mockUserRepo.Setup(repo => repo.GetUserByTokenAsync(It.IsAny<int>()))
                .ReturnsAsync(new User() { UserToken = 1 });

            mockCharacterRepo.Setup(repo => repo.CreateCharacterAsync(It.IsAny<DndCharacter>()))
            .ReturnsAsync(() => null);
            mockIMapper.Setup(m => m.Map<DndCharacterDto, DndCharacter>(It.IsAny<DndCharacterDto>(), It.IsAny<DndCharacter>()))
                       .Callback((DndCharacterDto s, DndCharacter d) => d.CharacterName = s.CharacterName);

            var characterService = new CharacterSheetService(mockIMapper.Object, mockCharacterRepo.Object, mockUserRepo.Object);

            var dto = new DndCharacterDto()
            {
                CharacterName = "TestCharacter",
                Strength = 10
            };

            var result = await characterService.CreateCharacterAsync<DndCharacter, DndCharacterDto>(1, dto);

            Assert.NotNull(result);
            Assert.Equal("TestCharacter", result.CharacterName);
        }
        [Fact]
        public async Task GetCharacterSheetsFilteredBySystemTypeAsync_ShouldReturnCharacters_WhenSystemTypeMatches()
        {
            var mockCharacterRepo = new Mock<ICharacterSheetRepository>();
            var mockUserRepo = new Mock<IUserRepository>();
            var mockIMapper = new Mock<IMapper>();

            var systemTypeToTest = SystemType.DND;
            var characterList = new List<BaseCharacter>
            {
                new BaseCharacter { SystemType = systemTypeToTest },
                new BaseCharacter { SystemType = systemTypeToTest }
            };

            mockCharacterRepo.Setup(repo => repo.GetCharacterSheetsFilteredBySystemTypeAsync(systemTypeToTest))
                             .ReturnsAsync(characterList);

            var characterService = new CharacterSheetService(mockIMapper.Object, mockCharacterRepo.Object, mockUserRepo.Object);

            var result = await characterService.GetCharacterSheetsFilteredBySystemTypeAsync(systemTypeToTest);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, c => Assert.Equal(systemTypeToTest, c.SystemType));
        }
        [Fact]
        public async Task GetCharacterSheetsFilteredBySystemTypeAsync_ShouldReturnEmpty_WhenSystemTypeDoesNotMatch()
        {
            var mockCharacterRepo = new Mock<ICharacterSheetRepository>();
            var mockUserRepo = new Mock<IUserRepository>();
            var mockIMapper = new Mock<IMapper>();

            mockCharacterRepo.Setup(repo => repo.GetCharacterSheetsFilteredBySystemTypeAsync(It.IsAny<SystemType>()))
                             .ReturnsAsync(new List<BaseCharacter>());

            var characterService = new CharacterSheetService(mockIMapper.Object, mockCharacterRepo.Object, mockUserRepo.Object);

            var result = await characterService.GetCharacterSheetsFilteredBySystemTypeAsync(SystemType.Cthulu);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

    }

}
