using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.StatisticTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StatisticTypeController : ControllerBase
    {
        private IStatisticTypeRepository _statisticTypeRepository;

        public StatisticTypeController(IStatisticTypeRepository statisticTypeRepository)
        {
            _statisticTypeRepository = statisticTypeRepository;
        }

        /// <summary>
        /// Get all statistic types
        /// </summary>
        /// <returns>All statistic type dtos</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="401">Not authorized</response> 
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<StatisticTypeGetAllDto> GetAll()
        {
            return await _statisticTypeRepository.GetAll();
        }

        /// <summary>
        /// Get statistic type by id
        /// </summary>
        /// <returns>Statistic type dto</returns>
        /// <param name="id">Id statistic type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")]
        public async Task<StatisticTypeDto> GetById(long id)
        {
            return await _statisticTypeRepository.GetById(id);
        }

        /// <summary>
        /// Create new statistic type
        /// </summary>
        /// <returns>Statistic type dto</returns>
        /// <param name="createDto">New statistic type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<StatisticTypeDto> Create([FromBody] StatisticTypeCreateDto createDto)
        {
            return await _statisticTypeRepository.Create(createDto);
        }

        /// <summary>
        /// Update statistic type
        /// </summary>
        /// <returns>Statistic type dto</returns>
        /// <param name="updateDto">Update model statistic type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<StatisticTypeDto> Update([FromBody] StatisticTypeUpdateDto updateDto)
        {
            return await _statisticTypeRepository.Update(updateDto);
        }

        /// <summary>
        /// Delete statistic type
        /// </summary>
        /// <param name="id">Id statistic type</param>
        /// <response code="204">Successful response, no content</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _statisticTypeRepository.Delete(id);

            return NoContent();
        }
    }
}