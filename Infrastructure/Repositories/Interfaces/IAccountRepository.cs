using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.Accounts;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountAuthorizedDto> Login(AccountLoginDto loginDto);
        Task<AccountDto> Registration(AccountRegistrationDto registrationDto);
        Task<AccountDto> UpdateInfo(AccountUpdateInfoDto updateInfoDto);
        Task<AccountGetAllDto> GetAll();
        Task<AccountDto> GetById(long id);
        Task<AccountDto> Create(AccountCreateDto createDto);
        Task<AccountDto> Update(AccountUpdateDto updateDto);
        Task Delete(long id);
    }
}