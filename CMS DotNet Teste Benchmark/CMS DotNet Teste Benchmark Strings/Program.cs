using BenchmarkDotNet.Running;
using TesteBenchmarkDotNet.Benchmarks;

Console.WriteLine("Inicio");

//BenchmarkRunner.Run<MemorBenchmarkery1>();
BenchmarkRunner.Run<MemorBenchmarkery2>();
//BenchmarkRunner.Run<MemorBenchmarkery3>();
//BenchmarkRunner.Run<MemorBenchmarkery4>();

Console.ReadLine();

// dotnet run TesteBenchmarkDotNet.csproj -c Release
// dotnet run -p TesteBenchmarkDotNet.csproj -c Release

// dotnet build -c Release
// dotnet C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste Outros\TesteBenchmarkDotNet\bin\Release\net7.0\TesteBenchmarkDotNet.dll

