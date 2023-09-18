using webapi.DTO;
using webapi.Models;
using webapi.Models.DND;

namespace webapi.Interfaces
{
    public interface ICharacterSheetService
    {
        Task<BaseCharacter?> GetCharacterSheetByIdAsync(int id);
        Task<IEnumerable<BaseCharacter>> GetCharacterSheetsFilteredBySystemTypeAsync(SystemType systemType);

        Task<T> CreateCharacterAsync<T, TDto>(int userToken, TDto dto) where T : BaseCharacter, new();

        Task<IEnumerable<BaseCharacter>> GetAllCharacterSheetsAsync();
        Task<IEnumerable<BaseCharacter>> GetAllCharacterSheetsByFiltersAsync(int userToken, SystemType? systemType = null);

    }
}
