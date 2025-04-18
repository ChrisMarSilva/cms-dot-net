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

| Method                  | NumberOfItems | Mean          | Error         | StdDev        | Median        | Rank | Completed Work Items | Lock Contentions | Gen0    | Gen1    | Gen2    | Allocated |
|------------------------ |-------------- |--------------:|--------------:|--------------:|--------------:|-----:|---------------------:|-----------------:|--------:|--------:|--------:|----------:|
| MapWithMapperly         | 1             |      18.49 ns |      0.092 ns |      0.077 ns |      18.46 ns |    1 |                    - |                - |  0.0162 |       - |       - |     152 B |
| MapWithMapster          | 1             |      22.00 ns |      0.063 ns |      0.052 ns |      21.99 ns |    2 |                    - |                - |  0.0119 |       - |       - |     112 B |
| MapWithImplicitOperator | 1             |      28.01 ns |      0.605 ns |      1.208 ns |      27.79 ns |    3 |                    - |                - |  0.0196 |       - |       - |     184 B |
| MapWithManualMapping    | 1             |      28.35 ns |      0.777 ns |      2.279 ns |      27.25 ns |    3 |                    - |                - |  0.0196 |       - |       - |     184 B |
| MapWithAutoMapper       | 1             |      55.46 ns |      0.972 ns |      2.509 ns |      54.12 ns |    4 |                    - |                - |  0.0144 |       - |       - |     136 B |
| MapWithExpressMapper    | 1             |  28,242.79 ns |    226.457 ns |    211.828 ns |  28,287.51 ns |    5 |                    - |                - |  0.4883 |  0.4272 |       - |    5014 B |
|                         |               |               |               |               |               |      |                      |                  |         |         |         |           |
| MapWithMapperly         | 10            |      84.44 ns |      0.515 ns |      0.430 ns |      84.22 ns |    1 |                    - |                - |  0.0696 |  0.0001 |       - |     656 B |
| MapWithMapster          | 10            |      85.09 ns |      0.454 ns |      0.379 ns |      84.87 ns |    1 |                    - |                - |  0.0654 |  0.0001 |       - |     616 B |
| MapWithManualMapping    | 10            |      90.20 ns |      0.385 ns |      0.342 ns |      90.27 ns |    2 |                    - |                - |  0.0731 |  0.0001 |       - |     688 B |
| MapWithImplicitOperator | 10            |      91.29 ns |      0.688 ns |      0.644 ns |      91.15 ns |    2 |                    - |                - |  0.0731 |  0.0001 |       - |     688 B |
| MapWithAutoMapper       | 10            |     156.01 ns |      0.745 ns |      0.581 ns |     155.97 ns |    3 |                    - |                - |  0.0858 |  0.0002 |       - |     808 B |
| MapWithExpressMapper    | 10            |  28,190.53 ns |    201.015 ns |    178.195 ns |  28,143.79 ns |    4 |                    - |                - |  0.5493 |  0.4883 |       - |    5686 B |
|                         |               |               |               |               |               |      |                      |                  |         |         |         |           |
| MapWithMapperly         | 100           |     656.15 ns |      2.858 ns |      2.534 ns |     655.72 ns |    1 |                    - |                - |  0.6046 |  0.0124 |       - |    5696 B |
| MapWithManualMapping    | 100           |     705.15 ns |      2.182 ns |      1.822 ns |     705.57 ns |    2 |                    - |                - |  0.6084 |  0.0124 |       - |    5728 B |
| MapWithMapster          | 100           |     712.47 ns |      3.011 ns |      2.817 ns |     711.54 ns |    2 |                    - |                - |  0.6008 |  0.0124 |       - |    5656 B |
| MapWithImplicitOperator | 100           |     742.84 ns |      5.023 ns |      4.699 ns |     742.05 ns |    3 |                    - |                - |  0.6084 |  0.0124 |       - |    5728 B |
| MapWithAutoMapper       | 100           |     982.85 ns |      4.214 ns |      3.942 ns |     983.43 ns |    4 |                    - |                - |  0.7420 |  0.0153 |       - |    6992 B |
| MapWithExpressMapper    | 100           |  29,543.48 ns |    221.676 ns |    196.510 ns |  29,529.53 ns |    5 |                    - |                - |  1.2207 |  1.1597 |       - |   11872 B |
|                         |               |               |               |               |               |      |                      |                  |         |         |         |           |
| MapWithMapperly         | 1000          |   6,509.88 ns |     39.751 ns |     37.183 ns |   6,501.30 ns |    1 |                    - |                - |  5.9586 |  0.7935 |       - |   56096 B |
| MapWithMapster          | 1000          |   6,872.37 ns |     47.750 ns |     42.329 ns |   6,877.08 ns |    2 |                    - |                - |  5.9509 |  0.8469 |       - |   56056 B |
| MapWithManualMapping    | 1000          |   7,078.59 ns |     45.123 ns |     40.000 ns |   7,067.79 ns |    2 |                    - |                - |  5.9586 |  0.9918 |       - |   56128 B |
| MapWithImplicitOperator | 1000          |   7,620.16 ns |    148.574 ns |    138.976 ns |   7,586.85 ns |    3 |                    - |                - |  5.9509 |  0.9766 |       - |   56128 B |
| MapWithAutoMapper       | 1000          |   8,619.68 ns |     37.925 ns |     35.475 ns |   8,635.79 ns |    4 |                    - |                - |  6.8512 |  1.1292 |       - |   64600 B |
| MapWithExpressMapper    | 1000          |  45,028.23 ns |    238.727 ns |    211.625 ns |  45,018.13 ns |    5 |                    - |                - |  7.3853 |  2.4414 |       - |   69484 B |
|                         |               |               |               |               |               |      |                      |                  |         |         |         |           |
| MapWithManualMapping    | 10000         |  77,530.86 ns |    679.589 ns |    602.438 ns |  77,551.70 ns |    1 |                    - |                - | 59.4482 | 24.0479 |       - |  560128 B |
| MapWithMapperly         | 10000         |  80,256.02 ns |  1,583.817 ns |  1,945.069 ns |  79,910.10 ns |    1 |                    - |                - | 59.4482 | 24.1699 |       - |  560096 B |
| MapWithImplicitOperator | 10000         |  85,259.22 ns |    494.019 ns |    462.106 ns |  85,315.76 ns |    2 |                    - |                - | 59.4482 | 24.0479 |       - |  560128 B |
| MapWithMapster          | 10000         |  85,387.36 ns |  1,839.512 ns |  5,394.970 ns |  83,460.64 ns |    2 |                    - |                - | 59.4482 | 24.1699 |       - |  560056 B |
| MapWithAutoMapper       | 10000         | 594,093.48 ns | 11,797.362 ns | 24,884.643 ns | 596,903.96 ns |    3 |                    - |                - | 96.6797 | 94.7266 | 32.2266 |  742491 B |
| MapWithExpressMapper    | 10000         | 741,182.84 ns | 16,968.990 ns | 49,230.119 ns | 727,919.63 ns |    4 |                    - |                - | 97.6563 | 96.6797 | 32.2266 |  747425 B |

 */
