using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTO;
using webapi.Interfaces;
using webapi.Models;
using webapi.Models.DND;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public readonly ICharacterSheetService _characterSheetService;
        public UserController(IUserService userService, ICharacterSheetService characterSheetService)
        {
            _userService = userService;
            _characterSheetService = characterSheetService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var character = await _userService.GetUserByIdAsync(id);
            if (character == null)
                return NotFound();
            return Ok(character);
        }
        [HttpGet("ByToken/{userToken}")]
        public async Task<IActionResult> GetUserByUserTokenAsync(int userToken)
        {
            var character = await _userService.GetUserByTokenAsync(userToken);
            if(character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateNewUser(CreateUserRequest userRequest)
        {
            try
            {
                var user = await _userService.CreateUserAsync(userRequest.User);
                DndCharacter? dndCharacter = null;
                if(userRequest.DndCharacter != null)
                {
                    dndCharacter = await _characterSheetService.CreateCharacterAsync(userRequest.User.UserToken, userRequest.DndCharacter);
                }
                if(user!= null && dndCharacter != null)
                {
                    await _userService.UpdateUserAndCharacterRelationship(user.UserToken, dndCharacter.UserToken);
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
