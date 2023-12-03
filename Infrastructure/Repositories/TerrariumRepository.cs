using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.Terrariums;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="Terrarium"/> entities.
    /// </summary
    public class TerrariumRepository : ITerrariumRepository
    {
        private readonly IMapper _mapper;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrariumRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// <param name="cache">The ICacheService instance for cache database.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public TerrariumRepository(
            IMapper mapper,
            CrealutionDb context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Gets all Terrarium entities.
        /// </summary>
        /// <returns>A collection of Terrarium entities.</returns>
        public async Task<TerrariumGetAllDto> GetAll()
        {
            var entities = await _context.Terrariums
                .Include(x => x.Account)
                .ToListAsync();
            var dto = _mapper.Map<TerrariumGetAllDto>(entities);

            return dto;
        }

        /// <summary>
        /// Gets a Terrarium entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Terrarium entity to retrieve.</param>
        /// <returns>The Terrarium entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the Terrarium entity is not found.</exception>
        public async Task<TerrariumDto> GetById(long id)
        {
            var entity = await _context.Terrariums
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(Terrarium)} has been not found");

            var dto = _mapper.Map<TerrariumDto>(entity);

            return dto;
        }

        /// <summary>
        /// Creates a new Terrarium entity.
        /// </summary>
        /// <param name="createDto">The data to create the Terrarium entity.</param>
        /// <returns>The created Terrarium entity.</returns>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate terrarium name is detected.</exception>
        public async Task<TerrariumDto> Create(TerrariumCreateDto createDto)
        {
            var entity = _mapper.Map<Terrarium>(createDto);
            var dublicate = await _context.Terrariums.FirstOrDefaultAsync(x => x.Name == createDto.Name);
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == createDto.AccountId);
            var entryEntity = await _context.Terrariums.AddAsync(entity);

            if (account == null)
                throw new CrealutionEntityNotFound($"{nameof(Account)} [Id:{createDto.AccountId}] has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(Terrarium)} name already exist");

            entity.SetAccount(account);
            await _context.SaveChangesAsync();

            return _mapper.Map<TerrariumDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing Terrarium entity.
        /// </summary>
        /// <param name="updateDto">The data to update the Terrarium entity.</param>
        /// <returns>The updated Terrarium entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the Terrarium entity is not found.</exception>
        /// <exception cref="CrealutionEntityValidateException">Thrown if a duplicate terrarium name is detected.</exception>
        public async Task<TerrariumDto> Update(TerrariumUpdateDto updateDto)
        {
            var entity = await _context.Terrariums
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == updateDto.AccountId);
            var dublicate = await _context.Terrariums.FirstOrDefaultAsync(x => x.Name == updateDto.Name);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(Terrarium)} has been not found");

            if (account == null)
                throw new CrealutionEntityNotFound($"{nameof(Account)} [Id:{updateDto.AccountId}] has been not found");

            if (dublicate != null)
                throw new CrealutionEntityValidateException($"{nameof(Terrarium)} name already exist");

            _mapper.Map(updateDto, entity);
            entity.SetAccount(account);
            await _context.SaveChangesAsync();

            return _mapper.Map<TerrariumDto>(entity);
        }

        /// <summary>
        /// Deletes a Terrarium entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Terrarium entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the Terrarium entity is not found.</exception>
        public async Task Delete(long id)
        {
            var entity = await _context.Terrariums
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(Terrarium)} has been not found");

            _context.Terrariums.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}