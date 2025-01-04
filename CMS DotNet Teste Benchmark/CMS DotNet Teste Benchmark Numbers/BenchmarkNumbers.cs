using BenchmarkDotNet.Attributes;
using System.Collections;

namespace CMS_DotNet_Teste_Benchmark_Numbers;

[MemoryDiagnoser]
public class BenchmarkNumbers
{
    private List<int> numbers;
    private ArrayList arrayList;

    [GlobalSetup]
    public void Setup()
    {
        numbers = new List<int> { 1, 2, 3, 4, 5, 3, 2, 6, 7, 5, 2, 3, 4, 5, 6, 7, 7, 8, 8, 8 };
        arrayList = new ArrayList { 1, 2, 3, 4, 5, 3, 2, 6, 7, 5, 2, 3, 4, 5, 6, 7, 7, 8, 8, 8 };
    }

    // Método para encontrar os duplicados usando Dictionary
    [Benchmark]
    public List<int> FindDuplicatesWithDictionary()
    {
        var countDictionary = new Dictionary<int, int>();
        foreach (var number in numbers)
        {
            if (countDictionary.ContainsKey(number))
                countDictionary[number]++;
            else
                countDictionary[number] = 1;
        }

        return countDictionary.Where(kvp => kvp.Value > 1).Select(kvp => kvp.Key).ToList();
    }

    // Método para encontrar os duplicados usando LINQ
    [Benchmark]
    public List<int> FindDuplicatesWithLinq()
    {
        return numbers.GroupBy(n => n)
                      .Where(g => g.Count() > 1)
                      .Select(g => g.Key)
                      .ToList();
    }

    [Benchmark]
    public ArrayList FindDuplicates()
    {
        //var seen = new HashSet<int>();
        //var duplicates = new HashSet<int>();

        Hashtable seen = new Hashtable();
        ArrayList duplicates = new ArrayList();

        foreach (var number in arrayList)
        {
            if (seen.ContainsKey(number))
            {
                if (!duplicates.Contains(number))
                {
                    duplicates.Add(number);
                }
            }
            else
            {
                seen[number] = true;
            }
        }

        return duplicates;
    }
}
