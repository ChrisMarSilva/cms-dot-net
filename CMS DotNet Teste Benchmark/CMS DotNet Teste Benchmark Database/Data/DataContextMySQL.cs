//using Microsoft.EntityFrameworkCore;
//using TesteBenchmarkDotNet.Models;

//namespace TesteBenchmarkDotNet.Data;

//public sealed class DataContextMySQL : DbContext
//{
//    //public DataContextMySQL(DbContextOptions<DataContextMySQL> options)
//    //    : base(options) { }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        //var connectionString = @"server=localhost;port=3306;database=rinha_backend_2024;user=root;password=Chrs8723;";
//        var connectionString = @"Server=localhost;port=3306;Database=rinha_backend_2024;Uid=root;Pwd=Chrs8723;";
//        //optionsBuilder.UseMySQL(connectionString);
//        //optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(5, 6, 0)));
//        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//        // optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).LogTo(Console.WriteLine, LogLevel.Information);
//    }

//    public DbSet<Cliente> Clientes { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

//        modelBuilder.Entity<Cliente>(entity =>
//        {
//            entity.ToTable("Cliente");
//            entity.Property(e => e.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
//            entity.Property(e => e.Limite).HasColumnName("Limite").IsRequired();
//        });

//        // base.OnModelCreating(modelBuilder);
//    }
//}
