using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class BehaviorTypeConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BehaviorType>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<BehaviorType>()
                .HasIndex(k => k.Name)
                .IsUnique();

            modelBuilder.Entity<BehaviorType>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            //modelBuilder.Entity<BehaviorType>()
            //    .HasMany(x => x.AccountBehaviorTypes)
            //    .WithOne(x => x.BehaviorType)
            //    .HasForeignKey(x => x.BehaviorTypeId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}