using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.BehaviorTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BehaviorTypeController : ControllerBase
    {
        private IBehaviorTypeRepository _behaviorTypeRepository;

        public BehaviorTypeController(IBehaviorTypeRepository behaviorTypeRepository)
        {
            _behaviorTypeRepository = behaviorTypeRepository;
        }

        /// <summary>
        /// Get all behavior types
        /// </summary>
        /// <returns>All behavior type dtos</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="401">Not authorized</response> 
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<BehaviorTypeGetAllDto> GetAll()
        {
            return await _behaviorTypeRepository.GetAll();
        }

        /// <summary>
        /// Get behavior type by id
        /// </summary>
        /// <returns>BehaviorType dto</returns>
        /// <param name="id">Id behavior type</param>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")]
        public async Task<BehaviorTypeDto> GetById(long id)
        {
            return await _behaviorTypeRepository.GetById(id);
        }

        /// <summary>
        /// Create new behavior type
        /// </summary>
        /// <returns>BehaviorType dto</returns>
        /// <param name="createDto">New behavior type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<BehaviorTypeDto> Create([FromBody] BehaviorTypeCreateDto createDto)
        {
            return await _behaviorTypeRepository.Create(createDto);
        }

        /// <summary>
        /// Update behavior type
        /// </summary>
        /// <returns>BehaviorType dto</returns>
        /// <param name="updateDto">Update model behavior type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<BehaviorTypeDto> Update([FromBody] BehaviorTypeUpdateDto updateDto)
        {
            return await _behaviorTypeRepository.Update(updateDto);
        }

        /// <summary>
        /// Delete behavior type
        /// </summary>
        /// <param name="id">Id behavior type</param>
        /// <response code="204">Successful response, no content</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _behaviorTypeRepository.Delete(id);

            return NoContent();
        }
    }
}