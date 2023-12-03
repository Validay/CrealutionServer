using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.BodyTypes;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface IBodyTypeRepository
    {
        Task<BodyTypeGetAllDto> GetAll();
        Task<BodyTypeDto> GetById(long id);
        Task<BodyTypeDto> Create(BodyTypeCreateDto createDto);
        Task<BodyTypeDto> Update(BodyTypeUpdateDto updateDto);
        Task Delete(long id);
    }
}