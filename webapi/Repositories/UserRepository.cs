using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.DTO;
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
            return await _context.BaseCharacters.Include(u => u.DNDCharacters).FirstOrDefaultAsync(u => u.ID == id);
        }
        public async Task<User?> GetUserByTokenAsync(int userToken)
        {
            return await _context.BaseCharacters.Include(u => u.DNDCharacters).Where(u => u.UserToken == userToken).FirstOrDefaultAsync();
        }
        public async Task<User> CreateUserAsync(User user)
        {
            _context.BaseCharacters.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAndCharacterRelationship(int userToken, int characterId)
        {
            var user = await _context.BaseCharacters.Where(u => u.UserToken == userToken).FirstOrDefaultAsync();

            if (user == null)
                return false;

            var character = await _context.DNDCharacters.Where(u => u.UserToken == user.ID).FirstOrDefaultAsync();

            if (character == null)
            {
                return false;
            }

            user.UserToken = userToken;

            character.UserToken = userToken;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
