using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.ZoneTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ZoneTypeController : ControllerBase
    {
        private IZoneTypeRepository _zoneTypeRepository;

        public ZoneTypeController(IZoneTypeRepository zoneTypeRepository)
        {
            _zoneTypeRepository = zoneTypeRepository;
        }

        /// <summary>
        /// Get all zone types
        /// </summary>
        /// <returns>All zone type dtos</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="401">Not authorized</response> 
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<ZoneTypeGetAllDto> GetAll()
        {
            return await _zoneTypeRepository.GetAll();
        }

        /// <summary>
        /// Get zone type by id
        /// </summary>
        /// <returns>ZoneType dto</returns>
        /// <param name="id">Id zone type</param>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")]
        public async Task<ZoneTypeDto> GetById(long id)
        {
            return await _zoneTypeRepository.GetById(id);
        }

        /// <summary>
        /// Create new zone type
        /// </summary>
        /// <returns>ZoneType dto</returns>
        /// <param name="createDto">New zone type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<ZoneTypeDto> Create([FromBody] ZoneTypeCreateDto createDto)
        {
            return await _zoneTypeRepository.Create(createDto);
        }

        /// <summary>
        /// Update zone type
        /// </summary>
        /// <returns>ZoneType dto</returns>
        /// <param name="updateDto">Update model zone type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<ZoneTypeDto> Update([FromBody] ZoneTypeUpdateDto updateDto)
        {
            return await _zoneTypeRepository.Update(updateDto);
        }

        /// <summary>
        /// Delete zone type
        /// </summary>
        /// <param name="id">Id zone type</param>
        /// <response code="204">Successful response, no content</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _zoneTypeRepository.Delete(id);

            return NoContent();
        }
    }
}