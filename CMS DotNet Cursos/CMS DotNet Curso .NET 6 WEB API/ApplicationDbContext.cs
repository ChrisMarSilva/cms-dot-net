using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{

    #region DbSet

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Tag> Tags { get; set; }

    #endregion 

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // protected override void OnConfiguring(DbContextOptionsBuilder option, IConfiguration configuration)
    // {
    //     option.UseSqlServer(configuration["database:sqlserver"]); // Database:SqlServer
    // }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        // Categories

        builder.Entity<Category>()
            .ToTable("Categories");

        builder.Entity<Category>()
            .Property(p => p.Name)
            .HasMaxLength(120)
            .IsRequired();

        // Products

        builder.Entity<Product>()
            .ToTable("Products");

        builder.Entity<Product>()
            .Property(p => p.Code)
            .HasMaxLength(20)
            .IsRequired();

        builder.Entity<Product>()
            .Property(p => p.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        // Tag

        builder.Entity<Tag>()
            .ToTable("Tags");

        builder.Entity<Tag>()
            .Property(p => p.Name)
            .HasMaxLength(120)
            .IsRequired();
    }

}
