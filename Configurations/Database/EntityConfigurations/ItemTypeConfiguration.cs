using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class ItemTypeConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemType>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Role>()
                .HasIndex(k => k.Name)
                .IsUnique();

            modelBuilder.Entity<ItemType>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<ItemType>()
                .HasMany(x => x.AccountItemTypes)
                .WithOne(x => x.ItemType)
                .HasForeignKey(x => x.ItemTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}