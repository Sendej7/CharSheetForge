using AutoMapper;
using webapi.DTO;
using webapi.Interfaces;
using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Services
{
    public class CharacterSheetService : ICharacterSheetService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICharacterSheetRepository _characterRepository;

        public CharacterSheetService(IMapper mapper, ICharacterSheetRepository characterRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _characterRepository = characterRepository;
            _userRepository = userRepository;
        }
        public async Task<BaseCharacter?> GetDNDCardByIdAsync(int id)
        {
            var dndCard = await _characterRepository.GetDNDCardByIdAsync(id);
            if (dndCard == null)
            {
                return null;
            }
            return dndCard;
        }
        public async Task<IEnumerable<BaseCharacter>> GetAllDNDCharactersAsync()
        {
            return await _characterRepository.GetAllDNDCharactersAsync();
        }

        public async Task<IEnumerable<BaseCharacter>> GetAllDNDCharactersByFiltersAsync(int UserToken, SystemType? systemType = null)
        {
            if(systemType == null)
            {
                return await _characterRepository.GetAllDNDCharactersByFiltersAsync(UserToken);
            }
            return await _characterRepository.GetAllDNDCharactersByFiltersAsync(UserToken, systemType);
        }
        public async Task<T> CreateCharacterAsync<T, TDto>(int userToken, TDto dto) where T : BaseCharacter, new()
        {
            var isUserCreated = await _userRepository.GetUserByTokenAsync(userToken) ?? throw new Exception("User Not Found.");
            T character = new()
            {
                UserToken = userToken,
                User = isUserCreated,
            };
            _mapper.Map(dto, character); // AutoMapper robi swoje

            await _characterRepository.CreateCharacterAsync(character);
            return character;
        }
    }
}
