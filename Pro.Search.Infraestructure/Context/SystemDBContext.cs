using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Configurations;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;

namespace Pro.Search.Infraestructure.Context
{
    public sealed class SystemDBContext : DbContext, ISystemDBContext
    {
        public SystemDBContext(DbContextOptions<SystemDBContext> options) : base(options)
        {
            this.Pessoas = this.Set<Persons>();
            this.Food = this.Set<Food>();
        }

        public DbSet<Persons> Pessoas { get; set; }

        public DbSet<Food> Food { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                _ = modelBuilder.HasDefaultSchema("SYSTEM");
                _ = modelBuilder.ApplyConfiguration(new PersonsConfigurations());
                _ = modelBuilder.ApplyConfiguration(new FoodConfigurations());
                base.OnModelCreating(modelBuilder);
            }
            else
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }
        }
    }
}
