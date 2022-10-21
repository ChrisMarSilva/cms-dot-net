using JDSVRCabineWeb.Models;
using JDSVRCabineWeb.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JDSVRCabineWeb.Data
{
    public class DataContext : DbContext
    {

        private IConfiguration _config;
        public DbSet<Usuario> Usuarios { get; set; }

        public DataContext(IConfiguration config)
        {
            this._config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._config.GetConnectionString("Conn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // DbModelBuilder
        {
            // modelBuilder.Configurations.Add<Usuario>(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            base.OnModelCreating(modelBuilder);
        }

    }
}
