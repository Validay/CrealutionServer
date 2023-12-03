using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.Terrariums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TerrariumController : ControllerBase
    {
        private ITerrariumRepository _terrariumRepository;

        public TerrariumController(ITerrariumRepository terrariumRepository)
        {
            _terrariumRepository = terrariumRepository;
        }

        /// <summary>
        /// Get all terrariums
        /// </summary>
        /// <returns>All terrarium dtos</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="401">Not authorized</response> 
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<TerrariumGetAllDto> GetAll()
        {
            return await _terrariumRepository.GetAll();
        }

        /// <summary>
        /// Get terrarium by id
        /// </summary>
        /// <returns>Terrarium dto</returns>
        /// <param name="id">Id terrarium</param>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")]
        public async Task<TerrariumDto> GetById(long id)
        {
            return await _terrariumRepository.GetById(id);
        }

        /// <summary>
        /// Create new terrarium
        /// </summary>
        /// <returns>Terrarium dto</returns>
        /// <param name="createDto">New terrarium</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<TerrariumDto> Create([FromBody] TerrariumCreateDto createDto)
        {
            return await _terrariumRepository.Create(createDto);
        }

        /// <summary>
        /// Update terrarium
        /// </summary>
        /// <returns>Terrarium dto</returns>
        /// <param name="updateDto">Update model terrarium</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<TerrariumDto> Update([FromBody] TerrariumUpdateDto updateDto)
        {
            return await _terrariumRepository.Update(updateDto);
        }

        /// <summary>
        /// Delete terrarium
        /// </summary>
        /// <param name="id">Id terrarium</param>
        /// <response code="204">Successful response, no content</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _terrariumRepository.Delete(id);

            return NoContent();
        }
    }
}