using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class CharacteristicTypeConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacteristicType>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<CharacteristicType>()
                .HasIndex(k => k.Name)
                .IsUnique();

            modelBuilder.Entity<CharacteristicType>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            //modelBuilder.Entity<CharacteristicType>()
            //    .HasMany(x => x.AccountZoneTypes)
            //    .WithOne(x => x.ZoneType)
            //    .HasForeignKey(x => x.ZoneTypeId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}