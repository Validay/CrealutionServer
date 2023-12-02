using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.Terrariums;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface ITerrariumRepository
    {
        Task<TerrariumGetAllDto> GetAll();
        Task<TerrariumDto> GetById(long id);
        Task<TerrariumDto> Create(TerrariumCreateDto createDto);
        Task<TerrariumDto> Update(TerrariumUpdateDto updateDto);
        Task Delete(long id);
    }
}