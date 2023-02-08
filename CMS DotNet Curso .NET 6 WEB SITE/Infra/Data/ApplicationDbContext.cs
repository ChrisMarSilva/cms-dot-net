namespace IWantApp.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

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

        // Products

        builder.Entity<Product>()
                .ToTable("Products");

        builder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired();

        builder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(255);

        builder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        //Orders

        builder.Entity<Order>()
                .ToTable("Orders");

        builder.Entity<Order>()
           .Property(o => o.ClientId).IsRequired();

        builder.Entity<Order>()
           .Property(o => o.DeliveryAddress).IsRequired();

        builder.Entity<Order>()
           .HasMany(o => o.Products)
           .WithMany(p => p.Orders)
           .UsingEntity(x => x.ToTable("OrderProducts"));
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }

}
