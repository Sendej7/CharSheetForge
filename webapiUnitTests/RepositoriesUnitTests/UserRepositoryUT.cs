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
using webapi.Repositories;
using Xunit;

namespace webapiUnitTests.RepositoriesUnitTests
{
    public class UserRepositoryUT
    {
        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "User")
            .Options;

            using(var context = new CharSheetContext(options))
            {
                context.BaseCharacters.Add(new User { ID = 1, UserToken = 1 });
                context.SaveChanges();
            }
            User? retrievedUser;
            using (var context = new CharSheetContext(options))
            {
                var userRepository = new UserRepository(context);
                retrievedUser = await userRepository.GetUserByIdAsync(1);
            }

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(1, retrievedUser?.ID);
            Assert.Equal(1, retrievedUser?.UserToken);
        }
        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            User? retrievedUser;
            using(var context = new CharSheetContext(options))
            {
                var userRepository = new UserRepository(context);
                retrievedUser = await userRepository.GetUserByIdAsync(1);
            }
            Assert.Null(retrievedUser);
        }
        [Fact]
        public async Task UpdateUserAndCharacterRelationship_ShouldReturnTrue_WhenBothExist()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new CharSheetContext(options))
            {
                context.BaseCharacters.Add(new User { ID = 1, UserToken = 1 });
                context.DNDCharacters.Add(new DndCharacter { ID = 1, UserToken = 1, CharacterName = "John" });
                await context.SaveChangesAsync();
            }

            bool result;
            using (var context = new CharSheetContext(options))
            {
                var userRepository = new UserRepository(context);  
                result = await userRepository.UpdateUserAndCharacterRelationship(1, 1);
            }

            Assert.True(result);
        }
        [Fact]
        public async Task UpdateUserAndCharacterRelationship_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            bool result;
            using (var context = new CharSheetContext(options))
            {
                var userRepository = new UserRepository(context); 
                result = await userRepository.UpdateUserAndCharacterRelationship(1, 1);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateUserAndCharacterRelationship_ShouldReturnFalse_WhenCharacterDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new CharSheetContext(options))
            {
                context.BaseCharacters.Add(new User { ID = 1, UserToken = 1 });
                await context.SaveChangesAsync();
            }

            bool result;
            using (var context = new CharSheetContext(options))
            {
                var userRepository = new UserRepository(context); 
                result = await userRepository.UpdateUserAndCharacterRelationship(1, 1);
            }

            // Assert
            Assert.False(result);
        }
    }
}
