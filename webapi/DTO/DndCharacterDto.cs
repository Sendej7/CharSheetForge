using webapi.Models.DND;
using webapi.Models.DND.Enums.DND;
using webapi.Models.Generics;

namespace webapi.DTO
{
    public class DndCharacterDto
    {
        public string CharacterName { get; set; } = string.Empty;
        public CharacterClass Class { get; set; }
        public int Level { get; set; }
        public string Background { get; set; } = string.Empty;
        public CharacterRace Race { get; set; }
        public CharacterAlignment Alignment { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public int CardToken { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public int HitPoints { get; set; }
        public int ArmorClass { get; set; }
        public int Speed { get; set; }
        public int Initiative { get; set; }

        public int Gold { get; set; }

        public string Backstory { get; set; } = string.Empty;
        public string AdditionalNotes { get; set; } = string.Empty;

    }

}
