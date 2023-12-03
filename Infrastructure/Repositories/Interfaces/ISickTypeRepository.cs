using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.SickTypes;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface ISickTypeRepository
    {
        Task<SickTypeGetAllDto> GetAll();
        Task<SickTypeDto> GetById(long id);
        Task<SickTypeDto> Create(SickTypeCreateDto createDto);
        Task<SickTypeDto> Update(SickTypeUpdateDto updateDto);
        Task Delete(long id);
    }
}