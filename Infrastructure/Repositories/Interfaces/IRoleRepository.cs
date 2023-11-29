using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.Roles;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<RoleGetAllDto> GetAll();
        Task<RoleDto> GetById(long id);
        Task<RoleDto> Create(RoleCreateDto createDto);
        Task<RoleDto> Update(RoleUpdateDto updateDto);
        Task Delete(long id);
    }
}