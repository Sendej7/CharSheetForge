using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Interfaces
{
    public interface ICharacterSheetRepository
    {
        Task<DNDCharacter?> GetDNDCardByIdAsync(int id);

        Task<IEnumerable<DNDCharacter>> GetAllDNDCharactersAsync();
        Task<IEnumerable<DNDCharacter>> GetAllDNDCharactersByFiltersAsync(int UserToken, SystemType? systemType = null);
    }
}
