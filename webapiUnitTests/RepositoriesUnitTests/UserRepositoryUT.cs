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
    }
}
