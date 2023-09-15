using webapi.DTO;
using webapi.Models;
using webapi.Models.DND;

namespace webapi.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByTokenAsync(int userToken);
        Task<User> CreateUserAsync(UserDto userDto);
        Task<bool?> UpdateUserAndCharacterRelationship(int userToken, int dndUserToken);
    }
}
