using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class MoveTypeConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoveType>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<MoveType>()
                .HasIndex(k => k.Name)
                .IsUnique();

            modelBuilder.Entity<MoveType>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            //modelBuilder.Entity<MoveType>()
            //    .HasMany(x => x.AccountZoneTypes)
            //    .WithOne(x => x.ZoneType)
            //    .HasForeignKey(x => x.ZoneTypeId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}