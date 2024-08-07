﻿using Catalogo.Domain.Models;
using Flunt.Notifications;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalogo.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser> // IdentityDbContext // DbContext
{
    //private readonly IConfiguration _configuration;

    public AppDbContext() // IConfiguration configuration
    {
        //_configuration = configuration;
    }

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

    // dotnet tool install --global dotnet-ef
    // dotnet tool update --global dotnet-ef
    // dotnet build 

    // dotnet ef 
    // dotnet ef migrations add AddTablesInitOnDataTablesDb
    // dotnet ef migrations add AddDadosTables01
    // dotnet ef migrations add PopulaCategorias
    // dotnet ef migrations add PopulaCategorias02
    // dotnet ef migrations add PopulaProdutos
    // dotnet ef migrations add AddIdentity
    // dotnet ef database update
    // dotnet ef migrations remove

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public virtual DbSet<Aluno> Alunos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder opt)
    {
        //opt.EnableSensitiveDataLogging();
        //opt.EnableServiceProviderCaching();
        //opt.LogTo(Console.WriteLine, LogLevel.Information);
        if (!opt.IsConfigured)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //   .SetBasePath(Directory.GetCurrentDirectory())
            //   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //   .Build();'
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            var connectionString = "Server=localhost;Port=3306;Database=catalogo_api;Uid=root;Pwd=Chrs8723;Persist Security Info=False;Connect Timeout=300;Connection Reset=False;Max Pool Size=300;";
            //var connectionString = _configuration.GetConnectionString("DefaultConnection");
            //var connectionString = Configuration.GetConnectionString("DefaultConnection")
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        base.OnConfiguring(opt);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<Notification>();

        // builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        // builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // builder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration).Assembly);
        // builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext))!);

        // builder.ApplyConfiguration(new CategoriaConfiguration());
        // builder.ApplyConfiguration(new ProdutoConfiguration());
        // builder.ApplyConfiguration(new AlunoConfiguration());


        // // Categoria

        //var Categ1 = new Categoria { Nome = "Categora 01" };
        //var Categ2 = new Categoria { Nome = "Categora 02" };
        //var Categ3 = new Categoria { Nome = "Categora 03" };

        //builder.Entity<Categoria>().HasData(Categ1);
        //builder.Entity<Categoria>().HasData(Categ2);
        //builder.Entity<Categoria>().HasData(Categ3);

        // // Produto

        //var Prod1 = new Produto { Nome = "Produto 01", Preco = 10, Estoque = 100, CategoriaId = Categ1.Id };
        //var Prod2 = new Produto { Nome = "Produto 02", Preco = 20, Estoque = 200, CategoriaId = Categ2.Id };
        //var Prod3 = new Produto { Nome = "Produto 03", Preco = 30, Estoque = 300, CategoriaId = Categ3.Id };

        //builder.Entity<Produto>().HasData(Prod1);
        //builder.Entity<Produto>().HasData(Prod2);
        //builder.Entity<Produto>().HasData(Prod3);

        //    builder.Entity<Aluno>().HasData(
        //        new Aluno { Id = 1, Nome = "Maria da Penha", Email = "mariapenha@yahoo.com", Idade = 23 },
        //        new Aluno { Id = 2, Nome = "Manuel Bueno", Email = "manuelbueno@yahoo.com", Idade = 22 }
        //    );

        base.OnModelCreating(builder);
    }

}
