using Cache.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Cache.Web.Database.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {   }

    public DbSet<MensagemModel> Mensagens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}