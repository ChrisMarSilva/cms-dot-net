using Core.CMS.Data.Mappings;
using Core.CMS.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using Microsoft.EntityFrameworkCore.Proxies;

namespace Core.CMS.Data.Contexts
{
    public class BancoDeDadosContext : DbContext
    {

        private IConfiguration _config;
        private IHostingEnvironment _env;

        public DbSet<Empresa> Empresas { get; set; }

        public BancoDeDadosContext(IConfiguration config, IHostingEnvironment env)
        {
            this._config = config;
            this._env = env;
            //if (env.IsDevelopment()) Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies().UseSqlServer(this._config.GetConnectionString("DbConnJD")); // DbConnJD // DbConnNote
            optionsBuilder.UseSqlServer(this._config.GetConnectionString("DbConnJD")); // DbConnJD // DbConnNote
            // optionsBuilder.EnableSensitiveDataLogging(true).UseLoggerFactory(new Microsoft.Extensions.Logging.LoggerFactory().AddConsole((category, level) => level == LogLevel.Information && category == DbLoggerCategory.Database.Command.Name, true));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //if (this._env.IsDevelopment())  modelBuilder.Seed();
            modelBuilder.ApplyConfiguration(new EmpresaMapping());
            base.OnModelCreating(modelBuilder);
        }

    }

}
