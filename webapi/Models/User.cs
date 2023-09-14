using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Models
{
    public class User
    {
        public int ID { get; set; }
        public int UserToken { get; set; }
        public virtual ICollection<DndCharacter>? DNDCharacters { get; set; }

    }
}
