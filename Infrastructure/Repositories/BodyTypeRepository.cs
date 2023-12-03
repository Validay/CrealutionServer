using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Infrastructure.Services.Interfaces;
using CrealutionServer.Models.Dtos.BodyTypes;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="BodyType"/> entities.
    /// </summary
    public class BodyTypeRepository : IBodyTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyTypeRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// <param name="cache">The ICacheService instance for cache database.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public BodyTypeRepository(
            IMapper mapper,
            ICacheService cache,
            CrealutionDb context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }

        /// <summary>
        /// Gets all BodyType entities.
        /// </summary>
        /// <returns>A collection of BodyType entities.</returns>
        public async Task<BodyTypeGetAllDto> GetAll()
        {
            var key = $"{nameof(BodyTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<BodyTypeGetAllDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entities = await _context.BodyTypes
                   .ToListAsync();
            var dto = _mapper.Map<BodyTypeGetAllDto>(entities);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Gets a BodyType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BodyType entity to retrieve.</param>
        /// <returns>The BodyType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the BodyType entity is not found.</exception>
        public async Task<BodyTypeDto> GetById(long id)
        {
            var key = $"{nameof(BodyTypeDto)}_{id}";
            var cachedDto = await _cache.GetData<BodyTypeDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entity = await _context.BodyTypes
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(BodyType)} has been not found");

            var dto = _mapper.Map<BodyTypeDto>(entity);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Creates a new BodyType entity.
        /// </summary>
        /// <param name="createDto">The data to create the BodyType entity.</param>
        /// <returns>The created BodyType entity.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate body type name is detected.</exception>
        public async Task<BodyTypeDto> Create(BodyTypeCreateDto createDto)
        {
            var keyAllDto = $"{nameof(BodyTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<BodyTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = _mapper.Map<BodyType>(createDto);
            var dublicate = await _context.BodyTypes.FirstOrDefaultAsync(x => x.Name == createDto.Name);
            var entryEntity = await _context.BodyTypes.AddAsync(entity);

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(BodyType)} name already exist");

            await _context.SaveChangesAsync();

            return _mapper.Map<BodyTypeDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing BodyType entity.
        /// </summary>
        /// <param name="updateDto">The data to update the BodyType entity.</param>
        /// <returns>The updated BodyType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the BodyType entity is not found.</exception>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate body type name is detected.</exception>
        public async Task<BodyTypeDto> Update(BodyTypeUpdateDto updateDto)
        {
            var key = $"{nameof(BodyTypeDto)}_{updateDto.Id}";
            var cachedDto = await _cache.GetData<BodyTypeDto>(key);
            var keyAllDto = $"{nameof(BodyTypeGetAllDto)}";
            var cachedAllDto = await _cache.GetData<BodyTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(key);

            if (cachedAllDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.BodyTypes
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);
            var dublicate = await _context.BodyTypes.FirstOrDefaultAsync(x => x.Name == updateDto.Name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(BodyType)} has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(BodyType)} name already exist");

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BodyTypeDto>(entity);
        }

        /// <summary>
        /// Deletes a BodyType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BodyType entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the BodyType entity is not found.</exception>
        public async Task Delete(long id)
        {
            var keyAllDto = $"{nameof(BodyTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<BodyTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.BodyTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(BodyType)} has been not found");

            _context.BodyTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}