using BenchmarkDotNet.Running;
using TesteBenchmarkDotNet.Benchmarks;

BenchmarkRunner.Run<BenchmarkConfiguration>();
Console.ReadLine();

// dotnet build -c Release
// dotnet run TesteBenchmarkDotNet.csproj -c Release
// dotnet run -p TesteBenchmarkDotNet.csproj -c Release

// dotnet build -c Release
// dotnet C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste Outros\TesteBenchmarkDotNet\bin\Release\net7.0\TesteBenchmarkDotNet.dll

/*
 
 
|             Method | NumberOfItems |          Mean |        Error |        StdDev |        Median | Allocated |
|------------------- |-------------- |--------------:|-------------:|--------------:|--------------:|----------:|
|    CountNormalLoop |           100 |     105.91 ns |     2.161 ns |      5.177 ns |     103.82 ns |         - |
| CountNormalForeach |           100 |      95.81 ns |     4.084 ns |     12.040 ns |      92.43 ns |         - |
|   CountUnwoundLoop |           100 |     112.30 ns |     3.186 ns |      9.244 ns |     109.39 ns |         - |
| CountUnwoundLoop_5 |           100 |     111.06 ns |     1.888 ns |      2.826 ns |     110.23 ns |         - |
|           CountSum |           100 |      82.03 ns |     1.628 ns |      1.271 ns |      81.66 ns |         - |
|    CountNormalLoop |          1000 |   1,127.99 ns |    45.455 ns |    134.025 ns |   1,102.70 ns |         - |
| CountNormalForeach |          1000 |     851.59 ns |    33.261 ns |     97.547 ns |     817.50 ns |         - |
|   CountUnwoundLoop |          1000 |     932.94 ns |    18.469 ns |     26.488 ns |     923.44 ns |         - |
| CountUnwoundLoop_5 |          1000 |   1,126.58 ns |    22.520 ns |     23.126 ns |   1,119.76 ns |         - |
|           CountSum |          1000 |     681.21 ns |    13.146 ns |     18.854 ns |     678.91 ns |         - |
|    CountNormalLoop |         10000 |  10,415.31 ns |   551.349 ns |  1,536.940 ns |   9,626.57 ns |         - |
| CountNormalForeach |         10000 |   7,766.30 ns |   139.412 ns |    195.437 ns |   7,733.42 ns |         - |
|   CountUnwoundLoop |         10000 |   9,269.41 ns |   180.680 ns |    193.325 ns |   9,234.04 ns |         - |
| CountUnwoundLoop_5 |         10000 |  11,198.51 ns |   187.514 ns |    297.417 ns |  11,142.94 ns |         - |
|           CountSum |         10000 |   6,753.44 ns |   135.057 ns |    253.670 ns |   6,735.61 ns |         - |
|    CountNormalLoop |        100000 |  93,990.83 ns | 1,854.506 ns |  1,821.373 ns |  93,521.22 ns |         - |
| CountNormalForeach |        100000 |  91,506.20 ns | 4,586.041 ns | 13,522.044 ns |  86,931.62 ns |         - |
|   CountUnwoundLoop |        100000 |  95,142.98 ns | 1,820.410 ns |  1,869.427 ns |  95,040.40 ns |       1 B |
| CountUnwoundLoop_5 |        100000 | 111,453.86 ns | 1,790.794 ns |  1,495.394 ns | 111,234.86 ns |         - |
|           CountSum |        100000 |            NA |           NA |            NA |            NA |         - |


 */