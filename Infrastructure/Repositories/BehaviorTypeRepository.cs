using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Infrastructure.Services.Interfaces;
using CrealutionServer.Models.Dtos.BehaviorTypes;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="BehaviorType"/> entities.
    /// </summary
    public class BehaviorTypeRepository : IBehaviorTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BehaviorTypeRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// <param name="cache">The ICacheService instance for cache database.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public BehaviorTypeRepository(
            IMapper mapper,
            ICacheService cache,
            CrealutionDb context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }

        /// <summary>
        /// Gets all BehaviorType entities.
        /// </summary>
        /// <returns>A collection of BehaviorType entities.</returns>
        public async Task<BehaviorTypeGetAllDto> GetAll()
        {
            var key = $"{nameof(BehaviorTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<BehaviorTypeGetAllDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entities = await _context.BehaviorTypes
                   .ToListAsync();
            var dto = _mapper.Map<BehaviorTypeGetAllDto>(entities);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Gets a BehaviorType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BehaviorType entity to retrieve.</param>
        /// <returns>The BehaviorType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the BehaviorType entity is not found.</exception>
        public async Task<BehaviorTypeDto> GetById(long id)
        {
            var key = $"{nameof(BehaviorTypeDto)}_{id}";
            var cachedDto = await _cache.GetData<BehaviorTypeDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entity = await _context.BehaviorTypes
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(BehaviorType)} has been not found");

            var dto = _mapper.Map<BehaviorTypeDto>(entity);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Creates a new BehaviorType entity.
        /// </summary>
        /// <param name="createDto">The data to create the BehaviorType entity.</param>
        /// <returns>The created BehaviorType entity.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate behavior type name is detected.</exception>
        public async Task<BehaviorTypeDto> Create(BehaviorTypeCreateDto createDto)
        {
            var keyAllDto = $"{nameof(BehaviorTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<BehaviorTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = _mapper.Map<BehaviorType>(createDto);
            var dublicate = await _context.BehaviorTypes.FirstOrDefaultAsync(x => x.Name == createDto.Name);
            var entryEntity = await _context.BehaviorTypes.AddAsync(entity);

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(BehaviorType)} name already exist");

            await _context.SaveChangesAsync();

            return _mapper.Map<BehaviorTypeDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing BehaviorType entity.
        /// </summary>
        /// <param name="updateDto">The data to update the BehaviorType entity.</param>
        /// <returns>The updated BehaviorType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the BehaviorType entity is not found.</exception>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate behavior type name is detected.</exception>
        public async Task<BehaviorTypeDto> Update(BehaviorTypeUpdateDto updateDto)
        {
            var key = $"{nameof(BehaviorTypeDto)}_{updateDto.Id}";
            var cachedDto = await _cache.GetData<BehaviorTypeDto>(key);
            var keyAllDto = $"{nameof(BehaviorTypeGetAllDto)}";
            var cachedAllDto = await _cache.GetData<BehaviorTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(key);

            if (cachedAllDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.BehaviorTypes
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);
            var dublicate = await _context.BehaviorTypes.FirstOrDefaultAsync(x => x.Name == updateDto.Name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(BehaviorType)} has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(BehaviorType)} name already exist");

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BehaviorTypeDto>(entity);
        }

        /// <summary>
        /// Deletes a BehaviorType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BehaviorType entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the BehaviorType entity is not found.</exception>
        public async Task Delete(long id)
        {
            var keyAllDto = $"{nameof(BehaviorTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<BehaviorTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.BehaviorTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(BehaviorType)} has been not found");

            _context.BehaviorTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}