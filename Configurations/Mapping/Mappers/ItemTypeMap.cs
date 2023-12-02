using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CrealutionServer.Models.Dtos.ItemTypes;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class ItemTypeMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<ItemType>, ItemTypeGetAllDto>()
                .ForMember(dest => dest.ItemTypes, opt => opt
                    .MapFrom(src => src.Select(statisticType => new ItemTypeDto
                    {
                        Id = statisticType.Id,
                        Name = statisticType.Name
                    })));

            profile.CreateMap<ItemType, ItemTypeDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<ItemTypeCreateDto, ItemType>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<ItemTypeUpdateDto, ItemType>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));
        }
    }
}