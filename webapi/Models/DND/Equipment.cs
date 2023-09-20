using System.Text.Json.Serialization;
using webapi.Models.Generics;

namespace webapi.Models.DND
{
    public class Equipment : ICharacterAssociable<Equipment>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Weapon, Armor, Potion, etc.
        public string Description { get; set; } = string.Empty;
        public int Weight { get; set; }
        public int? Damage { get; set; } // Only for weapons
        public int? ArmorClass { get; set; } // Only for armor
        public string SpecialProperties { get; set; } = string.Empty;// Any special abilities or conditions
        [JsonIgnore]
        public List<CharacterAssociation<Equipment>> Associations { get; set; } = new List<CharacterAssociation<Equipment>>();
    }
}
