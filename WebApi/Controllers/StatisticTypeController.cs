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
        /// Create new statistic type
        /// </summary>
        /// <returns>All statistic type dtos</returns>
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
    }
}