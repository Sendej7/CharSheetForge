using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Interfaces;
using webapi.Models.DND;

namespace webapi.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly CharSheetContext _context;

        public CharacterRepository(CharSheetContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DNDCharacter>> GetAllCharactersAsync()
        {
            return await _context.DNDCharacter.ToListAsync();
        }

        public async Task<DNDCharacter> GetCharacterByIdAsync(int id)
        {
            return await _context.DNDCharacter.FindAsync(id);
        }

        public async Task AddCharacterAsync(DNDCharacter character)
        {
            await _context.DNDCharacter.AddAsync(character);
            await _context.SaveChangesAsync();
        }
    }
}
