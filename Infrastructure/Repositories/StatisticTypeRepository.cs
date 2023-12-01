using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Infrastructure.Services.Interfaces;
using CrealutionServer.Models.Dtos.StatisticTypes;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="StatisticType"/> entities.
    /// </summary
    public class StatisticTypeRepository : IStatisticTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticTypeRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// /// <param name="cache">The ICacheService instance for cache database.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public StatisticTypeRepository(
            IMapper mapper,
            ICacheService cache,
            CrealutionDb context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }   

        /// <summary>
        /// Gets all StatisticType entities.
        /// </summary>
        /// <returns>A collection of StatisticType entities.</returns>
        public async Task<StatisticTypeGetAllDto> GetAll()
        {
            var key = $"{nameof(StatisticTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<StatisticTypeGetAllDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entities = await _context.StatisticTypes
                .ToListAsync();
            var dto = _mapper.Map<StatisticTypeGetAllDto>(entities);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Gets a StatisticType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the StatisticType entity to retrieve.</param>
        /// <returns>The StatisticType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the StatisticType entity is not found.</exception>
        public async Task<StatisticTypeDto> GetById(long id)
        {
            var key = $"{nameof(StatisticTypeDto)}_{id}";
            var cachedDto = await _cache.GetData<StatisticTypeDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entity = await _context.StatisticTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(StatisticType)} has been not found");

            var dto = _mapper.Map<StatisticTypeDto>(entity);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Creates a new StatisticType entity.
        /// </summary>
        /// <param name="createDto">The data to create the StatisticType entity.</param>
        /// <returns>The created StatisticType entity.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate statistic type name is detected.</exception>
        public async Task<StatisticTypeDto> Create(StatisticTypeCreateDto createDto)
        {
            var keyAllDto = $"{nameof(StatisticTypeDto)}";
            var cachedDto = await _cache.GetData<StatisticTypeDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = _mapper.Map<StatisticType>(createDto);
            var dublicate = await _context.StatisticTypes.FirstOrDefaultAsync(x => x.Name == createDto.Name);
            var entryEntity = await _context.StatisticTypes.AddAsync(entity);

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(StatisticType)} name already exist");

            await _context.SaveChangesAsync();

            return _mapper.Map<StatisticTypeDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing StatisticType entity.
        /// </summary>
        /// <param name="updateDto">The data to update the StatisticType entity.</param>
        /// <returns>The updated StatisticType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the StatisticType entity is not found.</exception>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate statistic type name is detected.</exception>
        public async Task<StatisticTypeDto> Update(StatisticTypeUpdateDto updateDto)
        {
            var key = $"{nameof(StatisticTypeDto)}_{updateDto.Id}";
            var cachedDto = await _cache.GetData<StatisticTypeDto>(key);
            var keyAllDto = $"{nameof(StatisticTypeGetAllDto)}";
            var cachedAllDto = await _cache.GetData<StatisticTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(key);

            if (cachedAllDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.StatisticTypes
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);
            var dublicate = await _context.StatisticTypes.FirstOrDefaultAsync(x => x.Name == updateDto.Name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(StatisticType)} has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(StatisticType)} name already exist");

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<StatisticTypeDto>(entity);
        }

        /// <summary>
        /// Deletes a StatisticType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the StatisticType entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the StatisticType entity is not found.</exception>
        public async Task Delete(long id)
        {
            var keyAllDto = $"{nameof(StatisticTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<StatisticTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.StatisticTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(StatisticType)} has been not found");

            _context.StatisticTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}