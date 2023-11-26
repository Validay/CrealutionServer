using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class StatisticTypeConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatisticType>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<StatisticType>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}