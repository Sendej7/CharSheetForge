using webapi.DTO;
using webapi.Models;

namespace webapi.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByTokenAsync(int userToken);

        Task<User> CreateUserAsync(User user);
        Task<bool> UpdateUserAndCharacterRelationship(int userToken, int dndUserToken);
    }
}
