using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pro.Search.PersonDomains.PersonEngine.Entities;

namespace Pro.Search.Infraestructure.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("USERS").HasKey(p => new { p.Id }); ;
            builder.Property(p => p.Id).HasColumnName("ID");
            builder.Property(p => p.Username).HasColumnName("USERNAME").HasColumnType("VARCHAR2");
            builder.Property(p => p.Password).HasColumnName("PASSWORD").HasColumnType("VARCHAR2");
            builder.Property(p => p.Role).HasColumnName("ROLES").HasColumnType("VARCHAR2");
        }
    }
}
