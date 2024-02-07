using TesteBenchmarkDotNet.Data;

namespace TesteBenchmarkDotNet.Benchmarks
{

    // https://nishanc.medium.com/writing-better-performant-queries-with-linq-on-ef-core-6-0-%EF%B8%8F-85a1a406879
    public class Benchmark : IDisposable
    {
        protected readonly DataContext Context;

        public Benchmark()
        {
            Context = new DataContext();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
