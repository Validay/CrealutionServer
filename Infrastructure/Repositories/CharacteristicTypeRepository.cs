using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Infrastructure.Services.Interfaces;
using CrealutionServer.Models.Dtos.CharacteristicTypes;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="CharacteristicType"/> entities.
    /// </summary
    public class CharacteristicTypeRepository : ICharacteristicTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacteristicTypeRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// /// <param name="cache">The ICacheService instance for cache database.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public CharacteristicTypeRepository(
            IMapper mapper,
            ICacheService cache,
            CrealutionDb context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }   

        /// <summary>
        /// Gets all CharacteristicType entities.
        /// </summary>
        /// <returns>A collection of CharacteristicType entities.</returns>
        public async Task<CharacteristicTypeGetAllDto> GetAll()
        {
            var key = $"{nameof(CharacteristicTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<CharacteristicTypeGetAllDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entities = await _context.CharacteristicTypes
                .ToListAsync();
            var dto = _mapper.Map<CharacteristicTypeGetAllDto>(entities);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Gets a CharacteristicType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the CharacteristicType entity to retrieve.</param>
        /// <returns>The CharacteristicType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the CharacteristicType entity is not found.</exception>
        public async Task<CharacteristicTypeDto> GetById(long id)
        {
            var key = $"{nameof(CharacteristicTypeDto)}_{id}";
            var cachedDto = await _cache.GetData<CharacteristicTypeDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entity = await _context.CharacteristicTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(CharacteristicType)} has been not found");

            var dto = _mapper.Map<CharacteristicTypeDto>(entity);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Creates a new CharacteristicType entity.
        /// </summary>
        /// <param name="createDto">The data to create the CharacteristicType entity.</param>
        /// <returns>The created CharacteristicType entity.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate characteristic type name is detected.</exception>
        public async Task<CharacteristicTypeDto> Create(CharacteristicTypeCreateDto createDto)
        {
            var keyAllDto = $"{nameof(CharacteristicTypeDto)}";
            var cachedDto = await _cache.GetData<CharacteristicTypeDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = _mapper.Map<CharacteristicType>(createDto);
            var dublicate = await _context.CharacteristicTypes.FirstOrDefaultAsync(x => x.Name == createDto.Name);
            var entryEntity = await _context.CharacteristicTypes.AddAsync(entity);

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(CharacteristicType)} name already exist");

            await _context.SaveChangesAsync();

            return _mapper.Map<CharacteristicTypeDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing CharacteristicType entity.
        /// </summary>
        /// <param name="updateDto">The data to update the CharacteristicType entity.</param>
        /// <returns>The updated CharacteristicType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the CharacteristicType entity is not found.</exception>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate characteristic type name is detected.</exception>
        public async Task<CharacteristicTypeDto> Update(CharacteristicTypeUpdateDto updateDto)
        {
            var key = $"{nameof(CharacteristicTypeDto)}_{updateDto.Id}";
            var cachedDto = await _cache.GetData<CharacteristicTypeDto>(key);
            var keyAllDto = $"{nameof(CharacteristicTypeGetAllDto)}";
            var cachedAllDto = await _cache.GetData<CharacteristicTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(key);

            if (cachedAllDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.CharacteristicTypes
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);
            var dublicate = await _context.CharacteristicTypes.FirstOrDefaultAsync(x => x.Name == updateDto.Name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(CharacteristicType)} has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(CharacteristicType)} name already exist");

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<CharacteristicTypeDto>(entity);
        }

        /// <summary>
        /// Deletes a CharacteristicType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the CharacteristicType entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the CharacteristicType entity is not found.</exception>
        public async Task Delete(long id)
        {
            var keyAllDto = $"{nameof(CharacteristicTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<CharacteristicTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.CharacteristicTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(CharacteristicType)} has been not found");

            _context.CharacteristicTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}