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
using webapi.Models.DND.Enums;
using webapi.Models.DND;
using webapi.Services;
using Xunit;

namespace webapiUnitTests.ServicesUnitTests
{
    public class CharacterSheetServiceUT
    {

        [Fact]
        public void GetDNDCardByIdAsync_ShouldReturnCharacter_WhenIdIsValid()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "DNDCharacter")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            mockRepo.Setup(repo => repo.GetDNDCardByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => new DNDCharacter
                {
                    ID = 1,
                    UserToken = id,
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
                });
            var characterService = new CharacterSheetService(mockRepo.Object);
            var character = characterService.GetDNDCardByIdAsync(1);

            Assert.NotNull(character);
            Assert.Equal(1, character.Result.ID);
            Assert.Equal(1, character.Result.UserToken);
        }
        [Fact]
        public void GetDNDCardByIdAsync_MustReturnException_WhenIdIsNotValidOrThereIsNoCharacter()
        {
            var options = new DbContextOptionsBuilder<CharSheetContext>().UseInMemoryDatabase(databaseName: "BaseCharacters")
            .Options;

            var mockRepo = new Mock<ICharacterSheetRepository>();
            mockRepo.Setup(repo => repo.GetDNDCardByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => null);
            var characterService = new CharacterSheetService(mockRepo.Object);
            var character = characterService.GetDNDCardByIdAsync(1);

            Assert.ThrowsAsync<Exception>(() => characterService.GetDNDCardByIdAsync(0));
        }
    }

}