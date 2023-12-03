using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CrealutionServer.Models.Dtos.ZoneTypes;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class ZoneTypeMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<ZoneType>, ZoneTypeGetAllDto>()
                .ForMember(dest => dest.ZoneTypes, opt => opt
                    .MapFrom(src => src.Select(it => new ZoneTypeDto
                    {
                        Id = it.Id,
                        Name = it.Name
                    })));

            profile.CreateMap<ZoneType, ZoneTypeDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<ZoneTypeCreateDto, ZoneType>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<ZoneTypeUpdateDto, ZoneType>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));
        }
    }
}