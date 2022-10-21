using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace CMS.WinService.WorkerService
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("TbEmpresa");
            builder.HasKey(x => x.Id).HasName("PkEmpresa");
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
        }
    }

    public class BancoDeDadosContext : DbContext
    {

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole(); });

        public DbSet<Empresa> Empresas { get; set; }

        public BancoDeDadosContext() 
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            // optionsBuilder.UseSqlServer(_config.GetConnectionString("DbConnJD")); // DbConnJD // DbConnNote
            optionsBuilder.UseSqlServer(@"Data Source=JDSP108;Initial Catalog=CMS_DOTNET;User ID=jddesenv;Password=jddesenv;Persist Security Info=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmpresaMapping());
            base.OnModelCreating(modelBuilder);
        }

    }

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConfiguration _configuration { get; }

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

           //_logger.LogInformation("ConnectionStrings:DbConnJD", _configuration["ConnectionStrings:DbConnJD"]);
            //_logger.LogInformation("GetConnectionString:DbConnJD", _configuration.GetConnectionString("DbConnJD"));
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("01 - StartAsync", DateTimeOffset.Now);
            // await Task.CompletedTask;
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            //_logger.LogCritical("02 - StopAsync", DateTimeOffset.Now);
            //await Task.CompletedTask;
            return base.StopAsync(cancellationToken);
        }

        //  protected override Task ExecuteAsync(CancellationToken stoppingToken);
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.DoWork(stoppingToken);
            //await base.ExecuteAsync(stoppingToken);
        }
        private async Task DoWork(CancellationToken stoppingToken)
        {

            _logger.LogDebug("DoWork.Incio");
            using (var context = new BancoDeDadosContext())
            {
                _logger.LogInformation("Lista de Empresa");
                var listaEmpresa = context.Empresas.ToList();
                foreach (Empresa empr in listaEmpresa)
                    _logger.LogWarning($" => {empr.Id.ToString()} - {empr.Nome}");
            }

            _logger.LogWarning("DoWork.LogWarning");
            _logger.LogTrace("DoWork.LogTrace");
            _logger.LogInformation("DoWork.LogInformation");
            _logger.LogError("DoWork.LogError");
            _logger.LogDebug("DoWork.Fim");
            _logger.LogCritical("DoWork.LogCritical");

            _logger.LogDebug("DoWork.Fim");

            //while (!stoppingToken.IsCancellationRequested)
            // {
            //_logger.LogInformation("03 LogInformation - Worker running at: {time}", DateTimeOffset.Now);
            //_logger.LogWarning("04  LogWarning- Worker running at: {time}", DateTimeOffset.Now);
            //_logger.LogError("05 LogError - Worker running at: {time}", DateTimeOffset.Now);
            //_logger.LogTrace("06 LogTrace - Worker running at: {time}", DateTimeOffset.Now);
            //_logger.LogCritical("07 LogCritical - Worker running at: {time}", DateTimeOffset.Now);
            //_logger.LogDebug("08 LogDebug - Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
            //}
        }

        //public void Dispose()
        //{
        //    //_timer?.Dispose();
        //}

    }
}
