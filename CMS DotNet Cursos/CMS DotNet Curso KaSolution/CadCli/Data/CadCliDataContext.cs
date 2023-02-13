using CadCli.Models;
using CadCli.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Data
{
    public class CadCliDataContext : DbContext
    {

        public DbSet<Cliente> Clientes { get; set; }

        // private IConfiguration _config;
        private IConfigSistemaService _ConfigSistemaService;

        public CadCliDataContext(IConfigSistemaService ConfigSistemaService)
        {
            this._ConfigSistemaService = ConfigSistemaService;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // Modo 01
            // optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=cadclidb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            // Modo 02
            //var config = new ConfigSistemaService();
            //optionsBuilder.UseSqlServer(config.ObterDados().ConnString);

            // Modo 03
            optionsBuilder.UseSqlServer(this._ConfigSistemaService.Dados.ConnString);

            // Modo 04
            //optionsBuilder.UseSqlServer(this._config.GetConnectionString("CadCli"));

            // Modo 05
            //optionsBuilder.UseSqlServer(this._config["ConnectionStrings:CadCli"];

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente() { Id = 1, Nome = "Pessoa01", Idade = 11, DataCadastro = DateTime.Now },
                new Cliente() { Id = 2, Nome = "Pessoa02", Idade = 12, DataCadastro = DateTime.Now },
                new Cliente() { Id = 3, Nome = "Pessoa03", Idade = 13, DataCadastro = DateTime.Now },
                new Cliente() { Id = 4, Nome = "Pessoa04", Idade = 13, DataCadastro = DateTime.Now }
           );
        }

    }
}
