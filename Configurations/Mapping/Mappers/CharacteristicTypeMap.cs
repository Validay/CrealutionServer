using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CrealutionServer.Models.Dtos.CharacteristicTypes;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class CharacteristicTypeMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<CharacteristicType>, CharacteristicTypeGetAllDto>()
                .ForMember(dest => dest.CharacteristicTypeDtos, opt => opt
                    .MapFrom(src => src.Select(ct => new CharacteristicTypeDto
                    {
                        Id = ct.Id,
                        Name = ct.Name
                    })));

            profile.CreateMap<CharacteristicType, CharacteristicTypeDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<CharacteristicTypeCreateDto, CharacteristicType>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<CharacteristicTypeUpdateDto, CharacteristicType>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));
        }
    }
}