using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.StatisticTypes;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing StatisticType entities.
    /// </summary
    public class StatisticTypeRepository : IStatisticTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticTypeRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public StatisticTypeRepository(
            IMapper mapper,
            CrealutionDb context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Gets all StatisticType entities.
        /// </summary>
        /// <returns>A collection of StatisticType entities.</returns>
        public async Task<StatisticTypeGetAllDto> GetAll()
        {
            var entities = await _context.StatisticTypes
                .ToListAsync();

            return _mapper.Map<StatisticTypeGetAllDto>(entities);
        }

        /// <summary>
        /// Gets a StatisticType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the StatisticType entity to retrieve.</param>
        /// <returns>The StatisticType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the StatisticType entity is not found.</exception>
        public async Task<StatisticTypeDto> GetById(long id)
        {
            var entity = await _context.StatisticTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(StatisticType)} has been not found");

            return _mapper.Map<StatisticTypeDto>(entity);
        }

        /// <summary>
        /// Creates a new StatisticType entity.
        /// </summary>
        /// <param name="createDto">The data to create the StatisticType entity.</param>
        /// <returns>The created StatisticType entity.</returns>
        public async Task<StatisticTypeDto> Create(StatisticTypeCreateDto createDto)
        {
            var entity = _mapper.Map<StatisticType>(createDto);
            var entryEntity = await _context.StatisticTypes.AddAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<StatisticTypeDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing StatisticType entity.
        /// </summary>
        /// <param name="updateDto">The data to update the StatisticType entity.</param>
        /// <returns>The updated StatisticType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the StatisticType entity is not found.</exception>
        public async Task<StatisticTypeDto> Update(StatisticTypeUpdateDto updateDto)
        {
            var entity = await _context.StatisticTypes
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);

            if (entity != null)
            {
                _mapper.Map(updateDto, entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CrealutionEntityNotFound($"{nameof(StatisticType)} has been not found");
            }

            return _mapper.Map<StatisticTypeDto>(entity);
        }

        /// <summary>
        /// Deletes a StatisticType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the StatisticType entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the StatisticType entity is not found.</exception>
        public async Task Delete(long id)
        {
            var entity = await _context.StatisticTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                _context.StatisticTypes.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CrealutionEntityNotFound($"{nameof(StatisticType)} has been not found");
            }
        }
    }
}