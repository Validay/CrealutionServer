using AutoMapper;
using CrealutionServer.Configurations.Mapping;

namespace CrealutionServer.Tests
{
    internal static class TestMapper
    {
        public static IMapper Mapper => _mapper;

        private static IMapper _mapper;

        static TestMapper()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new CrealutionMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();

                _mapper = mapper;
            }
        }
    }
}