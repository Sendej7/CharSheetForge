using webapi.DTO;
using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Interfaces
{
    public interface ICharacterSheetService
    {
        Task<BaseCharacter?> GetDNDCardByIdAsync(int id);
        Task<T> CreateCharacterAsync<T, TDto>(int userToken, TDto dto) where T : BaseCharacter, new();

        Task<IEnumerable<BaseCharacter>> GetAllDNDCharactersAsync();
        Task<IEnumerable<BaseCharacter>> GetAllDNDCharactersByFiltersAsync(int UserToken, SystemType? systemType = null);

    }
}
