using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Helper.Database.EntityConfigurations.Interfaces
{
    public interface IEntityConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}