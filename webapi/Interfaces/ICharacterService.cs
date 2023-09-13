using webapi.DTO;
using webapi.Models.DND;

namespace webapi.Interfaces
{
    public interface ICharacterService
    {
        Task<IEnumerable<DNDCharacter>> GetAllCharactersAsync();
        Task<DNDCharacter> GetCharacterByIdAsync(int id);
        Task CreateCharacterAsync(DNDCharacterCreateDTO characterCreateDto);
    }
}
