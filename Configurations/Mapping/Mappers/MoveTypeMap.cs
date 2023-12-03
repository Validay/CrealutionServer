using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CrealutionServer.Models.Dtos.MoveTypes;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class MoveTypeMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<MoveType>, MoveTypeGetAllDto>()
                .ForMember(dest => dest.MoveTypes, opt => opt
                    .MapFrom(src => src.Select(mt => new MoveTypeDto
                    {
                        Id = mt.Id,
                        Name = mt.Name
                    })));

            profile.CreateMap<MoveType, MoveTypeDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<MoveTypeCreateDto, MoveType>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<MoveTypeUpdateDto, MoveType>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));
        }
    }
}