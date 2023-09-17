using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Models.DND.Enums.DND;
using webapi.Models.DND.Enums;
using webapi.Models.DND;
using webapi.Models;

namespace webapiUnitTests
{
    internal class Helpers
    {
        public static List<BaseCharacter> Characters()
        {
            return new List<BaseCharacter>
            {
                new DndCharacter
                {
                    ID = 1,
                    UserToken = 1,
                    User = new User { ID = 1, UserToken = 1 },
                    SystemType = SystemType.DND,
                    CharacterName = "Test Character 1",
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
                    Equipment = new List<Equipment>
                    {
                        new Equipment { Id = 1, Name = "Wand", Type = "Weapon", Description = "Magic wand", Weight = 1 },
                        new Equipment { Id = 2, Name = "Robe", Type = "Armor", Description = "Mage robe", Weight = 2, ArmorClass = 1 }
                    },
                    Gold = 100,
                    FeaturesAndTraits = new List<FeatureAndTrait>
                    {
                        new FeatureAndTrait { Id = 1, Name = "Arcane Recovery", Description = "Recover spell slots", Source = "Class", LevelRequired = 1 }
                    },
                    AttacksAndSpellcasting = new List<AttackAndSpellcasting>
                    {
                        new AttackAndSpellcasting { Id = 1, Name = "Fireball", Type = "Spell", Range = 150, Damage = 8, DamageType = "Fire", Components = "V, S, M", Duration = "Instantaneous" }
                    },
                    Backstory = "Backstory goes here",
                    AlliesAndOrganizations = new List<AllyAndOrganization>
                    {
                        new AllyAndOrganization { Id = 1, Name = "Mages Guild", Type = "Organization", Description = "Guild of mages", Relationship = "Member", TrustLevel = 5 }
                    },
                    AdditionalNotes = "Additional notes go here"
                },
                new DndCharacter
                {
                    ID = 2,
                    UserToken = 2,
                    User = new User { ID = 2, UserToken = 2 },
                    SystemType = SystemType.DND,
                    CharacterName = "Test Character 2",
                    Class = CharacterClass.Fighter,
                    Level = 4,
                    Background = "Soldier",
                    Race = CharacterRace.Dwarf,
                    Alignment = CharacterAlignment.LawfulNeutral,
                    PlayerName = "Emily",
                    Strength = 16,
                    Dexterity = 12,
                    Constitution = 14,
                    Intelligence = 10,
                    Wisdom = 12,
                    Charisma = 10,
                    HitPoints = 40,
                    ArmorClass = 18, // Assuming plate armor and shield
                    Speed = 25, // Dwarves are generally slower
                    Initiative = 1,
                    Equipment = new List<Equipment>
                    {
                        new Equipment { Id = 5, Name = "Longsword", Type = "Weapon", Description = "A trusty longsword", Weight = 3, Damage = 8 },
                        new Equipment { Id = 6, Name = "Plate Armor", Type = "Armor", Description = "Heavy plate armor", Weight = 65, ArmorClass = 8 },
                        new Equipment { Id = 7, Name = "Shield", Type = "Armor", Description = "A sturdy wooden shield", Weight = 6, ArmorClass = 2 }
                    },
                    Gold = 150,
                    FeaturesAndTraits = new List<FeatureAndTrait>
                    {
                        new FeatureAndTrait { Id = 3, Name = "Second Wind", Description = "Regain hit points in battle", Source = "Class", LevelRequired = 1 },
                        new FeatureAndTrait { Id = 4, Name = "Action Surge", Description = "Extra action on your turn", Source = "Class", LevelRequired = 2 }
                    },
                    AttacksAndSpellcasting = new List<AttackAndSpellcasting>
                    {
                        new AttackAndSpellcasting { Id = 3, Name = "Longsword Strike", Type = "Attack", Range = 5, Damage = 8, DamageType = "Slashing" }
                    },
                    Backstory = "A former soldier who seeks glory and honor.",
                    AlliesAndOrganizations = new List<AllyAndOrganization>
                    {
                        new AllyAndOrganization { Id = 3, Name = "Military Unit", Type = "Organization", Description = "A military unit from the kingdom", Relationship = "Former member", TrustLevel = 4 }
                    },
                    AdditionalNotes = "Holds a personal grudge against orcs"
                },
                new DndCharacter
                {
                    ID = 4,
                    UserToken = 4,
                    User = new User { ID = 3, UserToken = 4 },
                    SystemType = SystemType.DND,
                    CharacterName = "Test Character 4",
                    Class = CharacterClass.Rogue,
                    Level = 3,
                    Background = "Criminal",
                    Race = CharacterRace.Elf,
                    Alignment = CharacterAlignment.ChaoticNeutral,
                    PlayerName = "Jane",
                    Strength = 8,
                    Dexterity = 16,
                    Constitution = 12,
                    Intelligence = 14,
                    Wisdom = 10,
                    Charisma = 12,
                    HitPoints = 24,
                    ArmorClass = 14,
                    Speed = 35, // Elves are generally faster
                    Initiative = 3, // High dexterity increases initiative
                    Equipment = new List<Equipment>
                    {
                        new Equipment { Id = 3, Name = "Dagger", Type = "Weapon", Description = "A sharp dagger", Weight = 1, Damage = 4 },
                        new Equipment { Id = 4, Name = "Leather Armor", Type = "Armor", Description = "Light leather armor", Weight = 10, ArmorClass = 2 }
                    },
                    Gold = 50,
                    FeaturesAndTraits = new List<FeatureAndTrait>
                    {
                        new FeatureAndTrait { Id = 2, Name = "Sneak Attack", Description = "Extra damage when you have advantage", Source = "Class", LevelRequired = 1 }
                    },
                    AttacksAndSpellcasting = new List<AttackAndSpellcasting>
                    {
                        new AttackAndSpellcasting { Id = 2, Name = "Shortbow", Type = "Attack", Range = 80, Damage = 6, DamageType = "Piercing" }
                    },
                    Backstory = "Raised in the streets, learned to fend for herself at an early age.",
                    AlliesAndOrganizations = new List<AllyAndOrganization>
                    {
                        new AllyAndOrganization { Id = 2, Name = "Thieves Guild", Type = "Organization", Description = "A guild of skilled thieves", Relationship = "Member", TrustLevel = 3 }
                    },
                    AdditionalNotes = "Has a pet mouse named Squeaky"
                }

            };
        }
        


    }
}
