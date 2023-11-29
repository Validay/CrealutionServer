using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Mapping.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CrealutionServer.Models.Dtos.Accounts;
using CrealutionServer.Models.Dtos.Roles;

namespace CrealutionServer.Configurations.Mapping.Mappers
{
    public class AccountMap : IMap
    {
        public void Map(CrealutionMappingProfile profile)
        {
            profile.CreateMap<ICollection<Account>, AccountGetAllDto>()
                .ForMember(dest => dest.Accounts, opt => opt
                    .MapFrom(src => src.Select(account => new AccountDto
                    {
                        Id = account.Id,
                        Name = account.Name,
                        DisplayName = account.DisplayName,
                        CreateDate = account.CreateDate,
                        InBanned = account.InBanned,
                        Roles = account.Roles.Select(ar => new RoleDto
                        {
                            Id = ar.Id,
                            Name = ar.Name
                        }).ToList()
                    })));

            profile.CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name))
                .ForMember(dest => dest.DisplayName, opt => opt
                    .MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.InBanned, opt => opt
                    .MapFrom(src => src.InBanned))
                .ForMember(dest => dest.CreateDate, opt => opt
                    .MapFrom(src => src.CreateDate))
                .ForMember(dest => dest.Roles, opt => opt
                    .MapFrom(src => src.Roles.Select(ar => new RoleDto
                    {
                        Id = ar.Id,
                        Name = ar.Name
                    }).ToList()));

            profile.CreateMap<Account, AccountAuthorizedDto>()
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.AccountInfo, opt => opt
                    .MapFrom(src => new AccountDto
                    {
                        Name = src.Name,
                        DisplayName = src.DisplayName,
                        CreateDate = src.CreateDate,
                        InBanned = src.InBanned,
                        Roles = src.Roles.Select(ar => new RoleDto
                        {
                            Id = ar.Id,
                            Name = ar.Name
                        }).ToList()
                    }));

            profile.CreateMap<AccountCreateDto, Account>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name))
                .ForMember(dest => dest.DisplayName, opt => opt
                    .MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Password, opt => opt
                    .MapFrom(src => src.Password));

            profile.CreateMap<AccountUpdateInfoDto, Account>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.DisplayName, opt => opt
                    .MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Password, opt => opt
                    .MapFrom(src => src.Password));

            profile.CreateMap<AccountRegistrationDto, Account>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name))
                .ForMember(dest => dest.DisplayName, opt => opt
                    .MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Password, opt => opt
                    .MapFrom(src => src.Password));

            profile.CreateMap<AccountLoginDto, Account>()
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name))
                .ForMember(dest => dest.Password, opt => opt
                    .MapFrom(src => src.Password));

            profile.CreateMap<AccountUpdateDto, Account>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt
                    .MapFrom(src => src.Name))
                .ForMember(dest => dest.DisplayName, opt => opt
                    .MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Password, opt => opt
                    .MapFrom(src => src.Password))
                .ForMember(dest => dest.InBanned, opt => opt
                    .MapFrom(src => src.InBanned))
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}