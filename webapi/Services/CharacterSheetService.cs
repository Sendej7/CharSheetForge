using webapi.Interfaces;
using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Services
{
    public class CharacterSheetService : ICharacterSheetService
    {
        private readonly ICharacterSheetRepository _characterRepository;

        public CharacterSheetService(ICharacterSheetRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }
        public async Task<DndCharacter> GetDNDCardByIdAsync(int id)
        {
            var dndCard = await _characterRepository.GetDNDCardByIdAsync(id);
            if (dndCard == null)
            {
                return null;
            }
            return dndCard;
        }
        public async Task<IEnumerable<DndCharacter>> GetAllDNDCharactersAsync()
        {
            return await _characterRepository.GetAllDNDCharactersAsync();
        }

        public async Task<IEnumerable<DndCharacter>> GetAllDNDCharactersByFiltersAsync(int UserToken, SystemType? systemType = null)
        {
            return await _characterRepository.GetAllDNDCharactersByFiltersAsync(UserToken, systemType);
        }

        
    }
}
