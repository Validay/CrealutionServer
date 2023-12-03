using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Infrastructure.Services.Interfaces;
using CrealutionServer.Models.Dtos.ItemTypes;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="ItemType"/> entities.
    /// </summary
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTypeRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// <param name="cache">The ICacheService instance for cache database.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public ItemTypeRepository(
            IMapper mapper,
            ICacheService cache,
            CrealutionDb context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }

        /// <summary>
        /// Gets all ItemType entities.
        /// </summary>
        /// <returns>A collection of ItemType entities.</returns>
        public async Task<ItemTypeGetAllDto> GetAll()
        {
            var key = $"{nameof(ItemTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<ItemTypeGetAllDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entities = await _context.ItemTypes
                   .ToListAsync();
            var dto = _mapper.Map<ItemTypeGetAllDto>(entities);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Gets a ItemType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the ItemType entity to retrieve.</param>
        /// <returns>The ItemType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the ItemType entity is not found.</exception>
        public async Task<ItemTypeDto> GetById(long id)
        {
            var key = $"{nameof(ItemTypeDto)}_{id}";
            var cachedDto = await _cache.GetData<ItemTypeDto>(key);

            if (cachedDto != null)
                return cachedDto;

            var entity = await _context.ItemTypes
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(ItemType)} has been not found");

            var dto = _mapper.Map<ItemTypeDto>(entity);

            await _cache.SetData(
                key,
                dto,
                1);

            return dto;
        }

        /// <summary>
        /// Creates a new ItemType entity.
        /// </summary>
        /// <param name="createDto">The data to create the ItemType entity.</param>
        /// <returns>The created ItemType entity.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate item type name is detected.</exception>
        public async Task<ItemTypeDto> Create(ItemTypeCreateDto createDto)
        {
            var keyAllDto = $"{nameof(ItemTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<ItemTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = _mapper.Map<ItemType>(createDto);
            var dublicate = await _context.ItemTypes.FirstOrDefaultAsync(x => x.Name == createDto.Name);
            var entryEntity = await _context.ItemTypes.AddAsync(entity);

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(ItemType)} name already exist");

            await _context.SaveChangesAsync();

            return _mapper.Map<ItemTypeDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing ItemType entity.
        /// </summary>
        /// <param name="updateDto">The data to update the ItemType entity.</param>
        /// <returns>The updated ItemType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the ItemType entity is not found.</exception>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate item type name is detected.</exception>
        public async Task<ItemTypeDto> Update(ItemTypeUpdateDto updateDto)
        {
            var key = $"{nameof(ItemTypeDto)}_{updateDto.Id}";
            var cachedDto = await _cache.GetData<ItemTypeDto>(key);
            var keyAllDto = $"{nameof(ItemTypeGetAllDto)}";
            var cachedAllDto = await _cache.GetData<ItemTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(key);

            if (cachedAllDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.ItemTypes
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);
            var dublicate = await _context.ItemTypes.FirstOrDefaultAsync(x => x.Name == updateDto.Name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(ItemType)} has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(ItemType)} name already exist");

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ItemTypeDto>(entity);
        }

        /// <summary>
        /// Deletes a ItemType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the ItemType entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the ItemType entity is not found.</exception>
        public async Task Delete(long id)
        {
            var keyAllDto = $"{nameof(ItemTypeGetAllDto)}";
            var cachedDto = await _cache.GetData<ItemTypeGetAllDto>(keyAllDto);

            if (cachedDto != null)
                await _cache.RemoveData(keyAllDto);

            var entity = await _context.ItemTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(ItemType)} has been not found");

            _context.ItemTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}