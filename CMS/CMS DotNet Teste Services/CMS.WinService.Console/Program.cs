using System;
using System.ServiceProcess;
using System.Threading;
using Serilog;
using Serilog.Events;

namespace CMS.WinService.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            Thread.CurrentThread.CurrentCulture   = new System.Globalization.CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");

            #if DEBUG
                var ServicesToRun = new FileWriteService();
                ServicesToRun.onDebug();
            #else
                using (var service = new FileWriteService()) ServiceBase.Run(service); }
            #endif

            //_logger = new LoggerConfiguration().WriteTo.File($"logs/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}.txt").CreateLogger();
            //Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().MinimumLevel.Override("Microsoft", LogEventLevel.Information).Enrich.FromLogContext().WriteTo.Console().CreateLogger();
            // Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Console().CreateLogger();

            //try
            //{
            //    Log.Information("=====================================================================");
            //    Log.Information("TEXTO Information");
            //    Log.Warning("TEXTO Warning");
            //    Log.Fatal("TEXTO Fatal");
            //    Log.Information("=====================================================================");
            //    return;
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "Host terminated unexpectedly");
            //    return;
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}

            //JDTraceApp.LogInicio("==> INICIO");
            //try
            //{
            //    JDTraceApp.LogInfo("Hello World!");
            //    return;
            //}
            //catch (Exception ex)
            //{
            //    JDTraceApp.LogErro($"==> ERRO: {ex.Message}");
            //}
            //finally
            //{
            //    JDTraceApp.LogFim("==> FIM"); 
            //    System.Console.ReadKey();
            //}
        }
    }
}
