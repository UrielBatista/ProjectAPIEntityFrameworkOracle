using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PessoasAPI.Model.Configurations
{
    public class PessoasConfigurations : IEntityTypeConfiguration<Pessoas>
    {
        public void Configure(EntityTypeBuilder<Pessoas> builder)
        {
            builder.ToTable("PESSOAS");
            builder.Property(p => p.Id_Pessoas).HasColumnName("ID_PESSOAS");
            builder.Property(p => p.Nome).HasColumnName("NOME");
            builder.Property(p => p.Sobrenome).HasColumnName("SOBRENOME");
            builder.Property(p => p.DataHora).HasColumnName("DATA_HORA");
            builder.Property(p => p.Pessoas_Calc_Number).HasColumnName("PESSOAS_CALC_NUMBER");
        }
    }
}
