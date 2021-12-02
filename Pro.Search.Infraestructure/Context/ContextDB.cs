using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Configurations;
using Pro.Search.PersonDomains.PersonEngine.Entities;

namespace Pro.Search.Infraestructure.Context
{
    public sealed class ContextDB : DbContext, IContextDB
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
            this.Pessoas = this.Set<Pessoas>();
            this.Food = this.Set<Food>();
        }

        public DbSet<Pessoas> Pessoas { get; set; }

        public DbSet<Food> Food { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoasConfigurations());
            modelBuilder.ApplyConfiguration(new FoodConfigurations());
            base.OnModelCreating(modelBuilder);
        }

    }
}
