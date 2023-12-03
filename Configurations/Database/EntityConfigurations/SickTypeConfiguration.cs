using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class SickTypeConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SickType>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<SickType>()
                .HasIndex(k => k.Name)
                .IsUnique();

            modelBuilder.Entity<SickType>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            //modelBuilder.Entity<SickType>()
            //    .HasMany(x => x.AccountSickTypes)
            //    .WithOne(x => x.SickType)
            //    .HasForeignKey(x => x.SickTypeId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}