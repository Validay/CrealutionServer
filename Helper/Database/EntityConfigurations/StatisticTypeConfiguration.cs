using CrealutionServer.Domain.Entities;
using CrealutionServer.Helper.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Helper.Database.EntityConfigurations
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