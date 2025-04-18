using BenchmarkDotNet.Running;
using CMS_DotNet_Teste_Object_Mapping.Benchmarks;

//#if DEBUG
//System.Console.ForegroundColor = System.ConsoleColor.Yellow;
//System.Console.WriteLine("*****To achieve accurate results, set project configuration to Release mode.*****");
//#endif

//var config = DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator);
BenchmarkRunner.Run<BenchmarkMappingPerson>();
Console.ReadLine();

// cd "C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste Benchmark\CMS DotNet Teste Benchmark Object Mapping"
// dotnet build -c Release
// dotnet run -c Release

// dotnet build -c Release
// dotnet run --configuration Release
// dotnet run "CMS DotNet Teste Object Mapping.csproj" -c Release
// dotnet run -p "CMS DotNet Teste Object Mapping.csproj" -c Release

// dotnet build -c Release
// dotnet "C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste Outros\CMS DotNet Teste Object Mapping\bin\Release\net9.0\CMS DotNet Teste Object Mapping.dll"

/*


| Method                  | NumberOfItems | Mean            | Error          | StdDev         | Median          | Gen0     | Gen1     | Gen2     | Allocated |
|------------------------ |-------------- |----------------:|---------------:|---------------:|----------------:|---------:|---------:|---------:|----------:|
| MapWithMapperly         | 1             |        17.94 ns |       0.057 ns |       0.050 ns |        17.94 ns |   0.0162 |        - |        - |     152 B |
| MapWithMapster          | 1             |        20.13 ns |       0.068 ns |       0.053 ns |        20.15 ns |   0.0119 |        - |        - |     112 B |
| MapWithImplicitOperator | 1             |        25.77 ns |       0.473 ns |       1.125 ns |        25.40 ns |   0.0196 |        - |        - |     184 B |
| MapWithManualMapping    | 1             |        26.97 ns |       0.587 ns |       1.450 ns |        26.32 ns |   0.0196 |        - |        - |     184 B |
| MapWithAutoMapper       | 1             |        54.67 ns |       0.117 ns |       0.104 ns |        54.69 ns |   0.0144 |        - |        - |     136 B |
| MapWithExpressMapper    | 1             |    27,714.08 ns |     261.541 ns |     231.849 ns |    27,702.23 ns |   0.4883 |   0.4272 |        - |    5014 B |
|                         |               |                 |                |                |                 |          |          |          |           |
| MapWithMapperly         | 10            |        83.03 ns |       0.287 ns |       0.255 ns |        83.11 ns |   0.0696 |   0.0001 |        - |     656 B |
| MapWithMapster          | 10            |        83.14 ns |       0.483 ns |       0.428 ns |        83.13 ns |   0.0654 |   0.0001 |        - |     616 B |
| MapWithManualMapping    | 10            |        90.20 ns |       0.450 ns |       0.375 ns |        90.10 ns |   0.0731 |   0.0001 |        - |     688 B |
| MapWithImplicitOperator | 10            |        90.71 ns |       0.261 ns |       0.218 ns |        90.68 ns |   0.0731 |   0.0001 |        - |     688 B |
| MapWithAutoMapper       | 10            |       149.25 ns |       0.600 ns |       0.561 ns |       149.27 ns |   0.0858 |   0.0002 |        - |     808 B |
| MapWithExpressMapper    | 10            |    27,788.56 ns |     271.800 ns |     254.242 ns |    27,701.56 ns |   0.5493 |   0.4883 |        - |    5686 B |
|                         |               |                 |                |                |                 |          |          |          |           |
| MapWithMapperly         | 100           |       653.44 ns |       1.463 ns |       1.222 ns |       653.71 ns |   0.6046 |   0.0124 |        - |    5696 B |
| MapWithManualMapping    | 100           |       692.05 ns |       3.609 ns |       3.200 ns |       691.82 ns |   0.6084 |   0.0124 |        - |    5728 B |
| MapWithMapster          | 100           |       708.26 ns |       6.724 ns |       5.615 ns |       707.54 ns |   0.6008 |   0.0124 |        - |    5656 B |
| MapWithImplicitOperator | 100           |       714.79 ns |       3.411 ns |       3.024 ns |       714.44 ns |   0.6084 |   0.0124 |        - |    5728 B |
| MapWithAutoMapper       | 100           |       961.56 ns |       1.936 ns |       1.811 ns |       961.03 ns |   0.7420 |   0.0153 |        - |    6992 B |
| MapWithExpressMapper    | 100           |    29,385.39 ns |     261.273 ns |     231.612 ns |    29,348.28 ns |   1.2207 |   1.1597 |        - |   11872 B |
|                         |               |                 |                |                |                 |          |          |          |           |
| MapWithMapperly         | 1000          |     6,484.39 ns |      25.894 ns |      24.222 ns |     6,476.91 ns |   5.9586 |   0.7935 |        - |   56096 B |
| MapWithManualMapping    | 1000          |     6,662.58 ns |      74.195 ns |      57.927 ns |     6,645.99 ns |   5.9586 |   0.9918 |        - |   56128 B |
| MapWithMapster          | 1000          |     6,973.11 ns |      45.724 ns |      38.181 ns |     6,972.80 ns |   5.9509 |   0.8469 |        - |   56056 B |
| MapWithImplicitOperator | 1000          |     7,454.25 ns |      28.720 ns |      22.422 ns |     7,452.00 ns |   5.9586 |   0.9918 |        - |   56128 B |
| MapWithAutoMapper       | 1000          |     8,290.47 ns |     165.061 ns |     169.506 ns |     8,211.69 ns |   6.8512 |   1.1292 |        - |   64600 B |
| MapWithExpressMapper    | 1000          |    44,654.84 ns |     296.938 ns |     277.756 ns |    44,663.56 ns |   7.3853 |   2.4414 |        - |   69484 B |
|                         |               |                 |                |                |                 |          |          |          |           |
| MapWithMapster          | 10000         |    76,294.49 ns |     577.126 ns |     511.607 ns |    76,108.44 ns |  59.4482 |  24.1699 |        - |  560056 B |
| MapWithMapperly         | 10000         |    77,233.84 ns |     575.948 ns |     510.563 ns |    77,477.17 ns |  59.4482 |  24.1699 |        - |  560096 B |
| MapWithManualMapping    | 10000         |    77,272.43 ns |     439.047 ns |     389.204 ns |    77,302.92 ns |  59.4482 |  24.0479 |        - |  560128 B |
| MapWithImplicitOperator | 10000         |    84,566.96 ns |     439.487 ns |     366.991 ns |    84,568.40 ns |  59.4482 |  24.0479 |        - |  560128 B |
| MapWithAutoMapper       | 10000         |   580,506.63 ns |  11,603.003 ns |  21,216.749 ns |   584,707.23 ns |  96.6797 |  94.7266 |  32.2266 |  742490 B |
| MapWithExpressMapper    | 10000         |   659,016.79 ns |  13,175.783 ns |  32,812.275 ns |   653,176.12 ns |  97.6563 |  96.6797 |  32.2266 |  747415 B |
|                         |               |                 |                |                |                 |          |          |          |           |
| MapWithMapster          | 100000        | 5,626,000.44 ns | 112,310.851 ns | 196,703.412 ns | 5,620,087.50 ns | 671.8750 | 664.0625 | 164.0625 | 5600226 B |
| MapWithMapperly         | 100000        | 5,921,701.68 ns | 115,947.540 ns | 209,077.348 ns | 5,929,539.06 ns | 671.8750 | 664.0625 | 164.0625 | 5600274 B |
| MapWithImplicitOperator | 100000        | 6,389,620.65 ns | 119,425.858 ns | 142,168.001 ns | 6,446,612.50 ns | 671.8750 | 664.0625 | 164.0625 | 5600307 B |
| MapWithManualMapping    | 100000        | 6,414,921.07 ns | 125,005.808 ns | 201,861.094 ns | 6,362,490.62 ns | 671.8750 | 664.0625 | 164.0625 | 5600307 B |
| MapWithAutoMapper       | 100000        | 7,190,969.29 ns | 118,474.321 ns |  98,931.424 ns | 7,206,045.31 ns | 710.9375 | 703.1250 | 187.5000 | 6897734 B |
| MapWithExpressMapper    | 100000        | 8,076,869.23 ns | 161,313.606 ns | 310,796.578 ns | 8,036,910.94 ns | 703.1250 | 687.5000 | 187.5000 | 6902918 B |

 */
