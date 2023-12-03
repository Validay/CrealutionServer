using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.ItemTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemTypeController : ControllerBase
    {
        private IItemTypeRepository _itemTypeRepository;

        public ItemTypeController(IItemTypeRepository itemTypeRepository)
        {
            _itemTypeRepository = itemTypeRepository;
        }

        /// <summary>
        /// Get all item types
        /// </summary>
        /// <returns>All item type dtos</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="401">Not authorized</response> 
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<ItemTypeGetAllDto> GetAll()
        {
            return await _itemTypeRepository.GetAll();
        }

        /// <summary>
        /// Get item type by id
        /// </summary>
        /// <returns>ItemType dto</returns>
        /// <param name="id">Id item type</param>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")]
        public async Task<ItemTypeDto> GetById(long id)
        {
            return await _itemTypeRepository.GetById(id);
        }

        /// <summary>
        /// Create new item type
        /// </summary>
        /// <returns>ItemType dto</returns>
        /// <param name="createDto">New item type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<ItemTypeDto> Create([FromBody] ItemTypeCreateDto createDto)
        {
            return await _itemTypeRepository.Create(createDto);
        }

        /// <summary>
        /// Update item type
        /// </summary>
        /// <returns>ItemType dto</returns>
        /// <param name="updateDto">Update model item type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<ItemTypeDto> Update([FromBody] ItemTypeUpdateDto updateDto)
        {
            return await _itemTypeRepository.Update(updateDto);
        }

        /// <summary>
        /// Delete item type
        /// </summary>
        /// <param name="id">Id item type</param>
        /// <response code="204">Successful response, no content</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _itemTypeRepository.Delete(id);

            return NoContent();
        }
    }
}