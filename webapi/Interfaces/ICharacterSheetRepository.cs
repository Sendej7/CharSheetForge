using webapi.Models;
using webapi.Models.DND;

namespace webapi.Interfaces
{
    public interface ICharacterSheetRepository
    {
        Task<BaseCharacter?> GetCharacterSheetByIdAsync(int id);

        Task<IEnumerable<BaseCharacter>> GetAllCharacterSheetsAsync();
        Task<IEnumerable<BaseCharacter>> GetCharacterSheetsFilteredBySystemTypeAsync(SystemType systemType);
        Task<IEnumerable<BaseCharacter>> GetAllCharacterSheetsByFiltersAsync(int UserToken, SystemType? systemType = null);
        Task<T> CreateCharacterAsync<T>(T character) where T : BaseCharacter;
    }
}
