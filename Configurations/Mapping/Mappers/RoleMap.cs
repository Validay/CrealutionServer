using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CrealutionServer.Models.Dtos.Roles;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class RoleMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<Role>, RoleGetAllDto>()
                .ForMember(dest => dest.Roles, opt => opt
                    .MapFrom(src => src.Select(statisticType => new RoleDto
                    {
                        Id = statisticType.Id,
                        Name = statisticType.Name
                    })));

            profile.CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<RoleCreateDto, Role>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<RoleUpdateDto, Role>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));
        }
    }
}