using webapi.DTO;
using webapi.Interfaces;
using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Services
{
    public class CharacterSheetService : ICharacterSheetService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICharacterSheetRepository _characterRepository;

        public CharacterSheetService(ICharacterSheetRepository characterRepository, IUserRepository userRepository)
        {
            _characterRepository = characterRepository;
            _userRepository = userRepository;
        }
        public async Task<DndCharacter?> GetDNDCardByIdAsync(int id)
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
            if(systemType == null)
            {
                return await _characterRepository.GetAllDNDCharactersByFiltersAsync(UserToken);
            }
            return await _characterRepository.GetAllDNDCharactersByFiltersAsync(UserToken, systemType);
        }

        public async Task<DndCharacter> CreateCharacterAsync(int userToken, DndCharacterDto dndCharacterDto)
        {
            var isUserCreated = await _userRepository.GetUserByTokenAsync(userToken) ?? throw new Exception("User Not Found.");
            DndCharacter dndCharacter = new()
            {
                // User Information
                UserToken = userToken,
                User = isUserCreated,

                // Basic Information
                CharacterName = dndCharacterDto.CharacterName,
                Class = dndCharacterDto.Class,
                Level = dndCharacterDto.Level,
                Background = dndCharacterDto.Background,
                Race = dndCharacterDto.Race,
                Alignment = dndCharacterDto.Alignment,
                PlayerName = dndCharacterDto.PlayerName,

                // Statistics
                Strength = dndCharacterDto.Strength,
                Dexterity = dndCharacterDto.Dexterity,
                Constitution = dndCharacterDto.Constitution,
                Intelligence = dndCharacterDto.Intelligence,
                Wisdom = dndCharacterDto.Wisdom,
                Charisma = dndCharacterDto.Charisma,

                // Health and Defense
                HitPoints = dndCharacterDto.HitPoints,
                ArmorClass = dndCharacterDto.ArmorClass,
                Speed = dndCharacterDto.Speed,
                Initiative = dndCharacterDto.Initiative,

                // Inventory and Equipment
                Equipment = dndCharacterDto.Equipment,
                Gold = dndCharacterDto.Gold,

                // Traits and Special Abilities
                FeaturesAndTraits = dndCharacterDto.FeaturesAndTraits,

                // Attacks and Spells
                AttacksAndSpellcasting = dndCharacterDto.AttacksAndSpellcasting,

                // Other Information
                Backstory = dndCharacterDto.Backstory,
                AlliesAndOrganizations = dndCharacterDto.AlliesAndOrganizations,
                AdditionalNotes = dndCharacterDto.AdditionalNotes
            };

            await _characterRepository.CreateCharacterAsync(dndCharacter);
            return dndCharacter;
        }
    }
}
