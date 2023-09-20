using System.Text.Json.Serialization;
using webapi.Models.Generics;

namespace webapi.Models.DND
{
    public class FeatureAndTrait : ICharacterAssociable<FeatureAndTrait>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty; // Class, Race, etc.
        public int LevelRequired { get; set; }
        [JsonIgnore]
        public List<CharacterAssociation<FeatureAndTrait>> Associations { get; set; } = new List<CharacterAssociation<FeatureAndTrait>>();
    }
}
