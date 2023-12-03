using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.SickTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SickTypeController : ControllerBase
    {
        private ISickTypeRepository _sickTypeRepository;

        public SickTypeController(ISickTypeRepository sickTypeRepository)
        {
            _sickTypeRepository = sickTypeRepository;
        }

        /// <summary>
        /// Get all sick types
        /// </summary>
        /// <returns>All sick type dtos</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="401">Not authorized</response> 
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<SickTypeGetAllDto> GetAll()
        {
            return await _sickTypeRepository.GetAll();
        }

        /// <summary>
        /// Get sick type by id
        /// </summary>
        /// <returns>SickType dto</returns>
        /// <param name="id">Id sick type</param>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")]
        public async Task<SickTypeDto> GetById(long id)
        {
            return await _sickTypeRepository.GetById(id);
        }

        /// <summary>
        /// Create new sick type
        /// </summary>
        /// <returns>SickType dto</returns>
        /// <param name="createDto">New sick type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<SickTypeDto> Create([FromBody] SickTypeCreateDto createDto)
        {
            return await _sickTypeRepository.Create(createDto);
        }

        /// <summary>
        /// Update zonsicke type
        /// </summary>
        /// <returns>SickType dto</returns>
        /// <param name="updateDto">Update model sick type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<SickTypeDto> Update([FromBody] SickTypeUpdateDto updateDto)
        {
            return await _sickTypeRepository.Update(updateDto);
        }

        /// <summary>
        /// Delete sick type
        /// </summary>
        /// <param name="id">Id sick type</param>
        /// <response code="204">Successful response, no content</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _sickTypeRepository.Delete(id);

            return NoContent();
        }
    }
}