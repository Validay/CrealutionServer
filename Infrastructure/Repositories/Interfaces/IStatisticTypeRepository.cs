using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.StatisticTypes;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface IStatisticTypeRepository
    {
        Task<StatisticTypeGetAllDto> GetAll();
        Task<StatisticTypeDto> Create(StatisticTypeCreateDto createDto);
    }
}