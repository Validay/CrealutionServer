using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CrealutionServer.Models.Dtos.Terrariums;
using CrealutionServer.Models.Dtos.Accounts;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class TerrariumMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<Terrarium>, TerrariumGetAllDto>()
                .ForMember(dest => dest.Terrariums, opt => opt
                    .MapFrom(src => src.Select(t => new TerrariumDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        AccountLightInfo = new AccountLightDto
                        {
                            Id = t.Account.Id,
                            DisplayName = t.Account.DisplayName,
                            CreateDate = t.Account.CreateDate,
                            InBanned = t.Account.InBanned
                        }
                    })));

            profile.CreateMap<Terrarium, TerrariumDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name))
                .ForMember(dest => dest.AccountLightInfo, opt => opt
                    .MapFrom(src => new AccountLightDto
                    {
                        Id = src.Account.Id,
                        DisplayName = src.Account.DisplayName,
                        CreateDate = src.Account.CreateDate,
                        InBanned = src.Account.InBanned
                    }));

            profile.CreateMap<Terrarium, TerrariumLightDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<TerrariumCreateDto, Terrarium>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));

            profile.CreateMap<TerrariumUpdateDto, Terrarium>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name));
        }
    }
}