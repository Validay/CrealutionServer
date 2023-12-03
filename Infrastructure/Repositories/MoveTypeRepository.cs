using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Infrastructure.Services.Interfaces;
using CrealutionServer.Models.Dtos.MoveTypes;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="MoveType"/> entities.
    /// </summary
    public class MoveTypeRepository : IMoveTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveTypeRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// <param name="cache">The ICacheService instance for cache database.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public MoveTypeRepository(
            IMapper mapper,
            ICacheService cache,
            CrealutionDb context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }

        /// <summary>
        /// Gets all MoveType entities.
        /// </summary>
        /// <returns>A collection of MoveType entities.</returns>
        public async Task<MoveTypeGetAllDto> GetAll()
        {
            var key = $"{nameof(MoveTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<MoveTypeGetAllDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entities = await _context.MoveTypes
                   .ToListAsync();
            var dto = _mapper.Map<MoveTypeGetAllDto>(entities);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Gets a MoveType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the MoveType entity to retrieve.</param>
        /// <returns>The MoveType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the MoveType entity is not found.</exception>
        public async Task<MoveTypeDto> GetById(long id)
        {
            var key = $"{nameof(MoveTypeDto)}_{id}";
            var cachedDto = await _cache.GetData<MoveTypeDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entity = await _context.MoveTypes
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(MoveType)} has been not found");

            var dto = _mapper.Map<MoveTypeDto>(entity);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Creates a new MoveType entity.
        /// </summary>
        /// <param name="createDto">The data to create the MoveType entity.</param>
        /// <returns>The created MoveType entity.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate move type name is detected.</exception>
        public async Task<MoveTypeDto> Create(MoveTypeCreateDto createDto)
        {
            var keyAllDto = $"{nameof(MoveTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<MoveTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = _mapper.Map<MoveType>(createDto);
            var dublicate = await _context.MoveTypes.FirstOrDefaultAsync(x => x.Name == createDto.Name);
            var entryEntity = await _context.MoveTypes.AddAsync(entity);

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(MoveType)} name already exist");

            await _context.SaveChangesAsync();

            return _mapper.Map<MoveTypeDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing MoveType entity.
        /// </summary>
        /// <param name="updateDto">The data to update the MoveType entity.</param>
        /// <returns>The updated MoveType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the MoveType entity is not found.</exception>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate move type name is detected.</exception>
        public async Task<MoveTypeDto> Update(MoveTypeUpdateDto updateDto)
        {
            var key = $"{nameof(MoveTypeDto)}_{updateDto.Id}";
            var cachedDto = await _cache.GetData<MoveTypeDto>(key);
            var keyAllDto = $"{nameof(MoveTypeGetAllDto)}";
            var cachedAllDto = await _cache.GetData<MoveTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(key);

            if (cachedAllDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.MoveTypes
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);
            var dublicate = await _context.MoveTypes.FirstOrDefaultAsync(x => x.Name == updateDto.Name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(MoveType)} has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(MoveType)} name already exist");

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<MoveTypeDto>(entity);
        }

        /// <summary>
        /// Deletes a MoveType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the MoveType entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the MoveType entity is not found.</exception>
        public async Task Delete(long id)
        {
            var keyAllDto = $"{nameof(MoveTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<MoveTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.MoveTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(MoveType)} has been not found");

            _context.MoveTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}