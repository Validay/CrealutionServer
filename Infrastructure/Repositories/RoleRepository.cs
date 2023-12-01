using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Infrastructure.Services.Interfaces;
using CrealutionServer.Models.Dtos.Roles;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="Role"/> entities.
    /// </summary
    public class RoleRepository : IRoleRepository
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// <param name="cache">The ICacheService instance for cache database.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public RoleRepository(
            IMapper mapper,
            ICacheService cache,
            CrealutionDb context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }

        /// <summary>
        /// Gets all Role entities.
        /// </summary>
        /// <returns>A collection of Role entities.</returns>
        public async Task<RoleGetAllDto> GetAll()
        {
            var key = $"{nameof(RoleGetAllDto)}";
            var cachedDto = await _cache.GetData<RoleGetAllDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entities = await _context.Roles
                   .ToListAsync();
            var dto = _mapper.Map<RoleGetAllDto>(entities);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Gets a Role entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Role entity to retrieve.</param>
        /// <returns>The Role entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the Role entity is not found.</exception>
        public async Task<RoleDto> GetById(long id)
        {
            var key = $"{nameof(RoleDto)}_{id}";
            var cachedDto = await _cache.GetData<RoleDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entity = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(Role)} has been not found");

            var dto = _mapper.Map<RoleDto>(entity);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Creates a new Role entity.
        /// </summary>
        /// <param name="createDto">The data to create the Role entity.</param>
        /// <returns>The created Role entity.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate role name is detected.</exception>
        public async Task<RoleDto> Create(RoleCreateDto createDto)
        {
            var keyAllDto = $"{nameof(RoleGetAllDto)}";
            var cachedDto = await _cache.GetData<RoleGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = _mapper.Map<Role>(createDto);
            var dublicate = await _context.Roles.FirstOrDefaultAsync(x => x.Name == createDto.Name);
            var entryEntity = await _context.Roles.AddAsync(entity);

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(Role)} name already exist");

            await _context.SaveChangesAsync();

            return _mapper.Map<RoleDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing Role entity.
        /// </summary>
        /// <param name="updateDto">The data to update the Role entity.</param>
        /// <returns>The updated Role entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the Role entity is not found.</exception>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate role name is detected.</exception>
        public async Task<RoleDto> Update(RoleUpdateDto updateDto)
        {
            var key = $"{nameof(RoleDto)}_{updateDto.Id}";
            var cachedDto = await _cache.GetData<RoleDto>(key);
            var keyAllDto = $"{nameof(RoleGetAllDto)}";
            var cachedAllDto = await _cache.GetData<RoleGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(key);

            if (cachedAllDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);
            var dublicate = await _context.Roles.FirstOrDefaultAsync(x => x.Name == updateDto.Name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(Role)} has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(Role)} name already exist");

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoleDto>(entity);
        }

        /// <summary>
        /// Deletes a Role entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Role entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the Role entity is not found.</exception>
        public async Task Delete(long id)
        {
            var keyAllDto = $"{nameof(RoleGetAllDto)}";
            var cachedDto = await _cache.GetData<RoleGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(Role)} has been not found");

            _context.Roles.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}