using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.CharacteristicTypes;

namespace CrealutionServer.Infrastructure.Repositories.Interfaces
{
    public interface ICharacteristicTypeRepository
    {
        Task<CharacteristicTypeGetAllDto> GetAll();
        Task<CharacteristicTypeDto> GetById(long id);
        Task<CharacteristicTypeDto> Create(CharacteristicTypeCreateDto createDto);
        Task<CharacteristicTypeDto> Update(CharacteristicTypeUpdateDto updateDto);
        Task Delete(long id);
    }
}