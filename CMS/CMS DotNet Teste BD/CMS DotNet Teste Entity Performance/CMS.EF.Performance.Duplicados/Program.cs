using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.EF.Performance.Duplicados
{
    class Program
    {
        static void Main(string[] args)
        {



//            var distinctList = Itens.DistinctBy(x => x.UmaPropriedadeQualquer).ToList();

//            var repetidos = Itens
//                             .GroupBy(x => x.UmaPropriedadeQualquer)
//                             .Where(g => g.Count() > 1)
//                             .Select(g => g.Key)
//                             .ToList();




//            var dicionarioDeArrays = lista
//                .GroupBy(x => x)
//                .ToDictionary(x => x.Key, x => x.ToArray());

//            var listaDeDuplicatasAgrupadasEmArrays = dicionarioDeArrays
//                .Where(x => x.Value.Length > 1)
//                .Select(x => x.Value)
//                .ToList();

//            int[] array1 = { 2, 5, 8, 10, 2, 23, 2, 4, 5, 8, 8 };

//            var arrayAgrupadosRepeticaoCount = array1
//                         .GroupBy(x => x)
//                         .Select(a => new
//                         {
//                             Item = a.Key,
//                             Quant = a.Count()
//                         })
//                         .ToArray();

//            string[] myStrings = // ...

//if (myStrings.Distinct().Count() != myStrings.Count())
//            {
//                // Show message
//            }

//            string[] array = { "First", "Second", "Third", "First", "Third" };
//            var i = CheckforDuplicates(array);
//            public bool CheckforDuplicates(string[] array)
//            {
//                var duplicates = array
//                 .GroupBy(p => p)
//                 .Where(g => g.Count() > 1)
//                 .Select(g => g.Key);
//                return (duplicates.Count() > 0);
//            }


//            List<Store> myStoreList = Service.GetStoreListbyProvince(provinceId)
//                                                             .GroupBy(s => s.City)
//                                                             .Select(grp => grp.FirstOrDefault())
//                                                             .OrderBy(s => s.City)
//                                                             .ToList();




            #region MyRegion03

            // List<string> list = new List<string>();// { "a", "a", "b", "b", "r", "t" };
            // list.Add("1");
            // foreach (int value in list) Console.WriteLine("Before: {0}", value);
            // var dups = list.GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key).ToList();

            #endregion

            #region MyRegion02
            //List<string> lstStrMyList = new List<string>() { "01", "02", "03", "My03", "04", "05", "03" };
            //List<string> lstStrIgnore = new List<string>() { "07", "08", "03", "03" };
            //string strIgnored = "03";

            //Console.WriteLine("List 1:");
            //List<string> lstStrFiltered = lstStrMyList.Distinct().Where(x => x != strIgnored).ToList();
            //foreach (var item in lstStrFiltered)
            //    Console.WriteLine(item);

            //Console.WriteLine("");

            //Console.WriteLine("List 2:");
            //List<string> lstStrFiltered2 = lstStrMyList.Distinct().Where(x => !lstStrIgnore.Any(e => x.Contains(e))).ToList();
            //foreach (var item in lstStrFiltered2)
            //    Console.WriteLine(item);

            #endregion

            #region MyRegion01

            //List<int> list = new List<int>();
            //list.Add(1);
            //list.Add(2);
            //list.Add(3);
            //list.Add(3);
            //list.Add(4);
            //list.Add(4);
            //list.Add(4);

            //Console.WriteLine("Lista Normal");
            //foreach (int value in list)
            //    Console.WriteLine("Before: {0}", value);

            //Console.WriteLine("");

            //Console.WriteLine("Lista Sem Duplicados");
            //List<int> distinct = list.Distinct().ToList();
            //foreach (int value in distinct)
            //    Console.WriteLine("After: {0}", value);

            #endregion

            Console.ReadKey();
        }
    }
}
