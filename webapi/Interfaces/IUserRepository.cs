using webapi.Models;

namespace webapi.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int id);
    }
}
