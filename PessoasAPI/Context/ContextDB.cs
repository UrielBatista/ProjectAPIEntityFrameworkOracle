using Microsoft.EntityFrameworkCore;
using PessoasAPI.Model;
using PessoasAPI.Model.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PessoasAPI.Context
{
    public class ContextDB : DbContext
    {
        public DbSet<Pessoas> Pessoas { get; set; }

        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoasConfigurations());
        }

    }
}
