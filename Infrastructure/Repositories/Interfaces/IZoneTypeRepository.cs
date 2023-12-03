using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.ZoneTypes;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface IZoneTypeRepository
    {
        Task<ZoneTypeGetAllDto> GetAll();
        Task<ZoneTypeDto> GetById(long id);
        Task<ZoneTypeDto> Create(ZoneTypeCreateDto createDto);
        Task<ZoneTypeDto> Update(ZoneTypeUpdateDto updateDto);
        Task Delete(long id);
    }
}