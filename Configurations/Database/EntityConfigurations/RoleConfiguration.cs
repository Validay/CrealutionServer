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
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}