using BenchmarkDotNet.Running;
using CMS_DotNet_Teste_Benchmark_Numbers;

Console.WriteLine("INI");
try
{
    BenchmarkRunner.Run<BenchmarkNumbers>();
}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex.Message}");
}
finally
{
    Console.WriteLine("FIM");
    Console.ReadKey();
}