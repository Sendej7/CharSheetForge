using webapi.Models.Enums;

namespace webapi.Models
{
    public class BaseCharacter
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public SystemType SystemType { get; set; }
        public int OwnerID { get; set; }

    }
}
