﻿using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionServer.Configurations.Database.EntityConfigurations
{
    public class AccountConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Account>()
                .HasIndex(k => k.Name)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .Property(p => p.DisplayName)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .Property(p => p.Password)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .HasMany(x => x.Roles)
                .WithMany(x => x.Accounts);
        }
    }
}