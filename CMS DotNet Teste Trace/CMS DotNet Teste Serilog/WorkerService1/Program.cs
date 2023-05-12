using Serilog;
using WorkerService1;


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Information() // Verbose
    //.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithAssemblyName()
    .Enrich.WithAssemblyVersion()
    .Enrich.WithUserName() // Serilog.Enrichers.Context
    .Enrich.WithMemoryUsage()
    .Enrich.WithProcessName()
    .Enrich.WithEnrichedProperties()
    //.Enrich.WithFunction("f1", () => { return DateTime.Now.Ticks.ToString(); })
    .Enrich.WithProperty("Application", "JDSPB")
    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
    .ReadFrom.Configuration(configuration)
    //.WriteTo.Console()
    //.WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day)
    //.WriteTo.Async(a =>
    //{
    //    a.MSSqlServer(configuration.GetConnectionString("JDConexao"), "TBJKB_LOG", period: TimeSpan.FromMilliseconds(500), columnOptions: options);
    //    a.ColoredConsole(LogEventLevel.Verbose, "{NewLine}{Timestamp:HH:mm:ss.fff} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception} {Properties:j}");
    //    a.File(new CompactJsonFormatter(), ".\\logs\\log_.txt", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 1024 * 1024 * 20, buffered: true, flushToDiskInterval: TimeSpan.FromSeconds(5), rollOnFileSizeLimit: true);
    //}, bufferSize: 500)
    //  .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff zzz} [{Level:u3}] {Message:l}{NewLine}{Exception}")  //                //.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] ({SourceContext}) ({ThreadId}): {Message:lj}{NewLine}{Exception}") //{SourceContext}  
    //  .WriteTo.File(@"D:\CMSWorkerServiceLogs-.txt", shared: true, rollOnFileSizeLimit: true, fileSizeLimitBytes: 1234567,                rollingInterval: RollingInterval.Day,                 outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} || {Level:u3} || {UserName} || {AssemblyName} || {AssemblyVersion} || {MemoryUsage} || {Message} || {NewLine}")
    //.WriteTo.RollingFileAlternate(".\\logs")
    // .WriteTo.RollingFile(@"D:\CMSWorkerServiceLogs-{HalfHour}.txt", shared: true, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
    //.WriteTo.RollingFile( Configuration.GetValue<string>("LogFilePath") + "-{Date}.txt", LogEventLevel.Information)
    .CreateLogger(); // .CreateBootstrapLogger();

try
{
    // Log.Information("Starting the Service");


    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        //.ConfigureAppConfiguration((builderContext, config) =>
        //{
        //    //builderContext.Logging.ClearProviders();
        //    //builderContext.Logging.AddSerilog(Log.Logger);
        //    //config
        //    //    .SetBasePath(Directory.GetCurrentDirectory()) // System.AppDomain.CurrentDomain.BaseDirectory
        //    //    .AddJsonFile("appsettings.json", false, true)
        //    //    .AddJsonFile($"appsettings.{builderContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        //    //    .AddJsonFile($"appsettings.{Environment.MachineName}.json", true, true)
        //    //    .AddCommandLine(args)
        //    //    .Build();
        //})
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
            // services.AddLogging(configure => configure.AddSerilog());
        })
        .UseSerilog()
        //.UseSerilog((context, services, config) =>
        //{
        //    config
        //        .ReadFrom
        //        .Configuration(context.Configuration)
        //        .ReadFrom.Services(services)
        //        .Enrich.FromLogContext();
        //    config.WriteTo.Console();
        //})
        .Build();

    await host.RunAsync();

}
catch (Exception ex)
{
    Log.Fatal(ex, "There was a problem starting the service");
}
finally
{
    Log.Information("Service successfully stopped");

    Log.CloseAndFlush();
}

