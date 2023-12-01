using CrealutionServer.Infrastructure.Services.Interfaces;
using Moq;

namespace CrealutionServer.Tests
{
    internal static class TestCacheService
    {
        public static ICacheService GetTestCacheService() => new Mock<ICacheService>().Object;
    }
}