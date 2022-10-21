using FanSoft.Sotre.Domain.Entities;
using FanSoft.Sotre.Domain.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.Data.EF
{
    public class StoreDataContext : DbContext
    {

        private IConfiguration _config;
        private IHostingEnvironment _env;

        public DbSet<Usuario>   Usuarios   { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto>   Produtos   { get; set; }

        public StoreDataContext(IConfiguration config, IHostingEnvironment env)
        {
            this._config = config;
            this._env    = env;
            if (env.IsDevelopment()) Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._config.GetConnectionString("StoreConn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (this._env.IsDevelopment())  modelBuilder.Seed();
        }

    }
}

