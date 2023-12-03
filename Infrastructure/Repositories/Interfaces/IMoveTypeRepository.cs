using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.MoveTypes;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface IMoveTypeRepository
    {
        Task<MoveTypeGetAllDto> GetAll();
        Task<MoveTypeDto> GetById(long id);
        Task<MoveTypeDto> Create(MoveTypeCreateDto createDto);
        Task<MoveTypeDto> Update(MoveTypeUpdateDto updateDto);
        Task Delete(long id);
    }
}