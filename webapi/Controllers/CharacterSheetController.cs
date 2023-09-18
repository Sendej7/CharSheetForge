using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTO;
using webapi.Interfaces;
using webapi.Models;
using webapi.Models.DND;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterSheetController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICharacterSheetService _characterService;
        private readonly IUserService _userService;

        public CharacterSheetController(IMapper mapper, ICharacterSheetService characterService, IUserService userService)
        {
            _mapper = mapper;
            _characterService = characterService;
            _userService = userService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacterSheetByIdAsync(int id)
        {
            var dndCard = await _characterService.GetCharacterSheetByIdAsync(id);
            if (dndCard == null)
                return NotFound();
            return Ok(dndCard);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCharacterSheetsAsync()
        {
            var dndCharacters = await _characterService.GetAllCharacterSheetsAsync();
            return Ok(dndCharacters);
        }
        [HttpGet("all/system-type-filter")]
        public async Task<IActionResult> GetCharacterSheetsFilteredBySystemTypeAsync([FromQuery] SystemType systemType)
        {
            var dndCard = await _characterService.GetCharacterSheetsFilteredBySystemTypeAsync(systemType);
            if (dndCard == null)
                return NotFound();
            return Ok(dndCard);
        }
        [HttpGet("all/user-token-system-type-filter")]
        public async Task<IActionResult> GetAllCharacterSheetsByFiltersAsync([FromQuery] int userToken, [FromQuery] SystemType? systemType = null)
        {
            var filteredDNDCharacters = await _characterService.GetAllCharacterSheetsByFiltersAsync(userToken, systemType);
            return Ok(filteredDNDCharacters);
        }

        [HttpPost("create/{userToken}")]
        public async Task<IActionResult> CreateNewDndCharacter(int userToken, DndCharacterDto dndCharacterDto)
        {
            try
            {
                var user = await _userService.GetUserByTokenAsync(userToken);
                
                if (user == null)
                {
                    return NotFound();
                }
                var dndCharacter = await _characterService.CreateCharacterAsync<DndCharacter, DndCharacterDto>(userToken, dndCharacterDto);
                if (dndCharacter != null)
                {
                    await _userService.UpdateUserAndCharacterRelationship(userToken, dndCharacter.UserToken);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
