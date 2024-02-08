using BenchmarkDotNet.Attributes;
using System.Text;

namespace TesteBenchmarkDotNet.Benchmarks
{

    [MemoryDiagnoser]
    public class MemorBenchmarkery3
    {
        int n = 10_000;

        [Benchmark]
        public string Builder()
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < n; i++)
            {
                output.Append("falcon").Append(i);
            }

            return output.ToString();
        }

        [Benchmark]
        public string Interpolation()
        {
            string output = string.Empty;

            for (int i = 0; i < n; i++)
            {
                output = $"{output}falcon{i}";
            }

            return output;
        }

        [Benchmark]
        public string Addition()
        {
            string output = string.Empty;

            for (int i = 0; i < n; i++)
            {
                output += "falcon" + i;
            }

            return output.ToString();
        }
    }
}
