namespace webapi.Models.Generics
{
    public interface ICharacterAssociable<T> where T : ICharacterAssociable<T>
    {
        int Id { get; set; }
        List<CharacterAssociation<T>> Associations { get; }
    }
}
