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

| Method                  | NumberOfItems | Mean          | Error        | StdDev       | Median        | Ratio | RatioSD | Rank | Completed Work Items | Lock Contentions | Gen0    | Gen1    | Gen2    | Allocated | Alloc Ratio |
|------------------------ |-------------- |--------------:|-------------:|-------------:|--------------:|------:|--------:|-----:|---------------------:|-----------------:|--------:|--------:|--------:|----------:|------------:|
| MapWithTinyMapper       | 1             |            NA |           NA |           NA |            NA |     ? |       ? |    ? |         NA |               NA |      NA |      NA |      NA |        NA |           ? |
| MapWithMapperly         | 1             |      14.27 ns |     0.323 ns |     0.540 ns |      14.17 ns |  0.33 |    0.01 |    1 |          - |                - |  0.0119 |       - |       - |     112 B |        0.61 |
| MapWithMapster          | 1             |      23.77 ns |     0.484 ns |     0.429 ns |      23.80 ns |  0.53 |    0.01 |    2 |          - |                - |  0.0119 |       - |       - |     112 B |        0.61 |
| MapWithManualMapping    | 1             |      43.93 ns |     0.421 ns |     0.394 ns |      43.97 ns |  0.98 |    0.01 |    4 |          - |                - |  0.0196 |       - |       - |     184 B |        1.00 |
| MapWithImplicitOperator | 1             |      44.80 ns |     0.412 ns |     0.365 ns |      44.86 ns |  1.00 |    0.00 |    4 |          - |                - |  0.0196 |       - |       - |     184 B |        1.00 |
| MapWithAutoMapper       | 1             |      59.54 ns |     0.681 ns |     0.604 ns |      59.60 ns |  1.33 |    0.02 |    5 |          - |                - |  0.0144 |       - |       - |     136 B |        0.74 |
| MapWithAgileMapper      | 1             |     217.94 ns |     4.005 ns |     3.746 ns |     218.68 ns |  4.87 |    0.10 |    6 |          - |                - |  0.0398 |       - |       - |     376 B |        2.04 |
| MapWithExpressMapper    | 1             |     369.89 ns |     6.324 ns |     5.281 ns |     369.57 ns |  8.25 |    0.11 |    7 |          - |                - |  0.0668 |       - |       - |     632 B |        3.43 |

| MapWithTinyMapper       | 10            |            NA |           NA |           NA |            NA |     ? |       ? |    ? |         NA |               NA |      NA |      NA |      NA |        NA |           ? |
| MapWithMapperly         | 10            |      76.34 ns |     1.576 ns |     1.751 ns |      77.03 ns |  0.66 |    0.02 |    1 |          - |                - |  0.0654 |  0.0001 |       - |     616 B |        0.90 |
| MapWithMapster          | 10            |      84.55 ns |     1.655 ns |     1.292 ns |      84.66 ns |  0.73 |    0.01 |    2 |          - |                - |  0.0654 |  0.0001 |       - |     616 B |        0.90 |
| MapWithManualMapping    | 10            |     100.58 ns |     0.464 ns |     0.387 ns |     100.55 ns |  0.87 |    0.01 |    4 |          - |                - |  0.0731 |  0.0001 |       - |     688 B |        1.00 |
| MapWithImplicitOperator | 10            |     114.94 ns |     1.616 ns |     1.511 ns |     114.63 ns |  1.00 |    0.00 |    6 |          - |                - |  0.0730 |       - |       - |     688 B |        1.00 |
| MapWithAutoMapper       | 10            |     160.34 ns |     2.634 ns |     2.335 ns |     159.73 ns |  1.39 |    0.03 |    7 |          - |                - |  0.0858 |  0.0002 |       - |     808 B |        1.17 |
| MapWithAgileMapper      | 10            |     277.60 ns |     4.748 ns |     4.209 ns |     276.85 ns |  2.41 |    0.05 |    9 |          - |                - |  0.0935 |       - |       - |     880 B |        1.28 |
| MapWithExpressMapper    | 10            |     537.19 ns |    10.732 ns |    18.796 ns |     530.23 ns |  4.62 |    0.13 |   10 |          - |                - |  0.1459 |       - |       - |    1376 B |        2.00 |

| MapWithTinyMapper       | 100           |            NA |           NA |           NA |            NA |     ? |       ? |    ? |         NA |               NA |      NA |      NA |      NA |        NA |           ? |
| MapWithManualMapping    | 100           |     559.84 ns |    11.004 ns |    14.308 ns |     553.43 ns |  0.73 |    0.02 |    1 |          - |                - |  0.6084 |  0.0124 |       - |    5728 B |        1.00 |
| MapWithMapster          | 100           |     582.54 ns |     2.602 ns |     2.173 ns |     582.56 ns |  0.75 |    0.01 |    2 |          - |                - |  0.6008 |  0.0124 |       - |    5656 B |        0.99 |
| MapWithMapperly         | 100           |     586.11 ns |    11.610 ns |    25.968 ns |     576.31 ns |  0.76 |    0.05 |    2 |          - |                - |  0.6008 |  0.0124 |       - |    5656 B |        0.99 |
| MapWithImplicitOperator | 100           |     774.73 ns |    14.920 ns |    13.226 ns |     772.51 ns |  1.00 |    0.00 |    3 |          - |                - |  0.6084 |  0.0124 |       - |    5728 B |        1.00 |
| MapWithAutoMapper       | 100           |     905.15 ns |    11.310 ns |    10.026 ns |     904.03 ns |  1.17 |    0.02 |    4 |          - |                - |  0.7420 |  0.0153 |       - |    6992 B |        1.22 |
| MapWithAgileMapper      | 100           |     957.80 ns |    18.803 ns |    33.423 ns |     950.29 ns |  1.23 |    0.04 |    5 |          - |                - |  0.6285 |  0.0134 |       - |    5920 B |        1.03 |
| MapWithExpressMapper    | 100           |   1,969.27 ns |    38.561 ns |    60.035 ns |   1,959.99 ns |  2.55 |    0.06 |    6 |          - |                - |  0.8774 |  0.0191 |       - |    8281 B |        1.45 |

| MapWithTinyMapper       | 1000          |            NA |           NA |           NA |            NA |     ? |       ? |    ? |         NA |               NA |      NA |      NA |      NA |        NA |           ? |
| MapWithManualMapping    | 1000          |   5,414.91 ns |   107.341 ns |   204.228 ns |   5,349.64 ns |  0.69 |    0.03 |    1 |          - |                - |  5.9586 |  0.9918 |       - |   56128 B |        1.00 |
| MapWithMapperly         | 1000          |   5,558.75 ns |    89.679 ns |    79.498 ns |   5,553.26 ns |  0.70 |    0.03 |    2 |          - |                - |  5.9509 |  0.9842 |       - |   56056 B |        1.00 |
| MapWithMapster          | 1000          |   6,064.64 ns |    68.315 ns |    57.046 ns |   6,075.61 ns |  0.76 |    0.02 |    3 |          - |                - |  5.9509 |  0.9842 |       - |   56056 B |        1.00 |
| MapWithAgileMapper      | 1000          |   7,677.74 ns |   150.639 ns |   147.948 ns |   7,639.34 ns |  0.96 |    0.04 |    4 |          - |                - |  5.9814 |  0.8926 |       - |   56320 B |        1.00 |
| MapWithImplicitOperator | 1000          |   7,983.31 ns |   149.912 ns |   246.309 ns |   7,932.79 ns |  1.00 |    0.00 |    5 |          - |                - |  5.9586 |  0.9918 |       - |   56128 B |        1.00 |
| MapWithAutoMapper       | 1000          |   7,999.09 ns |   147.402 ns |   238.027 ns |   7,903.03 ns |  1.00 |    0.04 |    5 |          - |                - |  6.8512 |  1.1292 |       - |   64600 B |        1.15 |
| MapWithExpressMapper    | 1000          |  15,189.53 ns |   205.298 ns |   192.036 ns |  15,195.95 ns |  1.91 |    0.06 |    6 |          - |                - |  7.7515 |  1.2817 |       - |   73099 B |        1.30 |

| MapWithTinyMapper       | 10000         |            NA |           NA |           NA |            NA |     ? |       ? |    ? |         NA |               NA |      NA |      NA |      NA |        NA |           ? |
| MapWithManualMapping    | 10000         |  65,979.12 ns | 1,276.553 ns | 1,131.631 ns |  66,003.71 ns |  0.78 |    0.02 |    1 |          - |                - | 59.4482 | 24.0479 |       - |  560128 B |        1.00 |
| MapWithMapperly         | 10000         |  69,580.11 ns | 1,348.349 ns | 2,059.071 ns |  68,837.77 ns |  0.83 |    0.03 |    2 |          - |                - | 59.4482 | 24.1699 |       - |  560056 B |        1.00 |
| MapWithMapster          | 10000         |  73,372.20 ns | 1,271.449 ns | 1,127.107 ns |  73,251.02 ns |  0.86 |    0.02 |    3 |          - |                - | 59.4482 | 24.1699 |       - |  560056 B |        1.00 |
| MapWithAgileMapper      | 10000         |  83,425.85 ns | 1,533.761 ns | 3,133.067 ns |  82,459.45 ns |  1.00 |    0.04 |    4 |          - |                - | 59.4482 | 23.8037 |       - |  560320 B |        1.00 |
| MapWithImplicitOperator | 10000         |  84,776.94 ns | 1,577.972 ns | 1,317.678 ns |  84,395.18 ns |  1.00 |    0.00 |    4 |          - |                - | 59.4482 | 24.0479 |       - |  560128 B |        1.00 |
| MapWithAutoMapper       | 10000         | 295,320.40 ns | 5,906.078 ns | 6,564.590 ns | 293,902.05 ns |  3.47 |    0.06 |    5 |          - |                - | 83.0078 | 82.5195 | 41.5039 |  742470 B |        1.33 |
| MapWithExpressMapper    | 10000         | 363,853.67 ns | 7,134.940 ns | 6,674.027 ns | 360,960.11 ns |  4.29 |    0.10 |    6 |          - |                - | 83.0078 | 82.5195 | 41.5039 |  823076 B |        1.47 |

Global total time: 00:24:32 (1472.95 sec), executed benchmarks: 56

 */
