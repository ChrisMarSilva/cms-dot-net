using CMS_WebAPI_OAuth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS_WebAPI_OAuth.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.AccessFailedCount).IsRequired().HasColumnType("numeric(10)");
        builder.Property(x => x.EmailConfirmed).IsRequired().HasColumnType("numeric(1)");
        builder.Property(x => x.PhoneNumberConfirmed).IsRequired().HasColumnType("numeric(1)");
        builder.Property(x => x.TwoFactorEnabled).IsRequired().HasColumnType("numeric(1)");
        builder.Property(x => x.LockoutEnabled).IsRequired().HasColumnType("numeric(1)");
    }
}