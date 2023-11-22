using CrealutionServer.Domain.Entities;
using CrealutionServer.Helper.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Helper.Database.EntityConfigurations
{
    public class CreatureStatisticTypeConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreatureStatisticType>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<CreatureStatisticType>()
                .HasOne(p => p.StatisticType)
                .WithMany(p => p.CreatureStatisticTypes)
                .HasForeignKey(k => k.StatisticTypeId);
        }
    }
}