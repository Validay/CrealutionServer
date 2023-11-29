using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.Roles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns>All role dtos</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="401">Not authorized</response> 
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<RoleGetAllDto> GetAll()
        {
            return await _roleRepository.GetAll();
        }

        /// <summary>
        /// Get role by id
        /// </summary>
        /// <returns>Role dto</returns>
        /// <param name="id">Id statistic type</param>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")]
        public async Task<RoleDto> GetById(long id)
        {
            return await _roleRepository.GetById(id);
        }

        /// <summary>
        /// Create new role
        /// </summary>
        /// <returns>Role dto</returns>
        /// <param name="createDto">New role</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<RoleDto> Create([FromBody] RoleCreateDto createDto)
        {
            return await _roleRepository.Create(createDto);
        }

        /// <summary>
        /// Update role
        /// </summary>
        /// <returns>Role dto</returns>
        /// <param name="updateDto">Update model role</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<RoleDto> Update([FromBody] RoleUpdateDto updateDto)
        {
            return await _roleRepository.Update(updateDto);
        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="id">Id role</param>
        /// <response code="204">Successful response, no content</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _roleRepository.Delete(id);

            return NoContent();
        }
    }
}