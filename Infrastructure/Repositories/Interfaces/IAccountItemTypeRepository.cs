using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.AccountItemTypes;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface IAccountItemTypeRepository
    {
        Task<AccountItemTypeGetAllDto> GetAll();
        Task<AccountItemTypeGetAllDto> GetByAccountId(long idAccount);
        Task<AccountItemTypeDto> Create(AccountItemTypeCreateDto createDto);
        Task<AccountItemTypeDto> Update(AccountItemTypeUpdateDto updateDto);
        Task Delete(long id);
    }
}