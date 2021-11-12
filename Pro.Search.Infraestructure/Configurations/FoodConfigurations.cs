using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pro.Search.PersonDomains.PersonEngine.Entities;

namespace Pro.Search.Infraestructure.Configurations
{
    public class FoodConfigurations : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.ToTable("FOOD");
            builder.Property(p => p.Id_Food).HasColumnName("ID_FOOD").HasColumnType("VARCHAR2");
            builder.Property(p => p.Name_Food).HasColumnName("NAME_FOOD").HasColumnType("VARCHAR2");
            builder.Property(p => p.Locale_Purcache_Food).HasColumnName("LOCALE_PURCACHE_FOOD").HasColumnType("VARCHAR2");
            builder.Property(p => p.Id_Pessoas_References).HasColumnName("ID_PESSOAS_REFERENCES").HasColumnType("VARCHAR2");
            builder.Property(p => p.Price_Food).HasColumnName("PRICE_FOOD").HasColumnType("NUMBER");
        }
    }
}
