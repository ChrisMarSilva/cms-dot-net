using BenchmarkDotNet.Attributes;

namespace TesteBenchmarkDotNet.Benchmarks
{

    [MemoryDiagnoser]
    public class MemorBenchmarkery4
    {
        const int n = 100_000;
        int[] vals = new int[n];

        [GlobalSetup]
        public void GlobalSetup()
        {
            var rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                vals[i] = rnd.Next(1, 100);
            }
        }

        [Benchmark]
        public void SelectionSort()
        {
            int len = vals.Length;

            for (int i = 0; i < len - 1; i++)
            {
                int min_idx = i;

                for (int j = i + 1; j < len; j++)
                {
                    if (vals[j] < vals[min_idx])
                    {
                        min_idx = j;
                    }
                }

                int temp = vals[min_idx];
                vals[min_idx] = vals[i];
                vals[i] = temp;
            }
        }

        [Benchmark]
        public void BubbleSort()
        {
            int len = vals.Length;

            for (int i = 0; i < len - 1; i++)
            {
                for (int j = 0; j < len - i - 1; j++)
                {
                    if (vals[j] > vals[j + 1])
                    {
                        int temp = vals[j];
                        vals[j] = vals[j + 1];
                        vals[j + 1] = temp;
                    }
                }
            }
        }
    }
}
