//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Columns;
//using BenchmarkDotNet.Configs;
//using BenchmarkDotNet.Reports;

//namespace CMS_DotNet_Teste_Benchmark_Mapping;

////You can benchmark across multiple .NET versions by updating the target frameworks in your .csproj file and adding the [SimpleJob] attribute to your benchmark class.
////[SimpleJob(RuntimeMoniker.Net60)]
////[SimpleJob(RuntimeMoniker.Net70)]
////[SimpleJob(RuntimeMoniker.Net80)]

////O atributo [SimpleJob] também pode ser usado para especificar quantas vezes ele deve ser iniciado, aquecido e iterado, simulando o desempenho do mundo real em condições de inicialização a frio.
////[SimpleJob(RunStrategy.ColdStart, launchCount: 3, warmupCount: 2, iterationCount: 10)]

////Você também pode utilizar o ManualConfig para criar configurações personalizadas para seus benchmarks.
////Por exemplo, você pode configurar a saída resumida de benchmarks com diferentes formatos de proporção.
//[Config(typeof(Config))]

////Por exemplo, [MemoryDiagnoser] mede o uso de memória, rastreia alocações e coleta de lixo.
//[MemoryDiagnoser]
//public class LoopBenchmark
//{
//    //[Params(100, 10_000, 1_000_000)] public int Size { get; set; }
//    public const int Size = 1000;
//    private readonly List<string> _items = ["www.", "nikolatech", ".net"];

//    private class Config : ManualConfig
//    {
//        public Config()
//        {
//            SummaryStyle = SummaryStyle.WithRatioStyle(RatioStyle.Trend);
//        }
//    }

//    // Um método marcado com o atributo [GlobalSetup] é executado uma vez para cada método comparado.
//    // Um método marcado com o atributo [GlobalCleanup] é executado uma vez para cada método comparado, após todas as invocações desse método terem sido concluídas.
//    [GlobalSetup]
//    public void Setup()
//    {
//        var random = new Random(123);

//        for (var i = 0; i < Size; i++)
//        {
//            var randomValue = random.Next();
//            _items.Add(randomValue.ToString());
//        }
//    }

//    //[Benchmark]
//    [Benchmark(Baseline = true)]
//    public string For()
//    {
//        var response = string.Empty;
//        var size = _items.Count;

//        for (var i = 0; i < size; i++)
//            response = _items[i];

//        return response;
//    }

//    [Benchmark]
//    public string Foreach()
//    {
//        var response = string.Empty;

//        foreach (var item in _items)
//            response = item;

//        return response;
//    }
//}