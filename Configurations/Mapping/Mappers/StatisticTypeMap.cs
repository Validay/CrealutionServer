using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using CrealutionServer.Models.Dtos.StatisticTypes;
using System.Collections.Generic;
using System.Linq;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class StatisticTypeMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<StatisticType>, StatisticTypeGetAllDto>()
                .ForMember(dest => dest.StatisticTypeDtos, opt => opt
                    .MapFrom(src => src.Select(statisticType => new StatisticTypeDto
                    {
                        Id = statisticType.Id,
                        Name = statisticType.Name
                    })));

            profile.CreateMap<StatisticType, StatisticTypeDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<StatisticTypeCreateDto, StatisticType>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<StatisticTypeUpdateDto, StatisticType>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));
        }
    }
}
