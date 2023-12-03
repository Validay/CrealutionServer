using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.AccountItemTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountItemTypeController : ControllerBase
    {
        private IAccountItemTypeRepository _accountItemTypeRepository;

        public AccountItemTypeController(IAccountItemTypeRepository accountItemTypeRepository)
        {
            _accountItemTypeRepository = accountItemTypeRepository;
        }

        /// <summary>
        /// Get all account item types
        /// </summary>
        /// <returns>All item type dtos</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="401">Not authorized</response> 
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<AccountItemTypeGetAllDto> GetAll()
        {
            return await _accountItemTypeRepository.GetAll();
        }

        /// <summary>
        /// Get account item type by id
        /// </summary>
        /// <returns>All item type dtos</returns>
        /// <param name="idAccount">Id account</param>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{idAccount}")]
        public async Task<AccountItemTypeGetAllDto> GetByAccountId(long idAccount)
        {
            return await _accountItemTypeRepository.GetByAccountId(idAccount);
        }

        /// <summary>
        /// Create new account item type
        /// </summary>
        /// <returns>AccountItemType dto</returns>
        /// <param name="createDto">New account item type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<AccountItemTypeDto> Create([FromBody] AccountItemTypeCreateDto createDto)
        {
            return await _accountItemTypeRepository.Create(createDto);
        }

        /// <summary>
        /// Update account item type
        /// </summary>
        /// <returns>AccountItemType dto</returns>
        /// <param name="updateDto">Update model account item type</param>
        /// <response code="200">Successful response</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<AccountItemTypeDto> Update([FromBody] AccountItemTypeUpdateDto updateDto)
        {
            return await _accountItemTypeRepository.Update(updateDto);
        }

        /// <summary>
        /// Delete account item type
        /// </summary>
        /// <param name="id">Id account item type</param>
        /// <response code="204">Successful response, no content</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _accountItemTypeRepository.Delete(id);

            return NoContent();
        }
    }
}