using webapi.Models.DND.Enums;

namespace webapi.Models
{
    public interface ICharacter
    {
        int ID { get; set; }
        int UserToken { get; set; }
        int CardToken { get; set; }
        string CharacterName { get; set; }
        int Level { get; set; }
        SystemType SystemType { get; set; }
    }
}
