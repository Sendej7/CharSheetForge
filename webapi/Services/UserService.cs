using webapi.Interfaces;
using webapi.Models;

namespace webapi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            var character = await _userRepository.GetUserByIdAsync(id);
            if (character == null)
            {
                throw new Exception("Character not found");
            }
            return character;
        }
    }
}
