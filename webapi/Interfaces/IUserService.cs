using webapi.Models;

namespace webapi.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
    }
}
