using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CrealutionServer.Models.Dtos.SickTypes;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class SickTypeMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<SickType>, SickTypeGetAllDto>()
                .ForMember(dest => dest.SickTypes, opt => opt
                    .MapFrom(src => src.Select(it => new SickTypeDto
                    {
                        Id = it.Id,
                        Name = it.Name
                    })));

            profile.CreateMap<SickType, SickTypeDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<SickTypeCreateDto, SickType>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<SickTypeUpdateDto, SickType>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));
        }
    }
}