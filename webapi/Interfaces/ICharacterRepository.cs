using webapi.Models.DND;

namespace webapi.Interfaces
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<DNDCharacter>> GetAllCharactersAsync();
        Task<DNDCharacter> GetCharacterByIdAsync(int id);
        Task AddCharacterAsync(DNDCharacter character);
    }
}
