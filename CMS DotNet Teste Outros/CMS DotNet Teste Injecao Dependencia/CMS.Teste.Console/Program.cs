using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;


namespace CMS.Teste.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            System.Console.WriteLine("Inicio");
            try
            {

                Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Debug()
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                  .Enrich.FromLogContext()
                  //.WriteTo.Stackify()
                  .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {NewLine}{Exception}")
                  //.AddJsonFile("appsettings.json")
                  //.AddEventLog()
                  //.WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                  .CreateLogger();
                try
                {

                    //configuration.GetConnectionString("Database")
                    //var con = _context.Database.GetDbConnection();

                    Serilog.Debugging.SelfLog.Enable(msg => { Debug.Print(msg); Debug.WriteLine(msg); Debugger.Break(); });
                    Serilog.Debugging.SelfLog.Enable(System.Console.Error);

                    var service = new ServiceCollection()
                        .AddLogging(configure => { configure.ClearProviders(); configure.AddSerilog(); }) // configure.AddConsole();
                                                                                                          //.UseSerilog()
                        .AddTransient<Mensagem>()
                        .BuildServiceProvider();

                    //System.Console.WriteLine("Log in Console");
                    Log.Debug("Log in Debugs");
                    Log.Information("Log in Informations");
                    Log.Warning("Log in Warning");
                    Log.Error("Log in Error");
                    Log.Fatal("Log in Fatal");
                    Log.Verbose("Log in Verbose");

                    //var msg = service.GetService<Mensagem>();
                    //msg.Processar();
                    //var msg2 = new Mensagem();
                    //msg2.Processar();

                    //#if DEBUG
                    //                    Debug.WriteLine(value);
                    //#else
                    //                Console.WriteLine(value);
                    //#endif

                    // var StringDbConn = @"Data Source=JDSP108;Initial Catalog=CMS_DOTNET;User ID=jddesenv;Password=jddesenv;Persist Security Info=True;";
                    var StringDbConn = @"Data Source=CMS-NOTE\SQLEXPRESS;Initial Catalog=CMS_DOTNET;User ID=sa;Password=sa;Persist Security Info=True;";

                    using (var connection = new SqlConnection(StringDbConn))
                    {

                        connection.Open();
                        Log.Information("ServerVersion: {0}", connection.ServerVersion);
                        Log.Information("State: {0}", connection.State);
                        //  Database.Log = comando => System.Diagnostics.Debug.WriteLine(comando);
                        Log.Information("01 - Conectado");

                        using (SqlCommand commandDel = new SqlCommand("TRUNCATE TABLE TBEMPRESA", connection))
                        {
                            commandDel.ExecuteNonQuery();
                            commandDel.Dispose();
                            Log.Information("02 - Comando TRUNCATE executado");
                        }

                        using (var transaction = connection.BeginTransaction())
                        {
                            Log.Information("03 - Inicio Transaction");
                            try
                            {

                                using (SqlCommand commandIns = new SqlCommand("SELECT * FROM TBE MPRESA", connection, transaction))
                                {
                                    commandIns.ExecuteNonQuery();
                                    commandIns.Dispose();
                                    Log.Information("04 - Comando SELECT executado");
                                }

                                transaction.Commit();
                                Log.Information("05 - Fim Transaction Ok");
                            }
                            catch (SqlException e)
                            {
                                transaction.Rollback();

                                Log.Error($"06 - Fim Transaction Erro(SqlException)");
                                for (int i = 0; i < e.Errors.Count; i++)
                                {
                                    Log.Error($"06 - Index: #{i.ToString()}");
                                    Log.Error($"06 - Number: {e.Errors[i].Number.ToString()}");
                                    Log.Error($"06 - Server: {e.Errors[i].Server}");
                                    Log.Error($"06 - State: {e.Errors[i].State}");
                                    Log.Error($"06 - Class: {e.Errors[i].Class}");
                                    Log.Error($"06 - Message: {e.Errors[i].Message}");
                                    Log.Error($"06 - LineNumber: {e.Errors[i].LineNumber.ToString()}");
                                    Log.Error($"06 - Source: {e.Errors[i].Source}");
                                    Log.Error($"06 - Procedure: {e.Errors[i].Procedure}");
                                }

                                Log.Error($"06 - Fim Transaction Erro(SqlException)");
                                for (int i = 0; i < e.Errors.Count; i++)
                                {
                                    Log.Error($"06 - Index: #{i.ToString()} - {e.Errors[i].ToString()}");
                                }

                            }
                            catch (Exception e)
                            {
                                transaction.Rollback();
                                Log.Error($"06 - Fim Transaction Erro(Exception): {e.Message}");
                                throw;
                            }
                        }

                        connection.Close();
                        connection.Dispose();
                    }

                    //    catch (Exception ex)
                    //{
                    //    if (ex is SqlException)
                    //    {
                    //        // Handle more specific SqlException exception here.  
                    //    }
                    //    else
                    //    {
                    //        // Handle generic ones here.  
                    //    }
                    //}


                }
                catch (Exception e)
                {
                    Log.Error($"07 - Erro: {e.Message}");
                }
                finally
                {
                    Log.CloseAndFlush();
                }

            }
            finally
            {
                System.Console.WriteLine("Fim");
                System.Console.ReadKey();
            }
        }


    }
}





/*
 
     
		
	

JDTraceApp.LogInicio(out paramDtHr, paramMethod, "01");
            try
            {
                JDTraceApp.LogInfo(paramDtHr, paramMethod, "02");
                throw new Exception("teste erro");
            }
            catch (Exception ex)
            {
                JDTraceApp.LogErro(paramDtHr, paramMethod, "03");
            }
            finally
            {
                JDTraceApp.LogFim(paramDtHr, paramMethod, "04");
            }



using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;


namespace CMS.Thread.Trace.Console
{
    public static class JDTraceApp
    {

        //Task.Run(() => { });
        //new Task(() => { }).Start();
        //Task.Factory.StartNew(() => { });
        //System.Threading.Thread th = new System.Threading.Thread(() => { }).Start();

        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim(); //ReaderWriterLock
        private static readonly object _locker = new object();

        private static void Log(DateTime dthr, string method, string tipo, string texto, string param)
        {
            try
            {
                //Task.Factory.StartNew(() =>
                //{
                //    lock (_locker)
                //    {

                DateTime dthrFim = DateTime.Now;
                TimeSpan dthrDif = dthrFim.Subtract(dthr); //dthrFim - dthr; dthrDif.ToString("c")
                long memoryUsed = Process.GetCurrentProcess().PrivateMemorySize64;
                string linha = $"{dthrFim.ToString("yyyy.MM.dd HH:mm:ss.fffff")}|{dthrDif}|{method}|{tipo}|{memoryUsed}|{texto}|{param}";
                System.Console.WriteLine(linha);

                _readWriteLock.EnterWriteLock();  // _readWriteLock.AcquireWriterLock(int.MaxValue);
                try
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");  // System.AppDomain.CurrentDomain.BaseDirectory;  
                    using (FileStream file = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                    {
                        using (StreamWriter writer = new StreamWriter(file, Encoding.UTF8)) //Unicode
                        {
                            writer.WriteLine(linha);
                            writer.Close();
                        }
                    }
                }
                finally
                {
                    _readWriteLock.ExitWriteLock();  // _readWriteLock.ReleaseWriterLock();
                }

                //}
                //    });


            }
            catch (Exception ex)
            {
            }
        }

        public static void LogInicio(out DateTime dthr, string method, string texto, string param = "")
        {
            dthr = DateTime.Now;
            JDTraceApp.Log(dthr, method, "INI", texto, param);
        }

        public static void LogInfo(DateTime dthr, string method, string texto, string param = "")
        {
            JDTraceApp.Log(dthr, method, "LOG", texto, param);
        }

        public static void LogErro(DateTime dthr, string method, string texto, string param = "")
        {
            JDTraceApp.Log(dthr, method, "ERR", texto, param);
        }

        public static void LogFim(DateTime dthr, string method, string texto, string param = "")
        {
            JDTraceApp.Log(dthr, method, "FIM", texto, param);
        }

    }

}





     */
