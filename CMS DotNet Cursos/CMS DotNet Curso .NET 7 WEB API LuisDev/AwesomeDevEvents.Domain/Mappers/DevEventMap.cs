using AwesomeDevEvents.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeDevEvents.Domain.Mappers
{
    public class DevEventMap : BaseEntityMap<DevEvent>
    {
        public DevEventMap() : base("DevEvents") { }

        public override void Configure(EntityTypeBuilder<DevEvent> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title).IsRequired(false);
            builder.Property(x => x.Description).HasMaxLength(200).HasColumnType("varchar(200)"); //   .HasColumnName("CUSTOMER_NAME").HasColumnType("NVARCHAR(50)")
            builder.Property(x => x.StartDate).HasColumnName("Start_Date");
            builder.Property(x => x.EndDate).HasColumnName("End_Date");
            builder.HasMany(x => x.Speakers).WithOne().HasForeignKey(s => s.DevEventId);
        }
    }
}
