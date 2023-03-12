using Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace Catalogo.Domain.Mappers;

public class BaseEntityMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    private readonly string _tableName;

    public BaseEntityMap(string tableName)
    {
        _tableName = tableName;
    }

    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        if (!string.IsNullOrEmpty(_tableName)) 
            builder.ToTable(_tableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id").HasColumnType("char(36)").HasDefaultValueSql("NEWID()").IsRequired();
        //builder.Property(e => e.Id).HasColumnName("id").HasColumnType("char(36)").ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.DataCadastro).HasColumnName("data_cadastro").IsRequired();
        builder.Property(x => x.DataAlteracao).HasColumnName("data_alteracao").IsRequired(false);
    }
}
