using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.CharacteristicTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CharacteristicTypeController : ControllerBase
    {
        private ICharacteristicTypeRepository _characteristicTypeRepository;

        public CharacteristicTypeController(ICharacteristicTypeRepository characteristicTypeRepository)
        {
            _characteristicTypeRepository = characteristicTypeRepository;
        }

        /// <summary>
        /// Get all characteristic types
        /// </summary>
        /// <returns>All characteristic type dtos</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="401">Not authorized</response> 
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<CharacteristicTypeGetAllDto> GetAll()
        {
            return await _characteristicTypeRepository.GetAll();
        }

        /// <summary>
        /// Get characteristic type by id
        /// </summary>
        /// <returns>Characteristic type dto</returns>
        /// <param name="id">Id characteristic type</param>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")]
        public async Task<CharacteristicTypeDto> GetById(long id)
        {
            return await _characteristicTypeRepository.GetById(id);
        }

        /// <summary>
        /// Create new characteristic type
        /// </summary>
        /// <returns>Characteristic type dto</returns>
        /// <param name="createDto">New characteristic type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<CharacteristicTypeDto> Create([FromBody] CharacteristicTypeCreateDto createDto)
        {
            return await _characteristicTypeRepository.Create(createDto);
        }

        /// <summary>
        /// Update characteristic type
        /// </summary>
        /// <returns>Characteristic type dto</returns>
        /// <param name="updateDto">Update model characteristic type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<CharacteristicTypeDto> Update([FromBody] CharacteristicTypeUpdateDto updateDto)
        {
            return await _characteristicTypeRepository.Update(updateDto);
        }

        /// <summary>
        /// Delete characteristic type
        /// </summary>
        /// <param name="id">Id characteristic type</param>
        /// <response code="204">Successful response, no content</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _characteristicTypeRepository.Delete(id);

            return NoContent();
        }
    }
}