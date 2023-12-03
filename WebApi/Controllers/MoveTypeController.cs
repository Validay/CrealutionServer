using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.MoveTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoveTypeController : ControllerBase
    {
        private IMoveTypeRepository _moveTypeRepository;

        public MoveTypeController(IMoveTypeRepository moveTypeRepository)
        {
            _moveTypeRepository = moveTypeRepository;
        }

        /// <summary>
        /// Get all move types
        /// </summary>
        /// <returns>All move type dtos</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="401">Not authorized</response> 
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<MoveTypeGetAllDto> GetAll()
        {
            return await _moveTypeRepository.GetAll();
        }

        /// <summary>
        /// Get move type by id
        /// </summary>
        /// <returns>MoveType dto</returns>
        /// <param name="id">Id move type</param>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")]
        public async Task<MoveTypeDto> GetById(long id)
        {
            return await _moveTypeRepository.GetById(id);
        }

        /// <summary>
        /// Create new move type
        /// </summary>
        /// <returns>MoveType dto</returns>
        /// <param name="createDto">New move type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<MoveTypeDto> Create([FromBody] MoveTypeCreateDto createDto)
        {
            return await _moveTypeRepository.Create(createDto);
        }

        /// <summary>
        /// Update move type
        /// </summary>
        /// <returns>MoveType dto</returns>
        /// <param name="updateDto">Update model move type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<MoveTypeDto> Update([FromBody] MoveTypeUpdateDto updateDto)
        {
            return await _moveTypeRepository.Update(updateDto);
        }

        /// <summary>
        /// Delete move type
        /// </summary>
        /// <param name="id">Id move type</param>
        /// <response code="204">Successful response, no content</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _moveTypeRepository.Delete(id);

            return NoContent();
        }
    }
}