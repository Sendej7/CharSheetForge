using System.Text.Json.Serialization;
using webapi.DTO;
using webapi.Models.DND.Enums;
using webapi.Models.DND.Enums.DND;

namespace webapi.Models.DND
{
    public class DndCharacter : BaseCharacter
    {
        public CharacterClass Class { get; set; }
        public string Background { get; set; } = string.Empty;
        public CharacterRace Race { get; set; }
        public CharacterAlignment Alignment { get; set; }
        public string PlayerName { get; set; } = string.Empty;

        // Statistics
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        // Health and Defense
        public int HitPoints { get; set; }
        public int ArmorClass { get; set; }
        public int Speed { get; set; }
        public int Initiative { get; set; }

        // Inventory and Equipment
        public List<Equipment> Equipment { get; set; }
        public int Gold { get; set; }

        // Traits and Special Abilities
        public List<FeatureAndTrait> FeaturesAndTraits { get; set; }

        // Attacks and Spells
        public List<AttackAndSpellcasting> AttacksAndSpellcasting { get; set; }

        // Other Information
        public string Backstory { get; set; } = string.Empty;
        public List<AllyAndOrganization> AlliesAndOrganizations { get; set; }
        public string AdditionalNotes { get; set; } = string.Empty;
        public DndCharacter()
        {
            // List Initialization
            Equipment = new List<Equipment>();
            FeaturesAndTraits = new List<FeatureAndTrait>();
            AttacksAndSpellcasting = new List<AttackAndSpellcasting>();
            AlliesAndOrganizations = new List<AllyAndOrganization>();
        }
    }
}
