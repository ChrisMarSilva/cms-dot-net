using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlClient;
using System.Diagnostics;

// @nuget: EntityFramework
// @nuget: Z.EntityFramework.Extensions
// Website: https://entityframework-extensions.net/

namespace CMS.EF.Performance.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Inicio");
            try
            {
                var qtdeTestes = 4;
                var totalRegistros= 10000;
                var listaEmpresas = GerarEmpresas(totalRegistros);

                TesteSaveChanges(listaEmpresas, qtdeTestes);
                TesteBulkSaveChanges(listaEmpresas, qtdeTestes);
                TesteBatchSaveChanges(listaEmpresas, qtdeTestes);
                TesteBulkInsert(listaEmpresas, qtdeTestes, 1);
                TesteBulkInsert(listaEmpresas, qtdeTestes, 2);
                TesteBulkInsert(listaEmpresas, qtdeTestes, 3); 
                TesteBulkInsert(listaEmpresas, qtdeTestes, 4); // Melhor Total de Tempo / Melhor Media de Tempo
                TesteBulkInsert(listaEmpresas, qtdeTestes, 5); //  Ideal
                TesteSqlConnection(listaEmpresas, qtdeTestes);

                //using (var context = new BancoDeDadosContext())
                //{
                //    stopwatch.Reset();
                //    stopwatch.Start();
                //    var listaEmpresa = context.Empresas.ToList();
                //    foreach (Empresa empr in listaEmpresa)
                //    {
                //        System.Console.WriteLine($" => {empr.Id.ToString()} - {empr.Nome}");
                //    }
                //    stopwatch.Stop();
                //    System.Console.WriteLine("");
                //    System.Console.WriteLine($"ToList - Tempo: {stopwatch.Elapsed}");
                //}

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("Fim");
                System.Console.ReadKey();
            }
        }
        
        public static void TesteSaveChanges(List<Empresa> empresas, int qtdeTestes)
        {
            System.Console.WriteLine("");
            using (var context = new BancoDeDadosContext())
            {
                //var _transaction = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE TBEMPRESA");
                //_transaction.Commit(); // transaction.Rollback();
                var stopwatch = new Stopwatch();
                var total = new TimeSpan();
                for (int i = 0; i < qtdeTestes; i++)
                {
                    context.Empresas.AddRange(empresas);
                    stopwatch.Reset();
                    stopwatch.Start();
                    context.SaveChanges();
                    stopwatch.Stop();
                    total += stopwatch.Elapsed;
                    System.Console.WriteLine($"{stopwatch.Elapsed} => 0{i+1} SaveChanges");
                }
                System.Console.WriteLine($"{String.Format("{0:00}:{1:00}:{2:00}.{3:00}", total.Hours, total.Minutes, total.Seconds, total.Milliseconds)} => Total SaveChanges");
                System.Console.WriteLine($"{(new TimeSpan(total.Ticks / 4)).ToString()} => Media SaveChanges");
            }
        }

        public static void TesteBulkSaveChanges(List<Empresa> empresas, int qtdeTestes)
        {
            System.Console.WriteLine("");
            using (var context = new BancoDeDadosContext())
            {
                //var _transaction = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE TBEMPRESA");
                //_transaction.Commit(); // transaction.Rollback();
                var stopwatch = new Stopwatch();
                var total = new TimeSpan();
                for (int i = 0; i < qtdeTestes; i++)
                {
                    context.Empresas.AddRange(empresas);
                    stopwatch.Reset();
                    stopwatch.Start();
                    context.BulkSaveChanges();
                    //  //context.BulkSaveChanges(bulk => bulk.BatchSize = 1000);
                    stopwatch.Stop();
                    total += stopwatch.Elapsed;
                    System.Console.WriteLine($"{stopwatch.Elapsed} => 0{i + 1} BulkSaveChanges");
                }
                System.Console.WriteLine($"{String.Format("{0:00}:{1:00}:{2:00}.{3:00}", total.Hours, total.Minutes, total.Seconds, total.Milliseconds)} => Total BulkSaveChanges");
                System.Console.WriteLine($"{(new TimeSpan(total.Ticks / 4)).ToString()} => Media BulkSaveChanges");
            }
        }

        public static void TesteBatchSaveChanges(List<Empresa> empresas, int qtdeTestes)
        {
            System.Console.WriteLine("");
            using (var context = new BancoDeDadosContext())
            {
                //var _transaction = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE TBEMPRESA");
                //_transaction.Commit(); // transaction.Rollback();
                var stopwatch = new Stopwatch();
                var total = new TimeSpan();
                for (int i = 0; i < qtdeTestes; i++)
                {
                    context.Empresas.AddRange(empresas);
                    stopwatch.Reset();
                    stopwatch.Start();
                    context.BatchSaveChanges();
                    //context.BatchSaveChanges(bulk => bulk.BatchSize = 1000);
                    stopwatch.Stop();
                    total += stopwatch.Elapsed;
                    System.Console.WriteLine($"{stopwatch.Elapsed} => 0{i + 1} BatchSaveChanges");
                }
                System.Console.WriteLine($"{String.Format("{0:00}:{1:00}:{2:00}.{3:00}", total.Hours, total.Minutes, total.Seconds, total.Milliseconds)} => Total BatchSaveChanges");
                System.Console.WriteLine($"{(new TimeSpan(total.Ticks / 4)).ToString()} => Media BatchSaveChanges");
            }
        }
        
        public static void TesteBulkInsert(List<Empresa> empresas, int qtdeTestes, int tipoTeste)
        {
            System.Console.WriteLine("");
            using (var context = new BancoDeDadosContext())
            {
                //var _transaction = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE TBEMPRESA");
                //_transaction.Commit(); // transaction.Rollback();
                var stopwatch = new Stopwatch();
                var total = new TimeSpan();
                for (int i = 0; i < qtdeTestes; i++)
                {
                    stopwatch.Reset();
                    stopwatch.Start();
                    if (tipoTeste == 1) context.BulkInsert(empresas);
                    if (tipoTeste == 2) context.BulkInsert(empresas, options => options.IncludeGraph = true);
                    if (tipoTeste == 3) context.BulkInsert(empresas, options => options.BatchSize = 1000);
                    if (tipoTeste == 4) context.BulkInsert(empresas, options => options.AutoMapOutputDirection = false);
                    if (tipoTeste == 5) context.BulkInsert(empresas, options => { options.AutoMapOutputDirection = false; options.BatchSize = 1000; });
                    stopwatch.Stop();
                    total += stopwatch.Elapsed;
                    System.Console.WriteLine($"{stopwatch.Elapsed} => 0{i + 1} BulkInsert (TipoTeste:{tipoTeste})");
                }
                System.Console.WriteLine($"{String.Format("{0:00}:{1:00}:{2:00}.{3:00}", total.Hours, total.Minutes, total.Seconds, total.Milliseconds)} => Total BulkInsert (TipoTeste:{tipoTeste})");
                System.Console.WriteLine($"{(new TimeSpan(total.Ticks / 4)).ToString()} => Media BulkInsert (TipoTeste:{tipoTeste})");
            }
        }

        public static void TesteSqlConnection(List<Empresa> empresas, int qtdeTestes)
        {
            System.Console.WriteLine("");
            var DbConnJD = @"Data Source=JDSP108;Initial Catalog=CMS_DOTNET;User ID=jddesenv;Password=jddesenv;Persist Security Info=True;";
           // var DbConnNote = @"Data Source=CMS-NOTE\SQLEXPRESS;Initial Catalog=CMS_DOTNET;User ID=sa;Password=sa;Persist Security Info=True;";
            using (var connection = new SqlConnection(DbConnJD))
            {
                connection.Open();

                SqlCommand commandDel = new SqlCommand("TRUNCATE TABLE TBEMPRESA", connection);
                commandDel.ExecuteNonQuery();

                var stopwatch = new Stopwatch();
                var total = new TimeSpan();
                for (int i = 0; i < qtdeTestes; i++)
                {
                    stopwatch.Reset();
                    stopwatch.Start();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            SqlCommand commandIns = new SqlCommand("INSERT INTO TbEmpresa (Nome) VALUES (@Nome)", connection, transaction);
                            //commandIns.Parameters.Add("@Nome", DbType.String);
                            foreach (var item in empresas)
                            {
                                commandIns.Parameters[0].Value = item.Nome;
                                commandIns.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback(); throw;
                        }
                    }
                    stopwatch.Stop();
                    total += stopwatch.Elapsed;
                    System.Console.WriteLine($"{stopwatch.Elapsed} => 0{i + 1} SqlConnection");
                }
                System.Console.WriteLine($"{String.Format("{0:00}:{1:00}:{2:00}.{3:00}", total.Hours, total.Minutes, total.Seconds, total.Milliseconds)} => Total SqlConnection");
                System.Console.WriteLine($"{(new TimeSpan(total.Ticks / 4)).ToString()} => Media SqlConnection");
            }
        }

        public static List<Empresa> GerarEmpresas(int count)
        {
            var list = new List<Empresa>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new Empresa() { Nome = "Customer_" + i });
            }
            return list;
        }

        public class Empresa
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }

        public class EmpresaMapping : EntityTypeConfiguration<Empresa>
        {
            public EmpresaMapping()
            {
                ToTable("TbEmpresa");
                HasKey(v => v.Id);
                Property(v => v.Nome).IsRequired().HasMaxLength(100);
            }
        }

        public class BancoDeDadosContext : DbContext
        {

            public DbSet<Empresa> Empresas { get; set; }

            public BancoDeDadosContext() : base("Name=DbConnJD") // DbConnNote // DbConnJD
            {
                Configuration.LazyLoadingEnabled = false;
                Configuration.ProxyCreationEnabled = false;
                Configuration.AutoDetectChangesEnabled = false;
                Configuration.ValidateOnSaveEnabled = false;
                //Configuration.BatchSaveChanges.UseBatchForSaveChanges = true;
                // Database.Log = comando => System.Diagnostics.Debug.WriteLine(comando);
            }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Configurations.Add<Empresa>(new EmpresaMapping());
                base.OnModelCreating(modelBuilder);
            }

        }
    }
}
