using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
//using Newtonsoft.Json;
using System.Web;

namespace ConsoleApp1
{

    class IcabSaidaCampos
    {
        [JsonIgnore]
        public int Sequencia { get; set; }
        public String TagDadoTipo { get; set; }
        public String Valor { get; set; }
        public IcabSaidaCampos(int seq, string tag, string val)
        {
            this.Sequencia = seq;
            this.TagDadoTipo = tag;
            this.Valor = val;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio...");
            Console.WriteLine("");
            try
            {

                TesteCargaListaItens();


                //var nums = new int[]{ 2, 6, 3, 4, 5, 6, 7, 8, 9 };
                ////var pares = from n in nums where (n % 2) == 0 select n;
                //var pares = nums.Where( n => (n % 2) == 0).ToList();
                //foreach (int item in pares)
                //    Console.WriteLine(item.ToString());

                //int[] numbers = { 2, 6, 3, 4, 5 };
                //var squaredNumbers = numbers.Select((x) => x * x);
                //Console.WriteLine(string.Join(" ", squaredNumbers));
                //var squaredNumbers2 = numbers.Where((x) => x == 4);
                //Console.WriteLine(string.Join(" ", squaredNumbers2));
                //var squaredNumbers3 = numbers.OrderBy((x) => x);
                //Console.WriteLine(string.Join(" ", squaredNumbers3));
                //foreach (int number in squaredNumbers3)
                //    Console.WriteLine(number);

            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("");
                Console.WriteLine("Fim...");
                Console.ReadKey();
            }
        }

        private static void TesteCargaListaItens()
        {

            var stopwatch1 = new Stopwatch();

            var lista = new List<IcabSaidaCampos>();
            for (int i = 1; i < 31; i++)
                lista.Add( new IcabSaidaCampos(seq: i, tag: $"Item{i.ToString()}", val: $"Val{i.ToString()}"));

            return;


            //stopwatch1.Reset();
            //stopwatch1.Start();
            // Console.WriteLine(JsonSerializer.Serialize(lista));
            //Console.WriteLine(JsonConvert.SerializeObject(lista, Formatting.None));
            //stopwatch1.Stop();
            //System.Console.WriteLine($"Teste 0 - Tempo:  {stopwatch1.Elapsed}");

            //stopwatch1.Reset();
            //stopwatch1.Start();

            //for (int i = 1; i < 10000001; i++)
            //{
            //    var json = "";
            //    foreach (var item in lista)
            //        json += $"\"{item.TagDadoTipo}\":\"{item.Valor}\"";
            //    //Console.Write(json);
            //}
            //stopwatch1.Stop();
            //System.Console.WriteLine($"Teste 0 - Tempo:  {stopwatch1.Elapsed}");

            //foreach (var item in lista) 
            //    Console.WriteLine(item.ToString());

            //var item1 = "";
            //var item2 = "";
            //var item3 = "";
            //var item4 = "";
            //var item5 = "";
            //var item6 = "";
            //var item7 = "";
            //var item8 = "";
            //var item9 = "";
            //var item10 = "";
            //var item11 = "";
            //var item12 = "";
            //var item13 = "";
            //var item14 = "";
            //var item15 = "";
            //var item16 = "";
            //var item17 = "";
            //var item18 = "";
            //var item19 = "";
            //var item20 = "";
            //var item21 = "";
            //var item22 = "";
            //var item23 = "";
            //var item24 = "";
            //var item25 = "";
            //var item26 = "";
            //var item27 = "";
            //var item28 = "";
            //var item29 = "";
            //var item30 = "";


            //stopwatch1.Reset();
            //stopwatch1.Start();
            //for (int i = 1; i < 10000001; i++)
            //{
            //    item1 = lista.FirstOrDefault(x => x.Equals("Item1"));
            //    item2 = lista.FirstOrDefault(x => x.Equals("Item2"));
            //    item3 = lista.FirstOrDefault(x => x.Equals("Item3"));
            //    item4 = lista.FirstOrDefault(x => x.Equals("Item4"));
            //    item5 = lista.FirstOrDefault(x => x.Equals("Item5"));
            //    item6 = lista.FirstOrDefault(x => x.Equals("Item6"));
            //    item7 = lista.FirstOrDefault(x => x.Equals("Item7"));
            //    item8 = lista.FirstOrDefault(x => x.Equals("Item8"));
            //    item9 = lista.FirstOrDefault(x => x.Equals("Item9"));
            //    item10 = lista.FirstOrDefault(x => x.Equals("Item10"));
            //    item11 = lista.FirstOrDefault(x => x.Equals("Item11"));
            //    item12 = lista.FirstOrDefault(x => x.Equals("Item12"));
            //    item13 = lista.FirstOrDefault(x => x.Equals("Item13"));
            //    item14 = lista.FirstOrDefault(x => x.Equals("Item14"));
            //    item15 = lista.FirstOrDefault(x => x.Equals("Item15"));
            //    item16 = lista.FirstOrDefault(x => x.Equals("Item16"));
            //    item17 = lista.FirstOrDefault(x => x.Equals("Item17"));
            //    item18 = lista.FirstOrDefault(x => x.Equals("Item18"));
            //    item19 = lista.FirstOrDefault(x => x.Equals("Item19"));
            //    item20 = lista.FirstOrDefault(x => x.Equals("Item20"));
            //    item21 = lista.FirstOrDefault(x => x.Equals("Item21"));
            //    item22 = lista.FirstOrDefault(x => x.Equals("Item22"));
            //    item23 = lista.FirstOrDefault(x => x.Equals("Item23"));
            //    item24 = lista.FirstOrDefault(x => x.Equals("Item24"));
            //    item25 = lista.FirstOrDefault(x => x.Equals("Item25"));
            //    item26 = lista.FirstOrDefault(x => x.Equals("Item26"));
            //    item27 = lista.FirstOrDefault(x => x.Equals("Item27"));
            //    item28 = lista.FirstOrDefault(x => x.Equals("Item28"));
            //    item29 = lista.FirstOrDefault(x => x.Equals("Item29"));
            //    item30 = lista.FirstOrDefault(x => x.Equals("Item30"));
            //}
            //stopwatch1.Stop();
            //System.Console.WriteLine($"Teste 1 - Tempo:  {stopwatch1.Elapsed}");

            //stopwatch1.Reset();
            //stopwatch1.Start();

            //for (int i = 1; i < 10000001; i++)
            //{
            //    foreach (var item in lista)
            //    {
            //        switch (item)
            //        {
            //            case "Item1":
            //                item1 = item;
            //                break;
            //            case "Item2":
            //                item2 = item;
            //                break;
            //            case "Item3":
            //                item3 = item;
            //                break;
            //            case "Item4":
            //                item4 = item;
            //                break;
            //            case "Item5":
            //                item5 = item;
            //                break;
            //            case "Item6":
            //                item6 = item;
            //                break;
            //            case "Item7":
            //                item7 = item;
            //                break;
            //            case "Item8":
            //                item8 = item;
            //                break;
            //            case "Item9":
            //                item9 = item;
            //                break;
            //            case "Item10":
            //                item10 = item;
            //                break;
            //            case "Item11":
            //                item11 = item;
            //                break;
            //            case "Item12":
            //                item12 = item;
            //                break;
            //            case "Item13":
            //                item13 = item;
            //                break;
            //            case "Item14":
            //                item14 = item;
            //                break;
            //            case "Item15":
            //                item15 = item;
            //                break;
            //            case "Item16":
            //                item16 = item;
            //                break;
            //            case "Item17":
            //                item17 = item;
            //                break;
            //            case "Item18":
            //                item18 = item;
            //                break;
            //            case "Item19":
            //                item19 = item;
            //                break;
            //            case "Item20":
            //                item20 = item;
            //                break;
            //            case "Item21":
            //                item21 = item;
            //                break;
            //            case "Item22":
            //                item22 = item;
            //                break;
            //            case "Item23":
            //                item23 = item;
            //                break;
            //            case "Item24":
            //                item24 = item;
            //                break;
            //            case "Item25":
            //                item25 = item;
            //                break;
            //            case "Item26":
            //                item26 = item;
            //                break;
            //            case "Item27":
            //                item27 = item;
            //                break;
            //            case "Item28":
            //                item28 = item;
            //                break;
            //            case "Item29":
            //                item29 = item;
            //                break;
            //            case "Item30":
            //                item30 = item;
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}

            //stopwatch1.Stop();
            //System.Console.WriteLine($"Teste 2 - Tempo:  {stopwatch1.Elapsed}");

            //stopwatch1.Reset();
            //stopwatch1.Start();

            //for (int i = 1; i < 10000001; i++)
            //{
            //    foreach (var item in lista)
            //    {
            //        if (item.Equals("Item1"))
            //            item1 = item;
            //        else if (item.Equals("Item2"))
            //            item2 = item;
            //        else if (item.Equals("Item3"))
            //            item3 = item;
            //        else if (item.Equals("Item4"))
            //            item4 = item;
            //        else if (item.Equals("Item5"))
            //            item5 = item;
            //        else if (item.Equals("Item6"))
            //            item6 = item;
            //        else if (item.Equals("Item7"))
            //            item7 = item;
            //        else if (item.Equals("Item8"))
            //            item8 = item;
            //        else if (item.Equals("Item9"))
            //            item9 = item;
            //        else if (item.Equals("Item10"))
            //            item10 = item;
            //        else if (item.Equals("Item11"))
            //            item11 = item;
            //        else if (item.Equals("Item12"))
            //            item12 = item;
            //        else if (item.Equals("Item13"))
            //            item13 = item;
            //        else if (item.Equals("Item14"))
            //            item14 = item;
            //        else if (item.Equals("Item15"))
            //            item15 = item;
            //        else if (item.Equals("Item16"))
            //            item16 = item;
            //        else if (item.Equals("Item17"))
            //            item17 = item;
            //        else if (item.Equals("Item18"))
            //            item18 = item;
            //        else if (item.Equals("Item19"))
            //            item19 = item;
            //        else if (item.Equals("Item20"))
            //            item20 = item;
            //        else if (item.Equals("Item21"))
            //            item21 = item;
            //        else if (item.Equals("Item22"))
            //            item22 = item;
            //        else if (item.Equals("Item23"))
            //            item23 = item;
            //        else if (item.Equals("Item24"))
            //            item24 = item;
            //        else if (item.Equals("Item25"))
            //            item25 = item;
            //        else if (item.Equals("Item26"))
            //            item26 = item;
            //        else if (item.Equals("Item27"))
            //            item27 = item;
            //        else if (item.Equals("Item28"))
            //            item28 = item;
            //        else if (item.Equals("Item29"))
            //            item29 = item;
            //        else if (item.Equals("Item30"))
            //            item30 = item;
            //    }
            //}

            //stopwatch1.Stop();
            //System.Console.WriteLine($"Teste 3 - Tempo:  {stopwatch1.Elapsed}");
        }


    }
}
