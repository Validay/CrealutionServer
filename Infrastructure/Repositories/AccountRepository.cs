using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Configurations.Authentication;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for managing operations related to user accounts in the application.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly AuthenticationOptions _authenticationOptions;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Constructor for the AccountRepository class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance for mapping between DTOs and entities.</param>
        /// <param name="authenticationOptions">Authentication options for JWT token generation.</param>
        /// <param name="context">The database context for interacting with the data store.</param>
        public AccountRepository(
            IMapper mapper,
            AuthenticationOptions authenticationOptions,
            CrealutionDb context)
        {
            _mapper = mapper;
            _authenticationOptions = authenticationOptions;
            _context = context;
        }

        /// <summary>
        /// Authenticates a user based on login credentials and generates a JWT token upon successful authentication.
        /// </summary>
        /// <param name="loginDto">DTO containing user login credentials.</param>
        /// <returns>DTO containing an authenticated user's information and JWT token.</returns>
        public async Task<AccountAuthorizedDto> Login(AccountLoginDto loginDto)
        {
            var account = await _context.Accounts
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Name == loginDto.Name);
            var secretKey = _authenticationOptions.GetSymmetricSecurityKey();
            var signingCredentials = new SigningCredentials(
                secretKey,
                SecurityAlgorithms.HmacSha256);

            if (account == null)
                throw new CrealutionEntityNotFound($"{nameof(Account)} has been not found");

            if (account.Password != loginDto.Password)
                throw new CrealutionEntityValidateException($"{nameof(account.Name)} incorrect password");

            var claims = new List<Claim>
            {
                new Claim(
                    "Id",
                    Guid.NewGuid().ToString()),
                new Claim(
                    JwtRegisteredClaimNames.Name,
                    loginDto.Name),
                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            };

            foreach (var role in account.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role.Name));

            var token = new JwtSecurityToken(
                issuer: _authenticationOptions.Issuer,
                audience: _authenticationOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials
            );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            await Task.CompletedTask;

            return new AccountAuthorizedDto
            {
                Token = $"Bearer {encodedToken}",
                AccountInfo = _mapper.Map<AccountDto>(account)
            };
        }

        /// <summary>
        /// Registers a new user account based on the provided registration DTO.
        /// </summary>
        /// <param name="registrationDto">DTO containing user registration details.</param>
        /// <returns>DTO containing information for the registered user account.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate account name is detected.</exception>
        public async Task<AccountDto> Registration(AccountRegistrationDto registrationDto)
        {
            var entity = _mapper.Map<Account>(registrationDto);
            var dublicate = await _context.Accounts.FirstOrDefaultAsync(x => x.Name == registrationDto.Name);
            var entryEntiry = await _context.Accounts.AddAsync(entity);

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(Account)} name already exist");

            await _context.SaveChangesAsync();

            return _mapper.Map<AccountDto>(entryEntiry.Entity);
        }

        /// <summary>
        /// Updates information for a user account based on the provided DTO.
        /// </summary>
        /// <param name="updateInfoDto">DTO containing updated user information.</param>
        /// <returns>DTO containing information for the updated user account.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown if the user account is not found.</exception>
        public async Task<AccountDto> UpdateInfo(AccountUpdateInfoDto updateInfoDto)
        {
            var entity = await _context.Accounts
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Name == updateInfoDto.Name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(Account)} has been not found");

            _mapper.Map(updateInfoDto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<AccountDto>(entity);
        }

        /// <summary>
        /// Retrieves information for all user accounts, including associated roles.
        /// </summary>
        /// <returns>DTO containing information for all user accounts.</returns>
        public async Task<AccountGetAllDto> GetAll()
        {
            var entities = await _context.Accounts
                .Include(x => x.Roles)
                .ToListAsync();

            return _mapper.Map<AccountGetAllDto>(entities);
        }

        /// <summary>
        /// Retrieves information for a user account by its unique identifier, including associated roles.
        /// </summary>
        /// <param name="id">The unique identifier of the user account to retrieve.</param>
        /// <returns>DTO containing information for the specified user account.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown if the user account is not found.</exception>
        public async Task<AccountDto> GetById(long id)
        {
            var entity = await _context.Accounts
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(Account)} has been not found");

            return _mapper.Map<AccountDto>(entity);
        }

        /// <summary>
        /// Retrieves information for a user account by its username, including associated roles.
        /// </summary>
        /// <param name="name">The username of the user account to retrieve.</param>
        /// <returns>DTO containing information for the specified user account.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown if the user account is not found.</exception>
        public async Task<AccountDto> GetByName(string name)
        {
            var entity = await _context.Accounts
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Name == name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(Account)} has been not found");

            return _mapper.Map<AccountDto>(entity);
        }

        /// <summary>
        /// Creates a new user account based on the provided DTO, associating it with specified roles.
        /// </summary>
        /// <param name="createDto">DTO containing user account creation details.</param>
        /// <returns>DTO containing information for the created user account.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate account name is detected.</exception>
        /// <exception cref="CrealutionEntityNotFound">Thrown if roles with specified IDs are not found.</exception>
        public async Task<AccountDto> Create(AccountCreateDto createDto)
        {
            var entity = _mapper.Map<Account>(createDto);
            var dublicate = await _context.Accounts.FirstOrDefaultAsync(x => x.Name == createDto.Name);
            var roles = await _context.Roles
                .Where(x => createDto.RoleIds.Contains(x.Id))
                .ToListAsync();

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(Account)} name already exist");

            if (roles.Count != createDto.RoleIds.Count)
            {
                var notFoundRoleIds = createDto.RoleIds.Except(roles.Select(r => r.Id));
                var notFoundRoleIdsMessage = string.Join(", ", notFoundRoleIds);

                throw new CrealutionEntityNotFound($"{nameof(Role)}s with IDs {notFoundRoleIdsMessage} not found");
            }

            entity.SetRoles(roles);

            var entryEntiry = await _context.Accounts.AddAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<AccountDto>(entryEntiry.Entity);
        }

        /// <summary>
        /// Updates a user account based on the provided DTO, including adding or removing roles.
        /// </summary>
        /// <param name="updateDto">DTO containing user account update details.</param>
        /// <returns>DTO containing information for the updated user account.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown if the user account or roles are not found.</exception>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate account name is detected.</exception>
        public async Task<AccountDto> Update(AccountUpdateDto updateDto)
        {
            var entity = await _context.Accounts
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);
            var dublicate = await _context.Accounts.FirstOrDefaultAsync(x => x.Name == updateDto.Name);
            var roles = await _context.Roles
                .Include(x => x.Accounts)
                .Where(x => updateDto.RoleIds.Contains(x.Id))
                .ToListAsync();

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(Account)} has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(Account)} name already exist");

            if (roles.Count != updateDto.RoleIds.Count)
            {
                var notFoundRoleIds = updateDto.RoleIds.Except(roles.Select(r => r.Id));
                var notFoundRoleIdsMessage = string.Join(", ", notFoundRoleIds);

                throw new CrealutionEntityNotFound($"{nameof(Role)}s with IDs {notFoundRoleIdsMessage} not found");
            }

            var rolesToRemove = entity.Roles
                .Where(r => !updateDto.RoleIds.Contains(r.Id))
                .ToList();
            var rolesToAdd = roles
                .Where(x => !entity.Roles.Any(r => r.Id == x.Id))
                .ToList();

            _mapper.Map(updateDto, entity);
            rolesToRemove.ForEach(x => entity.Roles.Remove(x));
            rolesToAdd.ForEach(x => entity.Roles.Add(x));
            await _context.SaveChangesAsync();

            return _mapper.Map<AccountDto>(entity);
        }

        /// <summary>
        /// Deletes a user account by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user account to be deleted.</param>
        public async Task Delete(long id)
        {
            var entity = await _context.Accounts
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(Account)} has been not found");

            _context.Accounts.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}