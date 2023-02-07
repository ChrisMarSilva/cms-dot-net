using Flunt.Notifications;
using IWantApp.Domain.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<Notification>();

        // Categories

        builder.Entity<Category>()
            .ToTable("Categories");

        builder.Entity<Category>()
            .Property(p => p.Name)
            .IsRequired();

        //builder.Entity<Category>()
        //    .Property(p => p.CreatedBy)
        //    .HasMaxLength(120)
        //    .IsRequired();

        //builder.Entity<Category>()
        //    .Property(p => p.CreatedOn)
        //    .IsRequired();

        //builder.Entity<Category>()
        //    .Property(p => p.EditedBy)
        //    .HasMaxLength(120)
        //    .IsRequired(false);

        //builder.Entity<Category>()
        //    .Property(p => p.EditedOn)
        //    .IsRequired(false);

        // Products

        builder.Entity<Product>()
                .ToTable("Products");

        builder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired();

        builder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(255);

        //builder.Entity<Product>()
        //    .Property(p => p.IsStock)
        //    .IsRequired(false);

        //builder.Entity<Product>()
        //    .Property(p => p.CreatedBy)
        //    .HasMaxLength(120)
        //    .IsRequired();

        //builder.Entity<Product>()
        //    .Property(p => p.CreatedOn)
        //    .IsRequired();

        //builder.Entity<Product>()
        //    .Property(p => p.EditedBy)
        //    .HasMaxLength(120)
        //    .IsRequired(false);

        //builder.Entity<Product>()
        //    .Property(p => p.EditedOn)
        //    .IsRequired(false);

    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }

}
