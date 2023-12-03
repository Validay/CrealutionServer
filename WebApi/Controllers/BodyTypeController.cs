using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.BodyTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BodyTypeController : ControllerBase
    {
        private IBodyTypeRepository _bodyTypeRepository;

        public BodyTypeController(IBodyTypeRepository bodyTypeRepository)
        {
            _bodyTypeRepository = bodyTypeRepository;
        }

        /// <summary>
        /// Get all body types
        /// </summary>
        /// <returns>All body type dtos</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="401">Not authorized</response> 
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<BodyTypeGetAllDto> GetAll()
        {
            return await _bodyTypeRepository.GetAll();
        }

        /// <summary>
        /// Get body type by id
        /// </summary>
        /// <returns>BodyType dto</returns>
        /// <param name="id">Id body type</param>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")]
        public async Task<BodyTypeDto> GetById(long id)
        {
            return await _bodyTypeRepository.GetById(id);
        }

        /// <summary>
        /// Create new body type
        /// </summary>
        /// <returns>BodyType dto</returns>
        /// <param name="createDto">New body type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<BodyTypeDto> Create([FromBody] BodyTypeCreateDto createDto)
        {
            return await _bodyTypeRepository.Create(createDto);
        }

        /// <summary>
        /// Update body type
        /// </summary>
        /// <returns>BodyType dto</returns>
        /// <param name="updateDto">Update model body type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<BodyTypeDto> Update([FromBody] BodyTypeUpdateDto updateDto)
        {
            return await _bodyTypeRepository.Update(updateDto);
        }

        /// <summary>
        /// Delete body type
        /// </summary>
        /// <param name="id">Id body type</param>
        /// <response code="204">Successful response, no content</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _bodyTypeRepository.Delete(id);

            return NoContent();
        }
    }
}