using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Data;

public sealed class DataContextSQLServer : DbContext
{
    public DataContextSQLServer()
    {
    }

    public DataContextSQLServer(DbContextOptions<DataContextSQLServer> options) : base(options)
    {
        // DataContextSQLServer.Database.SetCommandTimeout(400);
    }

    public DbSet<ClienteModel> Clientes { get; set; }
    public DbSet<ClienteCarteiraModel> Carteiras { get; set; }
    public DbSet<ClienteTransacaoModel> Transacoes { get; set; }
    //public DbSet<Cliente> Clientes { get; set; }
    //public DbSet<Author> Authors { get; set; }
    //public DbSet<AuthorBiography> AuthorBiographies { get; set; }
    //public DbSet<Book> Books { get; set; }
    //public DbSet<Category> Categories { get; set; }
    //public DbSet<Company> Companies { get; set; }
    //public DbSet<Employee> Employees { get; set; }
    //public DbSet<BookCategory> BookCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = @"Data Source=127.0.0.1,5402;Initial Catalog=RinhaBackend2024;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.Information);
            //optionsBuilder.UseSqlServer(connectionString).ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DataContextSQLServer))!);
        //modelBuilder.ApplyConfiguration(new ClienteConfiguration());

        //modelBuilder.Entity<Cliente>(entity =>
        //{
        //    entity.ToTable("Cliente");
        //    entity.Property(e => e.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
        //    entity.Property(e => e.Limite).HasColumnName("Limite").IsRequired();
        //});

        ////Company
        //modelBuilder.Entity<Company>().HasMany(c => c.Employees).WithOne(e => e.Company);

        ////Author
        //modelBuilder.Entity<Author>().HasOne(a => a.Biography).WithOne(b => b.Author).HasForeignKey<AuthorBiography>(b => b.AuthorId);

        ////BookCategory
        //modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
        //modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Book).WithMany(b => b.BookCategories).HasForeignKey(bc => bc.BookId);
        //modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Category).WithMany(c => c.BookCategories).HasForeignKey(bc => bc.CategoryId);
    }

    public SqlConnection GetSqlConnection()
    {
        var sqlConnection = Database.GetDbConnection() as SqlConnection;
        if (sqlConnection == null) throw new InvalidOperationException("Não é possível obter a conexão SQL.");
        return sqlConnection;
    }
}
