using BenchmarkDotNet.Attributes;

namespace TesteBenchmarkDotNet.Benchmarks;

[MemoryDiagnoser]
public class BenchmarkConfiguration
{
    //private static readonly int[] Array = Enumerable.Range(0, 10_000).ToArray();
    private int[] _array = null!;

    [Params(100, 1_000, 10_000, 100_000)]
    public int NumberOfItems;

    //public BenchmarkConfiguration()  
    //{
    //    _array = new int[10];
    //    for (int i = 0; i < NumberOfItems; i++)
    //        _array[i] = i + 1;
    //}

    [GlobalSetup]
    public void Setup() 
    {
        _array = Enumerable
            .Range(0, NumberOfItems)
            .ToArray();
    }

    [Benchmark]
    public long CountNormalLoop() // LoopWithLengthInInsideFor
    {
        var count = 0L;

        for (int i = 0; i < _array.Length; i++)
            count += _array[i]; //_array[i] = i * 2;

        return count;
    }

    [Benchmark]
    public long CountNormalForeach() 
    {
        var count = 0L;

        foreach (int elemento in _array)
            count += elemento;

        return count;
    }

    [Benchmark]
    public long CountUnwoundLoop() // LoopWithLengthInOutsideFor
    {
        var count = 0L;
        var len = _array.Length;

        for (int i = 0; i < len; i++)
            count += _array[i]; //_array[i] = i * 2;

        return count;
    }

    [Benchmark]
    public long CountUnwoundLoop_5()
    {
        var count = 0L;
        var len = _array.Length;

        for (int i = 0; i < len; i += 5)
        {
            count += _array[i]; //_array[i] = i * 2;
            count += _array[i + 1]; //_array[i + 1] = (i + 1) * 2;
            count += _array[i + 2]; //_array[i + 2] = (i + 2) * 2;
            count += _array[i + 3]; //_array[i + 3] = (i + 3) * 2;
            count += _array[i + 4]; //_array[i + 4] = (i + 4) * 2;
        }

        return count;
    }

    [Benchmark]
    public long CountSum()
    {
        var count = _array.Sum();

        return count;
    }
}
