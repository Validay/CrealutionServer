using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Infrastructure.Services.Interfaces;
using CrealutionServer.Models.Dtos.ZoneTypes;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="ZoneType"/> entities.
    /// </summary
    public class ZoneTypeRepository : IZoneTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoneTypeRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// <param name="cache">The ICacheService instance for cache database.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public ZoneTypeRepository(
            IMapper mapper,
            ICacheService cache,
            CrealutionDb context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }

        /// <summary>
        /// Gets all ZoneType entities.
        /// </summary>
        /// <returns>A collection of ZoneType entities.</returns>
        public async Task<ZoneTypeGetAllDto> GetAll()
        {
            var key = $"{nameof(ZoneTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<ZoneTypeGetAllDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entities = await _context.ZoneTypes
                   .ToListAsync();
            var dto = _mapper.Map<ZoneTypeGetAllDto>(entities);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Gets a ZoneType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the ZoneType entity to retrieve.</param>
        /// <returns>The ZoneType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the ZoneType entity is not found.</exception>
        public async Task<ZoneTypeDto> GetById(long id)
        {
            var key = $"{nameof(ZoneTypeDto)}_{id}";
            var cachedDto = await _cache.GetData<ZoneTypeDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entity = await _context.ZoneTypes
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(ZoneType)} has been not found");

            var dto = _mapper.Map<ZoneTypeDto>(entity);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Creates a new ZoneType entity.
        /// </summary>
        /// <param name="createDto">The data to create the ZoneType entity.</param>
        /// <returns>The created ZoneType entity.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate zone type name is detected.</exception>
        public async Task<ZoneTypeDto> Create(ZoneTypeCreateDto createDto)
        {
            var keyAllDto = $"{nameof(ZoneTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<ZoneTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = _mapper.Map<ZoneType>(createDto);
            var dublicate = await _context.ZoneTypes.FirstOrDefaultAsync(x => x.Name == createDto.Name);
            var entryEntity = await _context.ZoneTypes.AddAsync(entity);

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(ZoneType)} name already exist");

            await _context.SaveChangesAsync();

            return _mapper.Map<ZoneTypeDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing ZoneType entity.
        /// </summary>
        /// <param name="updateDto">The data to update the ZoneType entity.</param>
        /// <returns>The updated ZoneType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the ZoneType entity is not found.</exception>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate zone type name is detected.</exception>
        public async Task<ZoneTypeDto> Update(ZoneTypeUpdateDto updateDto)
        {
            var key = $"{nameof(ZoneTypeDto)}_{updateDto.Id}";
            var cachedDto = await _cache.GetData<ZoneTypeDto>(key);
            var keyAllDto = $"{nameof(ZoneTypeGetAllDto)}";
            var cachedAllDto = await _cache.GetData<ZoneTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(key);

            if (cachedAllDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.ZoneTypes
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);
            var dublicate = await _context.ZoneTypes.FirstOrDefaultAsync(x => x.Name == updateDto.Name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(ZoneType)} has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(ZoneType)} name already exist");

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ZoneTypeDto>(entity);
        }

        /// <summary>
        /// Deletes a ZoneType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the ZoneType entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the ZoneType entity is not found.</exception>
        public async Task Delete(long id)
        {
            var keyAllDto = $"{nameof(ZoneTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<ZoneTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.ZoneTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(ZoneType)} has been not found");

            _context.ZoneTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}