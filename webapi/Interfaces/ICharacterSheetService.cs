using webapi.DTO;
using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Interfaces
{
    public interface ICharacterSheetService
    {
        Task<DndCharacter?> GetDNDCardByIdAsync(int id);
        Task<DndCharacter> CreateCharacterAsync(int userToken, DndCharacterDto dndCharacterDto);

        Task<IEnumerable<DndCharacter>> GetAllDNDCharactersAsync();
        Task<IEnumerable<DndCharacter>> GetAllDNDCharactersByFiltersAsync(int UserToken, SystemType? systemType = null);

    }
}
