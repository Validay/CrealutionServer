using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using CrealutionServer.Helper.Mapping.Interfaces;

namespace CrealutionServer.Helper.Mapping
{
    public class CrealutionMappingProfile : Profile
    {
        public CrealutionMappingProfile()
        {
            var mappers = Assembly.GetAssembly(typeof(IMap))
               .GetTypes()
               .Where(type => typeof(IMap).IsAssignableFrom(type)
                   && !type.IsInterface)
               .Select(Activator.CreateInstance)
               .Cast<IMap>();

            foreach (var mapper in mappers)
                mapper.Map(this);
        }
    }
}
