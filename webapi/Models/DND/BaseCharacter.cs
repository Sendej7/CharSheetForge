using webapi.Models.DND.Enums;

namespace webapi.Models.DND
{
    public class BaseCharacter
    {
        public int ID { get; set; }
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public SystemType SystemType { get; set; }
        public int OwnerID { get; set; }

    }
}
