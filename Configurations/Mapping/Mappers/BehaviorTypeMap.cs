using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CrealutionServer.Models.Dtos.BehaviorTypes;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class BehaviorTypeMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<BehaviorType>, BehaviorTypeGetAllDto>()
                .ForMember(dest => dest.BehaviorTypes, opt => opt
                    .MapFrom(src => src.Select(it => new BehaviorTypeDto
                    {
                        Id = it.Id,
                        Name = it.Name
                    })));

            profile.CreateMap<BehaviorType, BehaviorTypeDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<BehaviorTypeCreateDto, BehaviorType>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<BehaviorTypeUpdateDto, BehaviorType>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));
        }
    }
}