using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Configurations;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;

namespace Pro.Search.Infraestructure.Context
{
    public sealed class SystemWriteDBContext : DbContext, ISystemWriteDBContext
    {
        public SystemWriteDBContext(DbContextOptions<SystemWriteDBContext> options) : base(options)
        {
            this.Pessoas = this.Set<Persons>();
            this.Food = this.Set<Food>();
            this.Users = this.Set<UserEntity>();
        }

        public DbSet<Persons> Pessoas { get; set; }

        public DbSet<Food> Food { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                // _ = modelBuilder.HasDefaultSchema("SYSTEM");
                _ = modelBuilder.ApplyConfiguration(new PersonsConfigurations());
                _ = modelBuilder.ApplyConfiguration(new FoodConfigurations());
                _ = modelBuilder.ApplyConfiguration(new UserConfigurations());
                base.OnModelCreating(modelBuilder);
            }
            else
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }
        }
    }
}
