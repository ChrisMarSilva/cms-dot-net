using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            //string path = @"D:\CMS Projeto Local\CMS Teste XML Performace\Win32\Debug\XMLTeste02.xml";
            string path = @"C:\Users\cmartins\Desktop\XMLTeste02.xml";

         var stopwatch = new Stopwatch();


            //---------------------------------------------------------------------------------- 
            //---------------------------------------------------------------------------------- 

            stopwatch.Reset();
            stopwatch.Start();

            using (StreamWriter outputFile = new StreamWriter(path)) //"D:\CMS Projetos DotNet\CMS DotNet Cretate File\WriteLines.txt"
            {
                outputFile.Write(@"<?xml version='1.0'?>");
                outputFile.Write(@"<?xml version='1.0'?>");
                outputFile.Write("<pessoas>");
                outputFile.Write("<saldoIni>Pão simples</saldoIni>");
                for (int i = 1; i <= 10000; i++) // 10000000 // 1000000 // 100000 // 10000
                {
                    // var valor = i.ToString(".0", CultureInfo.InvariantCulture);
                    // String.Format(CultureInfo.InvariantCulture,"{0:0,0}", value)
                    // String.Format(CultureInfo.InvariantCulture,"{0:0,0}", value)
                    outputFile.Write("<Grupo_STR0014R1_Lanc>");
                    outputFile.Write($"<CodMsgOr>STR0008R1</CodMsgOr>");
                    outputFile.Write($"<NumCtrlIF_LDLOr></NumCtrlIF_LDLOr>");
                    outputFile.Write($"<ISPBCtrapart>00000000</ISPBCtrapart>");
                    outputFile.Write($"<NumCtrlSTROr>{i}</NumCtrlSTROr>");
                    outputFile.Write($"<DtHrSit>00000000</DtHrSit>");
                    outputFile.Write($"<TpDeb_Cred>D</TpDeb_Cred>");
                    outputFile.Write($"<VlrLanc>1</VlrLanc>");
                    outputFile.Write("</Grupo_STR0014R1_Lanc>");
                }
                outputFile.Write("<saldoFim>Pão simples</saldoFim>");
                outputFile.Write("</pessoas>");
            }

            stopwatch.Stop();
            Console.WriteLine("Tempo de Geracao:  " + stopwatch.Elapsed);

            //---------------------------------------------------------------------------------- 
            //---------------------------------------------------------------------------------- 

            stopwatch.Reset();
            stopwatch.Start();

            string line = "";
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    //  Console.WriteLine(line);
                }
            }

            stopwatch.Stop();
            Console.WriteLine("Tempo de Leitura:  " + stopwatch.Elapsed);

            //---------------------------------------------------------------------------------- 
            //---------------------------------------------------------------------------------- 

            stopwatch.Reset();
            stopwatch.Start();

            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var sr2 = new StreamReader(fs, Encoding.UTF8);
            string content = String.Empty; // sr2.ReadToEnd();
            while ((content = sr2.ReadLine()) != null)
            {
                //  Console.WriteLine(line);
            }

            stopwatch.Stop();
            Console.WriteLine("Tempo de Leitura:  " + stopwatch.Elapsed);

            //---------------------------------------------------------------------------------- 
            //---------------------------------------------------------------------------------- 

            stopwatch.Reset();
            stopwatch.Start();



            stopwatch.Stop();
            Console.WriteLine("Tempo de Leitura:  " + stopwatch.Elapsed);


            //---------------------------------------------------------------------------------- 
            //---------------------------------------------------------------------------------- 

            Console.WriteLine("Fim");
            Console.ReadKey();
        }
    }
}
