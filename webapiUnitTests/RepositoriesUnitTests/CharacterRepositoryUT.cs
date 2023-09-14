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
using webapi.Models.DND;
using webapi.Models.DND.Enums;
using webapi.Repositories;
using webapi.Services;
using Xunit;

namespace webapiUnitTests.RepositoriesUnitTests
{
    public class CharacterRepositoryUT
    {
        [Fact]
        public async Task GetDNDCardByIdAsync_ShouldReturnCharacter_WhenIdIsValid()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new CharSheetContext(options))
            {
                context.DNDCharacters.AddRange(Helpers.Characters());
                context.SaveChanges();
            }
            DndCharacter? character = new DndCharacter();
            using (var context = new CharSheetContext(options))
            {
                var characterSheetRepository = new CharacterSheetRepository(context);
                character = await characterSheetRepository.GetDNDCardByIdAsync(1);
            }

            // Assert
            Assert.NotNull(character);
            Assert.Equal(1, character.ID);
            Assert.Equal(1, character.UserToken);
        }
        [Fact]
        public void GetDNDCardByIdAsync_MustReturnException_WhenIdIsNotValidOrThereIsNoCharacter()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            DndCharacter? character = new DndCharacter();
            using (var context = new CharSheetContext(options))
            {
                var characterSheetRepository = new CharacterSheetRepository(context);
                Assert.ThrowsAsync<Exception>(() => characterSheetRepository.GetDNDCardByIdAsync(1));
            }
        }
        [Fact]
        public async Task GetAllDNDCharactersAsync_ShouldReturnAllCharacterSheetsAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new CharSheetContext(options))
            {
                context.DNDCharacters.AddRange(Helpers.Characters());
                context.SaveChanges();
            }
            IEnumerable<DndCharacter> characters = new List<DndCharacter>();
            using (var context = new CharSheetContext(options))
            {
                var characterSheetRepository = new CharacterSheetRepository(context);
                characters = await characterSheetRepository.GetAllDNDCharactersAsync();
            }

            Assert.NotNull(characters);
            Assert.Equal(3, characters.Count());
            Assert.Equal(1, characters.First().ID);
        }
        [Fact]
        public async Task GetAllDNDCharactersAsync_ReturnsEmptyList_WhenNoCharactersExist()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            IEnumerable<DndCharacter?> characters;
            using (var context = new CharSheetContext(options))
            {
                var characterSheetRepository = new CharacterSheetRepository(context);
                characters = await characterSheetRepository.GetAllDNDCharactersAsync();
            }

            Assert.NotNull(characters);
            Assert.Empty(characters);
        }
        [Fact]
        public async Task GetAllDNDCharactersByUserToken_ShouldReturnAllMatchingCharacterSheetsAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new CharSheetContext(options))
            {
                context.DNDCharacters.AddRange(Helpers.Characters());
                context.SaveChanges();
            }
            IEnumerable<DndCharacter> characters;
            using (var context = new CharSheetContext(options))
            {
                var characterSheetRepository = new CharacterSheetRepository(context);
                characters = await characterSheetRepository.GetAllDNDCharactersByFiltersAsync(1);
            }

            Assert.NotNull(characters);
            Assert.Single(characters);
            Assert.Equal(1, characters.First().ID);
        }
        [Fact]
        public async Task GetAllDNDCharactersByUserTokenAndSystemType_ShouldReturnAllMatchingCharacterSheetsAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new CharSheetContext(options))
            {
                context.DNDCharacters.AddRange(Helpers.Characters());
                context.SaveChanges();
            }
            IEnumerable<DndCharacter> characters;
            using (var context = new CharSheetContext(options))
            {
                var characterSheetRepository = new CharacterSheetRepository(context);
                characters = await characterSheetRepository.GetAllDNDCharactersByFiltersAsync(1, SystemType.DND);
            }

            Assert.NotNull(characters);
            Assert.Single(characters);
            Assert.Equal(1, characters.First().ID);
        }
        [Fact]
        public async Task GetAllDNDCharactersByUserTokenAndSystemType_ShouldReturnEmptyListWhenNoMatchesAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            IEnumerable<DndCharacter> characters;
            using (var context = new CharSheetContext(options))
            {
                var characterSheetRepository = new CharacterSheetRepository(context);
                characters = await characterSheetRepository.GetAllDNDCharactersByFiltersAsync(1, SystemType.DND);
            }

            Assert.NotNull(characters);
            Assert.Empty(characters);
        }
        [Fact]
        public async Task GetAllDNDCharactersByUserTokenAndNoSystemType_ShouldReturnEmptyListAsync()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new CharSheetContext(options))
            {
                context.DNDCharacters.AddRange(Helpers.Characters());
                context.SaveChanges();
            }
            IEnumerable<DndCharacter> characters;
            using (var context = new CharSheetContext(options))
            {
                var characterSheetRepository = new CharacterSheetRepository(context);
                characters = await characterSheetRepository.GetAllDNDCharactersByFiltersAsync(1, SystemType.Cthulu);
            }

            Assert.NotNull(characters);
            Assert.Empty(characters);
        }
    }
}
