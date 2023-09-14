using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Interfaces
{
    public interface ICharacterSheetService
    {
        Task<DndCharacter?> GetDNDCardByIdAsync(int id);

        Task<IEnumerable<DndCharacter>> GetAllDNDCharactersAsync();
        Task<IEnumerable<DndCharacter>> GetAllDNDCharactersByFiltersAsync(int UserToken, SystemType? systemType = null);

    }
}
