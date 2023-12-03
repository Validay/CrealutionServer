using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Infrastructure.Services.Interfaces;
using CrealutionServer.Models.Dtos.SickTypes;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="SickType"/> entities.
    /// </summary
    public class SickTypeRepository : ISickTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SickTypeRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// <param name="cache">The ICacheService instance for cache database.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public SickTypeRepository(
            IMapper mapper,
            ICacheService cache,
            CrealutionDb context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }

        /// <summary>
        /// Gets all SickType entities.
        /// </summary>
        /// <returns>A collection of SickType entities.</returns>
        public async Task<SickTypeGetAllDto> GetAll()
        {
            var key = $"{nameof(SickTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<SickTypeGetAllDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entities = await _context.SickTypes
                   .ToListAsync();
            var dto = _mapper.Map<SickTypeGetAllDto>(entities);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Gets a SickType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the SickType entity to retrieve.</param>
        /// <returns>The SickType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the SickType entity is not found.</exception>
        public async Task<SickTypeDto> GetById(long id)
        {
            var key = $"{nameof(SickTypeDto)}_{id}";
            var cachedDto = await _cache.GetData<SickTypeDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entity = await _context.SickTypes
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(SickType)} has been not found");

            var dto = _mapper.Map<SickTypeDto>(entity);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Creates a new SickType entity.
        /// </summary>
        /// <param name="createDto">The data to create the SickType entity.</param>
        /// <returns>The created SickType entity.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate sick type name is detected.</exception>
        public async Task<SickTypeDto> Create(SickTypeCreateDto createDto)
        {
            var keyAllDto = $"{nameof(SickTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<SickTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = _mapper.Map<SickType>(createDto);
            var dublicate = await _context.SickTypes.FirstOrDefaultAsync(x => x.Name == createDto.Name);
            var entryEntity = await _context.SickTypes.AddAsync(entity);

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(SickType)} name already exist");

            await _context.SaveChangesAsync();

            return _mapper.Map<SickTypeDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing SickType entity.
        /// </summary>
        /// <param name="updateDto">The data to update the SickType entity.</param>
        /// <returns>The updated SickType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the SickType entity is not found.</exception>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate sick type name is detected.</exception>
        public async Task<SickTypeDto> Update(SickTypeUpdateDto updateDto)
        {
            var key = $"{nameof(SickTypeDto)}_{updateDto.Id}";
            var cachedDto = await _cache.GetData<SickTypeDto>(key);
            var keyAllDto = $"{nameof(SickTypeGetAllDto)}";
            var cachedAllDto = await _cache.GetData<SickTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(key);

            if (cachedAllDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.SickTypes
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);
            var dublicate = await _context.SickTypes.FirstOrDefaultAsync(x => x.Name == updateDto.Name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(SickType)} has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(SickType)} name already exist");

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<SickTypeDto>(entity);
        }

        /// <summary>
        /// Deletes a SickType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the SickType entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the SickType entity is not found.</exception>
        public async Task Delete(long id)
        {
            var keyAllDto = $"{nameof(SickTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<SickTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.SickTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(SickType)} has been not found");

            _context.SickTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}