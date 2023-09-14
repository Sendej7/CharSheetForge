using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            try
            {
                var character = await _userService.GetUserByIdAsync(id);
                if (character == null)
                    return NotFound();
                return Ok(character);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
