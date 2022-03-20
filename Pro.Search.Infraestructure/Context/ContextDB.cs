using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Configurations;
using Pro.Search.PersonDomains.PersonEngine.Entities;

namespace Pro.Search.Infraestructure.Context
{
    public sealed class ContextDB : DbContext, IContextDB
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
            this.Pessoas = this.Set<Persons>();
            this.Food = this.Set<Food>();
        }

        public DbSet<Persons> Pessoas { get; set; }

        public DbSet<Food> Food { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonsConfigurations).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
