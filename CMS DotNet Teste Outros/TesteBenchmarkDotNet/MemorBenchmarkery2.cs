using BenchmarkDotNet.Attributes;
using System.Text;

namespace TesteBenchmarkDotNet
{
    [MemoryDiagnoser]
    public class MemorBenchmarkery2
    {
        int NumeroDeItens = 1_000;

        [Benchmark]
        public string ConcatenandoStringsCom_StringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < NumeroDeItens; i++)
            {
                sb.Append("Macoratti.net_" + i);
            }
            return sb.ToString();
        }

        [Benchmark]
        public string ConcatStringsUsando_GenericList()
        {
            var list = new List<string>(NumeroDeItens);
            for (int i = 0; i < NumeroDeItens; i++)
            {
                list.Add("Macoratti.net_" + i);
            }
            return list.ToString();
        }
    }
}
