using System.Text.Json.Serialization;
using webapi.DTO;
using webapi.Models.DND.Enums;
using webapi.Models.DND.Enums.DND;

namespace webapi.Models.DND
{
    public class DndCharacter : DndCharacterDto
    {
        public int ID { get; set; }  // Primary key
        public int UserToken { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; } 
        public SystemType SystemType { get; set; } = SystemType.DND;

        // Basic Information
    }
}
