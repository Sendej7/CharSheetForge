using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Interfaces
{
    public interface ICharacterSheetService
    {
        Task<DNDCharacter> GetDNDCardByIdAsync(int id);

        Task<IEnumerable<DNDCharacter>> GetAllDNDCharactersAsync();
        Task<IEnumerable<DNDCharacter>> GetAllDNDCharactersByFiltersAsync(int BaseCharacterID, SystemType? systemType = null);

    }
}
