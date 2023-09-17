using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Interfaces
{
    public interface ICharacterSheetRepository
    {
        Task<BaseCharacter?> GetDNDCardByIdAsync(int id);

        Task<IEnumerable<BaseCharacter>> GetAllDNDCharactersAsync();
        Task<IEnumerable<BaseCharacter>> GetAllDNDCharactersByFiltersAsync(int UserToken, SystemType? systemType = null);
        Task<T> CreateCharacterAsync<T>(T character) where T : BaseCharacter;
    }
}
