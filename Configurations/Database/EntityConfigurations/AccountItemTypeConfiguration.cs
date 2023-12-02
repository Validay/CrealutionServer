using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class AccountItemTypeConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountItemType>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<AccountItemType>()
                .Property(p => p.Count)
                .IsRequired();
        }
    }
}