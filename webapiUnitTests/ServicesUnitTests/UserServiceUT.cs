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
        [Fact]
        public void  GetUserByIdAsync_ShouldReturnUser_WhenIdIsValidAndReturnAllCharacterSheetsConnectedToUser()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "User")
            .Options;

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => new User
            {
                ID = id,
                UserToken = 323,
                DNDCharacters = new List<DndCharacter>
                {
                    new DndCharacter
                    {
                        ID = 1,
                        UserToken = 323,
                        SystemType = SystemType.DND,
                        CharacterName = "Test Character",
                        Class = CharacterClass.Wizard,
                        Level = 5,
                        Background = "Sage",
                        Race = CharacterRace.Human,
                        Alignment = CharacterAlignment.LawfulGood,
                        PlayerName = "John",
                        Strength = 10,
                        Dexterity = 12,
                        Constitution = 14,
                        Intelligence = 16,
                        Wisdom = 10,
                        Charisma = 8,
                        HitPoints = 35,
                        ArmorClass = 12,
                        Speed = 30,
                        Initiative = 2,
                        Equipment = new List<Equipment>(),
                        Gold = 100,
                        FeaturesAndTraits = new List<FeatureAndTrait>(),
                        AttacksAndSpellcasting = new List<AttackAndSpellcasting>(),
                        Backstory = "Backstory goes here",
                        AlliesAndOrganizations = new List<AllyAndOrganization>(),
                        AdditionalNotes = "Additional notes go here"
                    },
                    new DndCharacter
                    {
                        ID = 2,
                        UserToken = 323,
                        SystemType = SystemType.DND,
                        CharacterName = "Test Character",
                        Class = CharacterClass.Wizard,
                        Level = 5,
                        Background = "Sage",
                        Race = CharacterRace.Human,
                        Alignment = CharacterAlignment.LawfulGood,
                        PlayerName = "John",
                        Strength = 10,
                        Dexterity = 12,
                        Constitution = 14,
                        Intelligence = 16,
                        Wisdom = 10,
                        Charisma = 8,
                        HitPoints = 35,
                        ArmorClass = 12,
                        Speed = 30,
                        Initiative = 2,
                        Equipment = new List<Equipment>(),
                        Gold = 100,
                        FeaturesAndTraits = new List<FeatureAndTrait>(),
                        AttacksAndSpellcasting = new List<AttackAndSpellcasting>(),
                        Backstory = "Backstory goes here",
                        AlliesAndOrganizations = new List<AllyAndOrganization>(),
                        AdditionalNotes = "Additional notes go here"
                    },
                    new DndCharacter
                    {
                        ID = 3,
                        UserToken = 323,
                        SystemType = SystemType.DND,
                        CharacterName = "Test Character",
                        Class = CharacterClass.Wizard,
                        Level = 5,
                        Background = "Sage",
                        Race = CharacterRace.Human,
                        Alignment = CharacterAlignment.LawfulGood,
                        PlayerName = "John",
                        Strength = 10,
                        Dexterity = 12,
                        Constitution = 14,
                        Intelligence = 16,
                        Wisdom = 10,
                        Charisma = 8,
                        HitPoints = 35,
                        ArmorClass = 12,
                        Speed = 30,
                        Initiative = 2,
                        Equipment = new List<Equipment>(),
                        Gold = 100,
                        FeaturesAndTraits = new List<FeatureAndTrait>(),
                        AttacksAndSpellcasting = new List<AttackAndSpellcasting>(),
                        Backstory = "Backstory goes here",
                        AlliesAndOrganizations = new List<AllyAndOrganization>(),
                        AdditionalNotes = "Additional notes go here"
                    },
                }
            });

            var UserService = new UserService(mockRepo.Object);
            var User = UserService.GetUserByIdAsync(1);

            Assert.NotNull(User);
            Assert.Equal(1, User.Result.ID);
            Assert.Equal(3, User.Result.DNDCharacters.Count());
            Assert.Equal(1, User.Result.DNDCharacters.First().ID);
            Assert.Equal(323, User.Result.DNDCharacters.First().UserToken);
        }
    }
}
