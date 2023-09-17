using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTO;
using webapi.Interfaces;
using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;
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
        [HttpGet("dnd/{id}")]
        public async Task<IActionResult> GetDNDCardById(int id)
        {
            var dndCard = await _characterService.GetDNDCardByIdAsync(id);
            if (dndCard == null)
                return NotFound();
            return Ok(dndCard);
        }

        [HttpGet("dnd")]
        public async Task<IActionResult> GetAllDNDCharacters()
        {
            var dndCharacters = await _characterService.GetAllDNDCharactersAsync();
            return Ok(dndCharacters);
        }

        [HttpGet("dnd/filter")]
        public async Task<IActionResult> GetAllDNDCharactersByFilters([FromQuery] int baseCharacterId, [FromQuery] SystemType? systemType = null)
        {
            var filteredDNDCharacters = await _characterService.GetAllDNDCharactersByFiltersAsync(baseCharacterId, systemType);
            return Ok(filteredDNDCharacters);
        }
        [HttpPost("create/dnd")]
        public async Task<IActionResult> CreateDndCharacter(DndCharacterDto dndCharacterDto)
        {
            var dndCharacter = _mapper.Map<DndCharacterDto>(dndCharacterDto);
            //await _characterService.CreateCharacterAsync(3,dndCharacter);
            return Ok();
        }

        [HttpPost("{userToken}")]
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
