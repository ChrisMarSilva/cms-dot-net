using Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.DataCadastro).HasColumnName("data_cadastro");
        builder.Property(x => x.DataAlteracao).HasColumnName("aata_alteracao");
    }
}
