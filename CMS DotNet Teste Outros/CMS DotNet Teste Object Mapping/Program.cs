using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using CMS_DotNet_Teste_Object_Mapping.Benchmarks;

#if DEBUG
System.Console.ForegroundColor = System.ConsoleColor.Yellow;
System.Console.WriteLine("*****To achieve accurate results, set project configuration to Release mode.*****");
return;
#endif

var config = DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator);
BenchmarkRunner.Run<BenchmarkConfiguration>(config);
Console.ReadLine();

// dotnet build -c Release
// dotnet run --configuration Release
// dotnet run "CMS DotNet Teste Object Mapping.csproj" -c Release
// dotnet run -p "CMS DotNet Teste Object Mapping.csproj" -c Release

// dotnet build -c Release
// dotnet "C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste Outros\CMS DotNet Teste Object Mapping\bin\Release\net8.0\CMS DotNet Teste Object Mapping.dll"


/*
 
  Method            | NumberOfItems | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Rank | Gen0   | Completed Work Items | Lock Contentions | Allocated | Alloc Ratio |
|------------------ |-------------- |----------:|----------:|----------:|----------:|------:|--------:|-----:|-------:|---------------------:|-----------------:|----------:|------------:|
| ImplicitOperator2 | 100           |  13.24 ns |  0.319 ns |  0.249 ns |  13.22 ns |  0.76 |    0.15 |    1 | 0.0306 |                    - |                - |      48 B |        1.00 |
| Mapperly          | 100           |  15.27 ns |  0.303 ns |  0.324 ns |  15.16 ns |  0.91 |    0.15 |    2 | 0.0306 |                    - |                - |      48 B |        1.00 |
| ImplicitOperator  | 100           |  17.00 ns |  1.391 ns |  4.102 ns |  14.62 ns |  1.00 |    0.00 |    2 | 0.0306 |                    - |                - |      48 B |        1.00 |
| Mapster           | 100           |  43.14 ns |  1.792 ns |  5.200 ns |  41.15 ns |  2.67 |    0.65 |    3 | 0.0306 |                    - |                - |      48 B |        1.00 |
| TinyMapper        | 100           |  51.88 ns |  1.017 ns |  0.849 ns |  51.84 ns |  3.02 |    0.57 |    4 | 0.0305 |                    - |                - |      48 B |        1.00 |
| AutoMapper        | 100           | 134.31 ns |  2.745 ns |  4.510 ns | 133.20 ns |  6.83 |    1.66 |    5 | 0.0305 |                    - |                - |      48 B |        1.00 |
| ExpressMapper     | 100           | 142.64 ns |  3.282 ns |  8.930 ns | 140.10 ns |  8.65 |    1.90 |    6 | 0.0560 |                    - |                - |      88 B |        1.83 |
| AgileObjects      | 100           | 658.66 ns | 13.281 ns | 37.459 ns | 646.54 ns | 40.21 |    7.98 |    7 | 0.1984 |                    - |                - |     312 B |        6.50 |

// * Hints *
Outliers
  BenchmarkConfiguration.ImplicitOperator2: Default -> 3 outliers were removed (17.53 ns..21.00 ns)
  BenchmarkConfiguration.Mapperly: Default          -> 3 outliers were removed (19.35 ns..24.32 ns)
  BenchmarkConfiguration.Mapster: Default           -> 3 outliers were removed (57.83 ns..59.58 ns)
  BenchmarkConfiguration.TinyMapper: Default        -> 2 outliers were removed (62.33 ns, 72.78 ns)
  BenchmarkConfiguration.AutoMapper: Default        -> 5 outliers were removed (169.32 ns..192.53 ns)
  BenchmarkConfiguration.ExpressMapper: Default     -> 14 outliers were removed (177.35 ns..241.15 ns)
  BenchmarkConfiguration.AgileObjects: Default      -> 8 outliers were removed (785.34 ns..909.66 ns)

 */
