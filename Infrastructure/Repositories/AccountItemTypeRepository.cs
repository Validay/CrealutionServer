using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.AccountItemTypes;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="AccountItemType"/> entities.
    /// </summary
    public class AccountItemTypeRepository : IAccountItemTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly CrealutionDb _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountItemTypeRepository"/> class.
        /// </summary>
        /// <param name="mapper">The IMapper instance for entity mapping.</param>
        /// <param name="context">The ICrealutionDb instance for database access.</param>
        public AccountItemTypeRepository(
            IMapper mapper,
            CrealutionDb context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Gets all AccountItemType entities.
        /// </summary>
        /// <returns>A collection of AccountItemType entities.</returns>
        public async Task<AccountItemTypeGetAllDto> GetAll()
        {
            var entities = await _context.AccountItemTypes
                .Include(x => x.Account)
                .Include(x => x.ItemType)
                .ToListAsync();
            var dto = _mapper.Map<AccountItemTypeGetAllDto>(entities);

            return dto;
        }

        /// <summary>
        /// Gets all AccountItemType entities by account id.
        /// </summary>
        /// <returns>A collection of AccountItemType entities.</returns>
        public async Task<AccountItemTypeGetAllDto> GetByAccountId(long idAccount)
        {
            var entities = await _context.AccountItemTypes
                .Include(x => x.Account)
                .Include(x => x.ItemType)
                .Where(x => x.Account.Id == idAccount)
                .ToListAsync();
            var dto = _mapper.Map<AccountItemTypeGetAllDto>(entities);

            return dto;
        }

        /// <summary>
        /// Creates a new AccountItemType entity.
        /// </summary>
        /// <param name="createDto">The data to create the AccountItemType entity.</param>
        /// <returns>The created AccountItemType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the Account entity is not found.</exception>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the ItemType entity is not found.</exception>
        public async Task<AccountItemTypeDto> Create(AccountItemTypeCreateDto createDto)
        {
            var entity = _mapper.Map<AccountItemType>(createDto);
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == createDto.AccountId);
            var itemType = await _context.ItemTypes.FirstOrDefaultAsync(x => x.Id == createDto.ItemTypeId);

            if (account == null)
                throw new CrealutionEntityNotFound($"{nameof(Account)} has been not found");

            if (itemType == null)
                throw new CrealutionEntityNotFound($"{nameof(ItemType)} has been not found");

            entity.SetAccount(account);
            entity.SetItemType(itemType);

            var entryEntity = await _context.AccountItemTypes.AddAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<AccountItemTypeDto>(entryEntity.Entity);
        }

        /// <summary>
        /// Updates an existing AccountItemType entity.
        /// </summary>
        /// <param name="updateDto">The data to update the AccountItemType entity.</param>
        /// <returns>The updated AccountItemType entity.</returns>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the AccountItemType entity is not found.</exception>
        public async Task<AccountItemTypeDto> Update(AccountItemTypeUpdateDto updateDto)
        {
            var entity = await _context.AccountItemTypes
                .Include(x => x.Account)
                .Include(x => x.ItemType)
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(AccountItemType)} has been not found");

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<AccountItemTypeDto>(entity);
        }

        /// <summary>
        /// Deletes a AccountItemType entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the AccountItemType entity to delete.</param>
        /// <exception cref="CrealutionEntityNotFound">Thrown when the AccountItemType entity is not found.</exception>
        public async Task Delete(long id)
        {
            var entity = await _context.AccountItemTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new CrealutionEntityNotFound($"{nameof(AccountItemType)} has been not found");

            _context.AccountItemTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}