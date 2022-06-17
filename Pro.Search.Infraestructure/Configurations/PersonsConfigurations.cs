using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pro.Search.PersonDomains.PersonEngine.Entities;

namespace Pro.Search.Infraestructure.Configurations
{
    public class PersonsConfigurations : IEntityTypeConfiguration<Persons>
    {
        public void Configure(EntityTypeBuilder<Persons> builder)
        {
            builder.ToTable("PESSOAS");
            builder.Property(p => p.Id_Pessoas).HasColumnName("ID_PESSOAS").HasColumnType("VARCHAR2");
            builder.Property(p => p.Nome).HasColumnName("NOME").HasColumnType("VARCHAR2");
            builder.Property(p => p.Sobrenome).HasColumnName("SOBRENOME").HasColumnType("VARCHAR2");
            builder.Property(p => p.Email).HasColumnName("EMAIL").HasColumnType("VARCHAR2");
            builder.Property(p => p.DataHora).HasColumnName("DATA_HORA").HasColumnType("DATE");
            builder.Property(p => p.Pessoas_Calc_Number).HasColumnName("PESSOAS_CALC_NUMBER").HasColumnType("NUMBER");
        }
    }
}
