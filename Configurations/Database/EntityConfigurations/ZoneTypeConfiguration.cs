using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class ZoneTypeConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ZoneType>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<ZoneType>()
                .HasIndex(k => k.Name)
                .IsUnique();

            modelBuilder.Entity<ZoneType>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            //modelBuilder.Entity<ZoneType>()
            //    .HasMany(x => x.AccountZoneTypes)
            //    .WithOne(x => x.ZoneType)
            //    .HasForeignKey(x => x.ZoneTypeId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}