using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.StatisticTypes;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface IStatisticTypeRepository
    {
        Task<StatisticTypeGetAllDto> GetAll();
        Task<StatisticTypeDto> GetById(long id);
        Task<StatisticTypeDto> Create(StatisticTypeCreateDto createDto);
        Task<StatisticTypeDto> Update(StatisticTypeUpdateDto updateDto);
        Task Delete(long id);
    }
}