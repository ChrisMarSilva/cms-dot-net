using AwesomeDevEvents.API.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Maps
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
