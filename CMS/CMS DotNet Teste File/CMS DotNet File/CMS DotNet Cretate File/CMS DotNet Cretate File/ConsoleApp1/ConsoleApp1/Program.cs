using FileHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            #region CMS Arquivos


            //string path = @"D:\CMS Projeto Local\CMS Teste XML Performace\Win32\Debug\XMLTeste02.xml";

            //var stopwatch = new Stopwatch();


            //---------------------------------------------------------------------------------- 
            //---------------------------------------------------------------------------------- 

            //stopwatch.Reset();
            //stopwatch.Start();

            //using (StreamWriter outputFile = new StreamWriter(path)) //"D:\CMS Projetos DotNet\CMS DotNet Cretate File\WriteLines.txt"
            //{
            //    outputFile.Write(@"<?xml version='1.0'?>");
            //    outputFile.Write(@"<?xml version='1.0'?>");
            //    outputFile.Write("<pessoas>");
            //    outputFile.Write("<saldoIni>Pão simples</saldoIni>");
            //    for (int i = 1; i <= 1000000; i++) // 10000000 // 1000000 // 100000 // 10000
            //    {
            //       // var valor = i.ToString(".0", CultureInfo.InvariantCulture);
            //        // String.Format(CultureInfo.InvariantCulture,"{0:0,0}", value)
            //        // String.Format(CultureInfo.InvariantCulture,"{0:0,0}", value)
            //        outputFile.Write("<pessoa>");
            //        outputFile.Write($"<nome>Pessoa {i}</nome>");
            //        outputFile.Write($"<idade>Idade {i}</idade>");
            //        outputFile.Write("</pessoa>");
            //    }
            //    outputFile.Write("<saldoFim>Pão simples</saldoFim>");
            //    outputFile.Write("</pessoas>");
            //}

            //stopwatch.Stop();
            //Console.WriteLine("Tempo de Geracao:  " + stopwatch.Elapsed);

            //---------------------------------------------------------------------------------- 
            //---------------------------------------------------------------------------------- 

            //stopwatch.Reset();
            //stopwatch.Start();

            //string line = "";
            //using (StreamReader sr = new StreamReader(path))
            //{
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //      //  Console.WriteLine(line);
            //    }
            //}

            //stopwatch.Stop();
            //Console.WriteLine("Tempo de Leitura:  " + stopwatch.Elapsed);

            //---------------------------------------------------------------------------------- 
            //---------------------------------------------------------------------------------- 

            //stopwatch.Reset();
            //stopwatch.Start();

            //using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            //using var sr2 = new StreamReader(fs, Encoding.UTF8);
            //string content = String.Empty; // sr2.ReadToEnd();
            //while ((content = sr2.ReadLine()) != null)
            //{
            //    //  Console.WriteLine(line);
            //}

            //stopwatch.Stop();
            //Console.WriteLine("Tempo de Leitura:  " + stopwatch.Elapsed);

            //---------------------------------------------------------------------------------- 
            //---------------------------------------------------------------------------------- 

            #endregion

            #region CMS Try Catch
            try
            {

                var num1 = 10;
                var num2 = 20;
                var num3 = 30;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
            #endregion

            #region CMS Linq

            var Itens = new List<ItemVenda>();
            Itens.Add(new ItemVenda(1, 10));
            Itens.Add(new ItemVenda(2, 5));
            //Itens.Add(null);

            //var total = Itens.Sum((i) => { if (i == null) return 0.00; return i.Preco * i.Quantidade; });
            // var NovoItens = Itens.DefaultIfEmpty().Sum((i) => i.Preco * i.Quantidade);
            // var total = NovoItens.Sum((i) => i.Preco * i.Quantidade );

            //Console.WriteLine($"Total: {total}");
            var student2 = new ItemVenda
            {
                Quantidade = 1,
                Preco = 10,
            };
            //---------------------------------------------------------------------------------- 
            //---------------------------------------------------------------------------------- 
            #endregion

            #region MyRegion

            #region MyRegion1

            #endregion

            #region MyRegion1

            #endregion

            #endregion

            Console.WriteLine("Fim");
            Console.ReadKey();
        }
    }

    class ItemVenda
    {
        public int Quantidade { get; set; } = 0;
        public double Preco { get; set; } = 0.00;

        public ItemVenda(int quantidade = 0, double preco = 0.00)
        {
            this.Quantidade = quantidade;
            this.Preco      = preco;
        }
    }
}
