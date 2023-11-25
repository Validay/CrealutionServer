using CrealutionServer.Domain.Entities;
using CrealutionServer.Helper.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace CrealutionServer.Infrastructure.Database
{
    public class CrealutionDb : DbContext
    {
        public DbSet<StatisticType> StatisticTypes { get; set; }
        public DbSet<CreatureStatisticType> CreatureStatisticTypes { get; set; }

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
