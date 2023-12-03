using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.BehaviorTypes;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface IBehaviorTypeRepository
    {
        Task<BehaviorTypeGetAllDto> GetAll();
        Task<BehaviorTypeDto> GetById(long id);
        Task<BehaviorTypeDto> Create(BehaviorTypeCreateDto createDto);
        Task<BehaviorTypeDto> Update(BehaviorTypeUpdateDto updateDto);
        Task Delete(long id);
    }
}