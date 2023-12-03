using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.ItemTypes;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface IItemTypeRepository
    {
        Task<ItemTypeGetAllDto> GetAll();
        Task<ItemTypeDto> GetById(long id);
        Task<ItemTypeDto> Create(ItemTypeCreateDto createDto);
        Task<ItemTypeDto> Update(ItemTypeUpdateDto updateDto);
        Task Delete(long id);
    }
}