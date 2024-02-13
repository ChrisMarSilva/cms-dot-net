//using Flunt.Notifications;
//using Microsoft.EntityFrameworkCore;
//using Rinha.Backend._2024.API.Context.Interfaces;
//using Rinha.Backend._2024.API.Models;
//using System.Data;
//using System.Reflection;

//namespace Rinha.Backend._2024.API.Context;

//public sealed class AppDbContext : DbContext, IDataContext
//{
//    public AppDbContext(DbContextOptions<AppDbContext> options) 
//        : base(options) { }

//    public DbSet<ClienteModel> Clientes { get; set; }
//    public DbSet<ClienteCarteiraModel> Carteiras { get; set; }
//    public DbSet<ClienteTransacaoModel> Transacoes { get; set; }

//    protected override void OnModelCreating(ModelBuilder builder)
//    {
//        builder.Ignore<Notification>();
//        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext))!);
//        base.OnModelCreating(builder);
//    }

//    public async Task OpenConnection()
//    {
//        if (Database.GetDbConnection().State != ConnectionState.Open)
//            try { await Database.OpenConnectionAsync(); } catch { }
//    }
//}
