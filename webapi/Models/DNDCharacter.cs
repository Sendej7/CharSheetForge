using webapi.Models.Enums.DND;

namespace webapi.Models
{
    public class DNDCharacter : BaseCharacter
    {
        // Podstawowe informacje
        public string CharacterName { get; set; }
        public CharacterClass Class { get; set; }
        public int Level { get; set; }
        public string Background { get; set; }
        public CharacterRace Race { get; set; }
        public CharacterAlignment Alignment { get; set; }
        public string PlayerName { get; set; }

        // Statystyki
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        // Zdrowie i obrona
        public int HitPoints { get; set; }
        public int ArmorClass { get; set; }
        public int Speed { get; set; }
        public int Initiative { get; set; }

        // Inwentarz i wyposażenie
        public List<string> Equipment { get; set; }
        public int Gold { get; set; }

        // Cechy i umiejętności specjalne
        public List<string> FeaturesAndTraits { get; set; }

        // Ataki i czary
        public List<string> AttacksAndSpellcasting { get; set; }

        // Inne informacje
        public string Backstory { get; set; }
        public List<string> AlliesAndOrganizations { get; set; }
        public string AdditionalNotes { get; set; }

        public DNDCharacter()
        {
            // Inicjalizacja list
            Equipment = new List<string>();
            FeaturesAndTraits = new List<string>();
            AttacksAndSpellcasting = new List<string>();
            AlliesAndOrganizations = new List<string>();
        }
    }
}
