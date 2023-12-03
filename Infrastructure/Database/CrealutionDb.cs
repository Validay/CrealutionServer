using CrealutionServer.Domain.Entities;
using CrealutionServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace CrealutionServer.Infrastructure.Database
{
    public class CrealutionDb : DbContext
    {
        public virtual DbSet<BehaviorType> BehaviorTypes { get; set; }  
        public virtual DbSet<SickType> SickTypes { get; set; }  
        public virtual DbSet<MoveType> MoveTypes { get; set; }  
        public virtual DbSet<ZoneType> ZoneTypes { get; set; }  
        public virtual DbSet<StatisticType> StatisticTypes { get; set; }  
        public virtual DbSet<CharacteristicType> CharacteristicTypes { get; set; }  
        public virtual DbSet<CreatureStatisticType> CreatureStatisticTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Terrarium> Terrariums { get; set; }
        public virtual DbSet<ItemType> ItemTypes { get; set; }
        public virtual DbSet<AccountItemType> AccountItemTypes { get; set; }

        protected CrealutionDb()
        {
        }

        public CrealutionDb(DbContextOptions<CrealutionDb> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configurations = Assembly.GetAssembly(typeof(IEntityConfiguration))
                .GetTypes()
                .Where(type => typeof(IEntityConfiguration).IsAssignableFrom(type)
                    && !type.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEntityConfiguration>();

            foreach (var configuration in configurations)
                configuration.Configure(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}