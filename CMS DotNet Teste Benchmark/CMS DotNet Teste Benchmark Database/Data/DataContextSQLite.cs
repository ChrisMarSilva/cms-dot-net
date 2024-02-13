//using Microsoft.EntityFrameworkCore;
//using System.Reflection;
//using TesteBenchmarkDotNet.Models;

//namespace TesteBenchmarkDotNet.Data;

//public sealed class DataContextSQLite : DbContext
//{
//    //public DataContextSQLite(DbContextOptions<DataContextSQLite> options)
//    //    : base(options) { }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        var connectionString = @"Data Source=C:\Users\chris\AppData\Roaming\DBeaverData\workspace6\.metadata\sample-database-sqlite-1\Chinook.db;Version=3;";
//        optionsBuilder.UseSqlite(connectionString);
//        //optionsBuilder.UseSqlite(connectionString).LogTo(Console.WriteLine, LogLevel.Information);
//    }

//    public DbSet<Cliente> Clientes { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

//        //DataContextSQLite
//        //modelBuilder.Entity<Cliente>(entity =>
//        //{
//        //    entity.ToTable("Cliente");
//        //    entity.Property(e => e.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
//        //    entity.Property(e => e.Limite).HasColumnName("Limite").IsRequired();
//        //});

//        base.OnModelCreating(modelBuilder);
//    }
//}
