using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Models.DND.Enums.DND;
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
                    CardToken = 1,
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

                    Gold = 100,
                    Backstory = "Backstory goes here",

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

                    Gold = 150,

                    Backstory = "A former soldier who seeks glory and honor.",

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

                    Gold = 50,

                    Backstory = "Raised in the streets, learned to fend for herself at an early age.",

                    AdditionalNotes = "Has a pet mouse named Squeaky"
                }

            };
        }
        


    }
}
