using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces;
using webapi.Models.DND.Enums;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterSheetController : ControllerBase
    {
        private readonly ICharacterSheetService _characterService;

        public CharacterSheetController(ICharacterSheetService characterService)
        {
            _characterService = characterService;
        }
        [HttpGet("dnd/{id}")]
        public async Task<IActionResult> GetDNDCardById(int id)
        {
            try
            {
                var dndCard = await _characterService.GetDNDCardByIdAsync(id);
                if (dndCard == null)
                    return NotFound();
                return Ok(dndCard);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
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
    }
}
