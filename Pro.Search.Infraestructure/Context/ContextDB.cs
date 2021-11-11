using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Configurations;
using Pro.Search.PersonDomains.PersonEngine.Entities;

namespace Pro.Search.Infraestructure.Context
{
    public class ContextDB : DbContext
    {
        public DbSet<Pessoas> Pessoas { get; set; }

        public DbSet<Food> Food { get; set; }

        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoasConfigurations());
            modelBuilder.ApplyConfiguration(new FoodConfigurations());
        }

    }
}
