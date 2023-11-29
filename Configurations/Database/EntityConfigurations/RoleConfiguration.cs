using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class RoleConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Role>()
                .HasIndex(k => k.Name)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Role>()
               .HasMany(x => x.Accounts)
               .WithMany(x => x.Roles);
        }
    }
}