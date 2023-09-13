using webapi.DTO;
using webapi.Interfaces;
using webapi.Models.DND;

namespace webapi.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterService(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<IEnumerable<DNDCharacter>> GetAllCharactersAsync()
        {
            return await _characterRepository.GetAllCharactersAsync();
        }

        public async Task<DNDCharacter> GetCharacterByIdAsync(int id)
        {
            return await _characterRepository.GetCharacterByIdAsync(id);
        }

        public async Task CreateCharacterAsync(DNDCharacterCreateDTO characterCreateDto)
        {
            var newCharacter = new DNDCharacter
            {
                CharacterName = characterCreateDto.CharacterName,
            };

            await _characterRepository.AddCharacterAsync(newCharacter);
        }
    }
}
