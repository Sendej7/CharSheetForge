using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Interfaces;
using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Repositories
{
    public class CharacterSheetRepository : ICharacterSheetRepository
    {
        private readonly CharSheetContext _context;

        public CharacterSheetRepository(CharSheetContext context)
        {
            _context = context;
        }

        public async Task<DNDCharacter?> GetDNDCardByIdAsync(int id)
        {
            return await _context.DNDCharacters.FindAsync(id);
        }
        public async Task<IEnumerable<DNDCharacter>> GetAllDNDCharactersAsync()
        {
            return await _context.DNDCharacters.ToListAsync();
        }

        public async Task<IEnumerable<DNDCharacter>> GetAllDNDCharactersByFiltersAsync(int UserToken, SystemType? systemType = null)
        {
            var query = _context.DNDCharacters.AsQueryable();

            if (systemType.HasValue)
            {
                query = query.Where(c => c.SystemType == systemType.Value);
            }

            query = query.Where(c => c.UserToken == UserToken);

            return await query.ToListAsync();
        }


    }
}
