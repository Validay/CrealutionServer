using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class TerrariumConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Terrarium>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Terrarium>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}