using System.Data.Entity.ModelConfiguration;
using JDSVRCabineWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JDSVRCabineWeb.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TBJDSVRUsuario");
            builder.HasKey(x => x.Id);
            builder.Property(v => v.Id).HasColumnName("ID");
            builder.Property(v => v.Nome).IsRequired().HasMaxLength(110).HasColumnName("NOME");
            builder.Property(v => v.Situacao).IsRequired().HasMaxLength(3).HasColumnName("SITUACAO");
        }
    }
}