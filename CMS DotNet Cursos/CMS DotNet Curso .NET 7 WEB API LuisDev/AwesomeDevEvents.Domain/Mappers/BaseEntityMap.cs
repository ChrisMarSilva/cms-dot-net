using AwesomeDevEvents.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeDevEvents.Domain.Mappers
{
    public class BaseEntityMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        private readonly string _tableName;

        public BaseEntityMap(string tableName)
        {
            _tableName = tableName;
        }
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            if (!string.IsNullOrEmpty(_tableName)) builder.ToTable(_tableName);

            builder.HasKey(x => x.Id); // .HasName("PK_CUSTOMER_ID");
            builder.Property(x => x.Id).HasColumnName("id"); //.ValueGeneratedOnAdd();  .HasColumnType("INT");
        }
    }
}
