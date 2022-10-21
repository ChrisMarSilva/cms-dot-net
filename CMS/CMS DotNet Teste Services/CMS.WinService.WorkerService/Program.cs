using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Diagnostics;

namespace CMS.WinService.WorkerService
{
    public class Program
    {
       

        public static void Main(string[] args)
        {

            // Nuget Add Serilog.AspNetCore
            // Nuget Add Serilog.Sinks.File
            // Nuget Add Microsoft.Extensions.Hosting.WindowsServices
            // sc.exe create "CMSMyWorkerService" binPath= "D:\CMS Proj Teste\CMS DotNet\CMS DotNet Teste Services\CMS.WinService.WorkerService\bin\CMS Publish\CMS.WinService.WorkerService.exe"
            // sc.exe delete "CMSMyWorkerService"


            //ConnectionStrings.DbConnJD

            //System.Console.WriteLine("Key0: " + System.Configuration.ConfigurationManager.AppSettings.Get("Key0"));
           // System.Console.WriteLine("Key0: " + System.Configuration.ConfigurationManager.ConnectionStrings["DbConnJD"].ConnectionString);


            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", true)
            .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                //.MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning) // Microsoft.AspNetCore
                .Enrich.FromLogContext()
                // .Enrich.WithExceptionDetails()
                .Enrich.WithAssemblyName()
                .Enrich.WithAssemblyVersion()
                //.Enrich.WithMachineName()
                .Enrich.WithUserName()
                //.Enrich.WithEnvironmentUserName()
                .Enrich.WithMemoryUsage()
               //.Enrich.WithEnvironment("OS")
               //.Enrich.WithEnrichedProperties()
               //.Enrich.WithFunction("f1", () => DateTime.Now.Ticks.ToString())
               // .Enrich.WithProcessId()
               //  .Enrich.WithProcessName()
               .Enrich.WithProperty("Application", "JDKB")
               .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
               .ReadFrom.Configuration(configuration)
               //.WriteTo.Async(a =>
               //{
               //    a.MSSqlServer(configuration.GetConnectionString("JDConexao"), "TBJKB_LOG", period: TimeSpan.FromMilliseconds(500), columnOptions: options);
               //    a.ColoredConsole(LogEventLevel.Verbose, "{NewLine}{Timestamp:HH:mm:ss.fff} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception} {Properties:j}");
               //    a.File(new CompactJsonFormatter(), ".\\logs\\log_.txt", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 1024 * 1024 * 20, buffered: true, flushToDiskInterval: TimeSpan.FromSeconds(5), rollOnFileSizeLimit: true);
               //}, bufferSize: 500)
                //  .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff zzz} [{Level:u3}] {Message:l}{NewLine}{Exception}")  //                //.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] ({SourceContext}) ({ThreadId}): {Message:lj}{NewLine}{Exception}") //{SourceContext}  
                //  .WriteTo.File(@"D:\CMSWorkerServiceLogs-.txt", shared: true, rollOnFileSizeLimit: true, fileSizeLimitBytes: 1234567,                rollingInterval: RollingInterval.Day,                 outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} || {Level:u3} || {UserName} || {AssemblyName} || {AssemblyVersion} || {MemoryUsage} || {Message} || {NewLine}")
                //.WriteTo.Logentries("token")
                //.WriteTo.Seq("http://localhost:5341", compact: true)
                // .WriteTo.RollingFile(@"D:\CMSWorkerServiceLogs-{HalfHour}.txt", shared: true, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();

//            CREATE TABLE [dbo].[TABELA](
//   [Id] [int] IDENTITY(1,1) NOT NULL,
//   [Message] [nvarchar](max) NULL,
//   [MessageTemplate] [nvarchar](max) NULL,
//   [Level] [nvarchar](128) NULL,
//   [TimeStamp] [datetime] NOT NULL,
//   [Exception] [nvarchar](max) NULL,
//   [LogEvent] [nvarchar](max) NULL,
//CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
//(
//   [Id] ASC
//)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
//) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
//GO



            try
            {
                // Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
                // Serilog.Debugging.SelfLog.Enable(Console.Error);
                // Log.Information("MachineName: "+Environment.MachineName);
                // Log.Information("UserDomainName: " + Environment.UserDomainName);
                // Log.Information("UserName: " + Environment.UserName);
                // Log.Information("CurrentDirectory: " + Environment.CurrentDirectory);
                // Log.Information("StackTrace: " + Environment.StackTrace);
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //Log.Fatal(ex, "0003 - Erro ao iniciar CMS.WinService.WorkerService");
                //Log.Error($"0002 - Erro ao iniciar CMS.WinService.WorkerService - {ex.Message}");
            }
            finally
            {
                //Log.Information("0004 - Fim do Iniciando CMS.WinService.WorkerService");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog() //(ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration)
            .UseWindowsService()
            //.ConfigureLogging(logger => {
            //    logger.AddSerilog();
            //})
            .ConfigureServices((hostContext, services) =>
            {
                //services.AddScoped<BancoDeDadosContext>();
                //services.AddDbContext<BancoDeDadosContext>(ServiceLifetime.Scoped);
                services.AddHostedService<Worker>();
            });
    }
}
