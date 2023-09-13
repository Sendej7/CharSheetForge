using webapi.Data;
using webapi.Interfaces;
using webapi.Models;

namespace webapi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CharSheetContext _context;

        public UserRepository(CharSheetContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.BaseCharacters.FindAsync(id);
        }
    }
}
