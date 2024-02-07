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


| Method                  | NumberOfItems | Mean          | Error        | StdDev       | Ratio | RatioSD | Rank | Completed Work Items | Lock Contentions | Gen0    | Gen1    | Gen2    | Allocated | Alloc Ratio |
|------------------------ |-------------- |--------------:|-------------:|-------------:|------:|--------:|-----:|---------------------:|-----------------:|--------:|--------:|--------:|----------:|------------:|
| MapWithMapperly         | 1             |      13.45 ns |     0.080 ns |     0.071 ns |  0.31 |    0.00 |    1 |                    - |                - |  0.0119 |       - |       - |     112 B |        0.61 |
| MapWithMapster          | 1             |      21.85 ns |     0.098 ns |     0.092 ns |  0.50 |    0.00 |    3 |                    - |                - |  0.0119 |       - |       - |     112 B |        0.61 |
| MapWithManualMapping    | 1             |      40.99 ns |     0.262 ns |     0.245 ns |  0.93 |    0.01 |    4 |                    - |                - |  0.0196 |       - |       - |     184 B |        1.00 |
| MapWithImplicitOperator | 1             |      43.98 ns |     0.302 ns |     0.268 ns |  1.00 |    0.00 |    5 |                    - |                - |  0.0196 |       - |       - |     184 B |        1.00 |
| MapWithAutoMapper       | 1             |      58.12 ns |     0.552 ns |     0.489 ns |  1.32 |    0.01 |    6 |                    - |                - |  0.0144 |       - |       - |     136 B |        0.74 |
| MapWithAutoMapper       | 1             |      59.68 ns |     0.317 ns |     0.248 ns |  1.36 |    0.01 |    7 |                    - |                - |  0.0144 |       - |       - |     136 B |        0.74 |
| MapWithAgileMapper      | 1             |     206.91 ns |     1.951 ns |     1.825 ns |  4.71 |    0.06 |    8 |                    - |                - |  0.0398 |       - |       - |     376 B |        2.04 |
| MapWithAgileMapper      | 1             |     207.92 ns |     2.929 ns |     2.740 ns |  4.73 |    0.07 |    8 |                    - |                - |  0.0398 |       - |       - |     376 B |        2.04 |
| MapWithExpressMapper    | 1             |     349.88 ns |     1.974 ns |     1.750 ns |  7.96 |    0.05 |    9 |                    - |                - |  0.0668 |       - |       - |     632 B |        3.43 |
| MapWithExpressMapper    | 1             |     353.19 ns |     2.839 ns |     2.517 ns |  8.03 |    0.06 |    9 |                    - |                - |  0.0668 |       - |       - |     632 B |        3.43 |
|                         |               |               |              |              |       |         |      |                      |                  |         |         |         |           |             |
| MapWithMapperly         | 10            |      71.96 ns |     0.499 ns |     0.466 ns |  0.65 |    0.01 |    1 |                    - |                - |  0.0654 |  0.0001 |       - |     616 B |        0.90 |
| MapWithMapster          | 10            |      80.77 ns |     0.725 ns |     0.678 ns |  0.73 |    0.01 |    2 |                    - |                - |  0.0654 |  0.0001 |       - |     616 B |        0.90 |
| MapWithManualMapping    | 10            |     100.45 ns |     0.561 ns |     0.498 ns |  0.90 |    0.01 |    4 |                    - |                - |  0.0731 |  0.0001 |       - |     688 B |        1.00 |
| MapWithImplicitOperator | 10            |     111.30 ns |     0.513 ns |     0.455 ns |  1.00 |    0.00 |    5 |                    - |                - |  0.0731 |  0.0001 |       - |     688 B |        1.00 |
| MapWithAutoMapper       | 10            |     155.62 ns |     0.729 ns |     0.682 ns |  1.40 |    0.01 |    7 |                    - |                - |  0.0858 |  0.0002 |       - |     808 B |        1.17 |
| MapWithAgileMapper      | 10            |     272.10 ns |     2.150 ns |     2.011 ns |  2.44 |    0.02 |    8 |                    - |                - |  0.0935 |       - |       - |     880 B |        1.28 |
| MapWithExpressMapper    | 10            |     512.27 ns |     4.308 ns |     4.030 ns |  4.60 |    0.04 |   10 |                    - |                - |  0.1459 |       - |       - |    1376 B |        2.00 |
|                         |               |               |              |              |       |         |      |                      |                  |         |         |         |           |             |
| MapWithTinyMapper       | 100           |            NA |           NA |           NA |     ? |       ? |    ? |                   NA |               NA |      NA |      NA |      NA |        NA |           ? |
| MapWithManualMapping    | 100           |     542.98 ns |     4.693 ns |     3.919 ns |  0.72 |    0.01 |    1 |                    - |                - |  0.6084 |  0.0124 |       - |    5728 B |        1.00 |
| MapWithMapperly         | 100           |     557.80 ns |     3.217 ns |     3.009 ns |  0.74 |    0.00 |    2 |                    - |                - |  0.6008 |  0.0124 |       - |    5656 B |        0.99 |
| MapWithMapster          | 100           |     574.50 ns |     2.461 ns |     2.302 ns |  0.76 |    0.00 |    3 |                    - |                - |  0.6008 |  0.0124 |       - |    5656 B |        0.99 |
| MapWithImplicitOperator | 100           |     755.53 ns |     1.669 ns |     1.479 ns |  1.00 |    0.00 |    4 |                    - |                - |  0.6084 |  0.0124 |       - |    5728 B |        1.00 |
| MapWithAutoMapper       | 100           |     904.48 ns |     3.485 ns |     3.089 ns |  1.20 |    0.00 |    5 |                    - |                - |  0.7429 |  0.0162 |       - |    6992 B |        1.22 |
| MapWithAgileMapper      | 100           |     931.15 ns |     4.274 ns |     3.789 ns |  1.23 |    0.01 |    6 |                    - |                - |  0.6285 |  0.0134 |       - |    5920 B |        1.03 |
| MapWithExpressMapper    | 100           |   1,912.50 ns |    10.982 ns |    10.273 ns |  2.53 |    0.01 |    7 |                    - |                - |  0.8774 |  0.0191 |       - |    8281 B |        1.45 |
|                         |               |               |              |              |       |         |      |                      |                  |         |         |         |           |             |
| MapWithTinyMapper       | 1000          |            NA |           NA |           NA |     ? |       ? |    ? |                   NA |               NA |      NA |      NA |      NA |        NA |           ? |
| MapWithManualMapping    | 1000          |   5,168.46 ns |    39.425 ns |    34.949 ns |  0.68 |    0.01 |    1 |                    - |                - |  5.9586 |  0.9918 |       - |   56128 B |        1.00 |
| MapWithMapperly         | 1000          |   5,826.07 ns |    86.286 ns |    72.052 ns |  0.76 |    0.01 |    2 |                    - |                - |  5.9509 |  0.9842 |       - |   56056 B |        1.00 |
| MapWithMapster          | 1000          |   5,900.83 ns |    43.295 ns |    36.154 ns |  0.77 |    0.01 |    2 |                    - |                - |  5.9509 |  0.9842 |       - |   56056 B |        1.00 |
| MapWithAgileMapper      | 1000          |   7,474.99 ns |   138.566 ns |   136.091 ns |  0.98 |    0.02 |    3 |                    - |                - |  5.9814 |  0.8926 |       - |   56320 B |        1.00 |
| MapWithAutoMapper       | 1000          |   7,511.59 ns |    13.715 ns |    11.453 ns |  0.98 |    0.01 |    3 |                    - |                - |  6.8588 |  1.1368 |       - |   64600 B |        1.15 |
| MapWithImplicitOperator | 1000          |   7,642.89 ns |    42.904 ns |    40.132 ns |  1.00 |    0.00 |    4 |                    - |                - |  5.9586 |  0.9918 |       - |   56128 B |        1.00 |
| MapWithExpressMapper    | 1000          |  15,089.80 ns |    58.311 ns |    51.691 ns |  1.97 |    0.01 |    5 |                    - |                - |  7.7515 |  1.2817 |       - |   73099 B |        1.30 |
|                         |               |               |              |              |       |         |      |                      |                  |         |         |         |           |             |
| MapWithTinyMapper       | 10000         |            NA |           NA |           NA |     ? |       ? |    ? |                   NA |               NA |      NA |      NA |      NA |        NA |           ? |
| MapWithMapperly         | 10000         |  68,257.22 ns |   574.771 ns |   479.960 ns |  0.75 |    0.02 |    1 |                    - |                - | 59.4482 | 24.1699 |       - |  560056 B |        1.00 |
| MapWithManualMapping    | 10000         |  70,222.24 ns |   729.292 ns |   646.498 ns |  0.78 |    0.02 |    2 |                    - |                - | 59.4482 | 24.0479 |       - |  560128 B |        1.00 |
| MapWithMapster          | 10000         |  72,568.42 ns |   922.432 ns |   770.273 ns |  0.80 |    0.02 |    3 |                    - |                - | 59.4482 | 24.1699 |       - |  560056 B |        1.00 |
| MapWithAgileMapper      | 10000         |  83,115.74 ns |   967.908 ns |   808.247 ns |  0.92 |    0.02 |    4 |                    - |                - | 59.4482 | 23.8037 |       - |  560320 B |        1.00 |
| MapWithImplicitOperator | 10000         |  90,404.96 ns | 1,773.124 ns | 1,658.582 ns |  1.00 |    0.00 |    5 |                    - |                - | 59.4482 | 24.0479 |       - |  560128 B |        1.00 |
| MapWithAutoMapper       | 10000         | 297,793.93 ns | 5,839.639 ns | 7,593.180 ns |  3.33 |    0.10 |    6 |                    - |                - | 83.0078 | 82.5195 | 41.5039 |  742470 B |        1.33 |
| MapWithExpressMapper    | 10000         | 361,702.85 ns | 5,461.034 ns | 5,108.255 ns |  4.00 |    0.09 |    7 |                    - |                - | 83.0078 | 82.5195 | 41.5039 |  823076 B |        1.47 |



 */
