using Microsoft.EntityFrameworkCore;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=.;Database=MyDatabase1;Trusted_Connection=True;ConnectRetryCount=0");
            optionsBuilder.UseSqlServer(@"Data Source=127.0.0.1,5402;Initial Catalog=DevEvents;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Trusted_Connection=True;Integrated Security=true;MultipleActiveResultSets=true;");
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBiography> AuthorBiographies { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Company
            modelBuilder.Entity<Company>().HasMany(c => c.Employees).WithOne(e => e.Company);

            //Author
            modelBuilder.Entity<Author>().HasOne(a => a.Biography).WithOne(b => b.Author).HasForeignKey<AuthorBiography>(b => b.AuthorId);

            //BookCategory
            modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Book).WithMany(b => b.BookCategories).HasForeignKey(bc => bc.BookId);
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Category).WithMany(c => c.BookCategories).HasForeignKey(bc => bc.CategoryId);
        }
    }
}
