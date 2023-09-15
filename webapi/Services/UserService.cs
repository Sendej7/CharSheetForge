using webapi.DTO;
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
        
        public async Task<User?> GetUserByIdAsync(int id)
        {
            var character = await _userRepository.GetUserByIdAsync(id);
            if (character == null)
            {
                return null;
            }
            return character;
        }
        public async Task<User?> GetUserByTokenAsync(int userToken)
        {
            var character = await _userRepository.GetUserByTokenAsync(userToken);
            if(character == null)
            {
                return null;
            }
            return character;
        }
        public async Task<User> CreateUserAsync(UserDto userDto)
        {
            var character = await _userRepository.GetUserByTokenAsync(userDto.UserToken);
            if (character != null)
            {
                throw new Exception("User already registered.");
            }
            User user = new()
            {
                UserToken = userDto.UserToken
            };
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<bool?> UpdateUserAndCharacterRelationship(int userToken, int dndUserToken)
        {
            return await _userRepository.UpdateUserAndCharacterRelationship(userToken, dndUserToken);
        }
    }
}
