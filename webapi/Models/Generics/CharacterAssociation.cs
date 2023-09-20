using webapi.Models.DND;

namespace webapi.Models.Generics
{
    public class CharacterAssociation<T> where T : ICharacterAssociable<T>
    {
        public int DndCharacterId { get; set; }
        public DndCharacter DndCharacter { get; set; }
        public int AssociableId { get; set; }
        public T Associable { get; set; }
    }
}
