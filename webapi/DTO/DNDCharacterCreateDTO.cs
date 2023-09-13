using webapi.Models.DND.Enums.DND;

namespace webapi.DTO
{
    public class DNDCharacterCreateDTO
    {
        public string CharacterName { get; set; }
        public CharacterClass Class { get; set; }
        public int Level { get; set; }
        public string Background { get; set; }
        public int OwnerID { get; set; }
    }
}
