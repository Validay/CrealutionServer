using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CrealutionServer.Models.Dtos.BodyTypes;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class BodyTypeMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<BodyType>, BodyTypeGetAllDto>()
                .ForMember(dest => dest.BodyTypes, opt => opt
                    .MapFrom(src => src.Select(it => new BodyTypeDto
                    {
                        Id = it.Id,
                        Name = it.Name,
                        ImageData = it.ImageData
                    })));

            profile.CreateMap<BodyType, BodyTypeDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name))
                .ForMember(dest => dest.ImageData, opt => opt
                    .MapFrom(src => src.ImageData));

            profile.CreateMap<BodyTypeCreateDto, BodyType>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name))
                .ForMember(dest => dest.ImageData, opt => opt
                    .MapFrom(src => src.ImageData));

            profile.CreateMap<BodyTypeUpdateDto, BodyType>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name))
                .ForMember(dest => dest.ImageData, opt => opt
                    .MapFrom(src => src.ImageData));
        }
    }
}