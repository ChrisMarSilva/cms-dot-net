using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                System.DateTime dataInicio = DateTime.Now;
                System.DateTime dataFim = new DateTime(2023, 2, 3);
                var iTotal = 1_000_000; // 1_000 // 10_000 // 10_000 // 100_000 // 1_000_000
                var diasUteis = 0;

                stopwatch.Start();
                for (int i = 0; i < iTotal; i++)
                {
                    diasUteis = CalcularDiasUteis1(dataInicio, dataFim);
                }
                stopwatch.Stop();
                Console.WriteLine($"CalcularDiasUteis1( {diasUteis} dias utei ): " + stopwatch.ElapsedMilliseconds + "ms");

                stopwatch.Start();
                for (int i = 0; i < iTotal; i++)
                {
                    diasUteis = CalcularDiasUteis2(dataInicio, dataFim);
                }
                stopwatch.Stop();
                Console.WriteLine($"CalcularDiasUteis2( {diasUteis} dias utei ): " + stopwatch.ElapsedMilliseconds + "ms");

                stopwatch.Start();
                for (int i = 0; i < iTotal; i++)
                {
                    diasUteis = CalcularDiasUteis3(dataInicio, dataFim);
                }
                stopwatch.Stop();
                Console.WriteLine($"CalcularDiasUteis3( {diasUteis} dias utei ): " + stopwatch.ElapsedMilliseconds + "ms");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Falha geral: {e}");
            }
            finally
            {
                Console.ReadKey();
            }
        }

        public static int CalcularDiasUteis1(System.DateTime dataInicio, System.DateTime dataFim)
        {
            int diasUteis = 0;
            for (var data = dataInicio; data <= dataFim; data = data.AddDays(1))
            {
                if (data.DayOfWeek != DayOfWeek.Saturday && data.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasUteis++;
                }
            }
            return diasUteis;
        }

        public static int CalcularDiasUteis2(DateTime dataInicio, DateTime dataFim)
        {
            int diasUteis = 0;
            while (dataInicio <= dataFim)
            {
                if (dataInicio.DayOfWeek != DayOfWeek.Saturday && dataInicio.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasUteis++;
                }
                dataInicio = dataInicio.AddDays(1);
            }
            return diasUteis;
        }

        public static int CalcularDiasUteis3(System.DateTime dataInicio, System.DateTime dataFim)
        {
            int diasUteis = 0;
            TimeSpan intervalo = dataFim - dataInicio;
            int diasTotais = intervalo.Days;
            for (int i = 0; i <= diasTotais; i++)
            {
                System.DateTime diaAtual = dataInicio.AddDays(i);
                if (diaAtual.DayOfWeek != DayOfWeek.Saturday && diaAtual.DayOfWeek != DayOfWeek.Sunday)
                    diasUteis++;
            }
            return diasUteis;
        }

    }
}
