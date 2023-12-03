using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using CrealutionServer.Models.Dtos.ItemTypes;
using CrealutionServer.Models.Dtos.AccountItemTypes;
using System.Collections.Generic;
using System.Linq;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class AccountItemTypeMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<AccountItemType>, AccountItemTypeGetAllDto>()
                .ForMember(dest => dest.AccountItemTypes, opt => opt
                    .MapFrom(src => src.Select(ait => new AccountItemTypeDto
                    {
                        Id = ait.Id,
                        Count = ait.Count,
                        ItemType = new ItemTypeDto
                        {
                            Id = ait.ItemType.Id,
                            Name = ait.ItemType.Name
                        }
                    })));

            profile.CreateMap<AccountItemType, AccountItemTypeDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Count, opt => opt
                    .MapFrom(src => src.Count))
                .ForMember(dest => dest.ItemType, opt => opt
                    .MapFrom(src => new ItemTypeDto
                    {
                        Id = src.ItemType.Id,
                        Name = src.ItemType.Name
                    }));

            profile.CreateMap<AccountItemTypeCreateDto, AccountItemType>()
                .ForMember(dest => dest.Count, opt => opt
                    .MapFrom(src => src.Count));

            profile.CreateMap<AccountItemTypeUpdateDto, AccountItemType>()
                .ForMember(dest => dest.Count, opt => opt
                    .MapFrom(src => src.Count));
        }
    }
}