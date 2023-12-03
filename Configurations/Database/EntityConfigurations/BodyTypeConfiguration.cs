using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class BodyTypeConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BodyType>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<BodyType>()
                .HasIndex(k => k.Name)
                .IsUnique();

            modelBuilder.Entity<BodyType>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<BodyType>()
                .Property(p => p.ImageData)
                .IsRequired();

            //modelBuilder.Entity<BodyType>()
            //    .HasMany(x => x.AccountBodyTypes)
            //    .WithOne(x => x.BodyType)
            //    .HasForeignKey(x => x.BodyTypeId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}