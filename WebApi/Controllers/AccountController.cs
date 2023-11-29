using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrealutionServer.WebApi.Controllers
{
    /// <summary>
    /// Controller for handling user account operations.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountRepository">Repository for user account operations.</param>
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Handles user authentication and returns an authorized user DTO.
        /// </summary>
        /// <param name="loginDto">DTO containing user login credentials.</param>
        /// <returns>DTO containing authorization information for the user.</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="400">Bad request</response> 
        /// <response code="404">Not found</response> 
        /// <response code="500">Internal error</response>
        [HttpPost("Login")]
        public async Task<AccountAuthorizedDto> Login(AccountLoginDto loginDto)
        {
            return await _accountRepository.Login(loginDto);
        }

        /// <summary>
        /// Registers a new user account and returns the account DTO.
        /// </summary>
        /// <param name="registrationDto">DTO containing user registration details.</param>
        /// <returns>DTO containing information for the registered user account.</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="400">Bad request</response> 
        /// <response code="500">Internal error</response>
        [HttpPost("Registration")]
        public async Task<AccountDto> Registration(AccountRegistrationDto registrationDto)
        {
            return await _accountRepository.Registration(registrationDto);
        }

        /// <summary>
        /// Updates user account information and returns the updated account DTO.
        /// </summary>
        /// <param name="updateInfoDto">DTO containing updated user information.</param>
        /// <returns>DTO containing information for the updated user account.</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="400">Bad request</response> 
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response> 
        /// <response code="500">Internal error</response>
        [HttpPost("UpdateInfo")]
        public async Task<AccountDto> UpdateInfo(AccountUpdateInfoDto updateInfoDto)
        {
            return await _accountRepository.UpdateInfo(updateInfoDto);
        }

        /// <summary>
        /// Retrieves information for all user accounts and returns a DTO.
        /// </summary>
        /// <returns>DTO containing information for all user accounts.</returns>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet]
        public async Task<AccountGetAllDto> GetAll()
        {
            return await _accountRepository.GetAll();
        }

        /// <summary>
        /// Retrieves information for a user account by its identifier and returns a DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the user account.</param>
        /// <returns>DTO containing information for the specified user account.</returns>
        /// <response code="200">Successful response</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response> 
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")]
        public async Task<AccountDto> GetById(long id)
        {
            return await _accountRepository.GetById(id);
        }

        /// <summary>
        /// Creates a new user account and returns the account DTO.
        /// </summary>
        /// <param name="createDto">DTO containing user account creation details.</param>
        /// <returns>DTO containing information for the created user account.</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="400">Bad request</response> 
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response> 
        /// <response code="500">Internal error</response>
        [HttpPost]
        public async Task<AccountDto> Create([FromBody] AccountCreateDto createDto)
        {
            return await _accountRepository.Create(createDto);
        }

        /// <summary>
        /// Updates a user account and returns the updated account DTO.
        /// </summary>
        /// <param name="updateDto">DTO containing user account update details.</param>
        /// <returns>DTO containing information for the updated user account.</returns>
        /// <response code="200">Successful response</response>      
        /// <response code="400">Bad request</response> 
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response> 
        /// <response code="500">Internal error</response>
        [HttpPut]
        public async Task<AccountDto> Update([FromBody] AccountUpdateDto updateDto)
        {
            return await _accountRepository.Update(updateDto);
        }

        /// <summary>
        /// Deletes a user account by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user account to be deleted.</param>
        /// <returns>Action result indicating the success of the deletion.</returns>
        /// <response code="204">Successful response, no content</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response> 
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _accountRepository.Delete(id);

            return NoContent();
        }
    }
}