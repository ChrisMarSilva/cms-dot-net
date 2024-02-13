using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Immutable;
using System.Threading.Channels;

//var log = (object msg) => Console.WriteLine(msg);

//var envioDeEmails = Enumerable.Range(0, 200)
//    .Select(i => (Index: i, Tempo: Random.Shared.Next(10, 100)))
//    .ToImmutableArray();

//await EnviarEmailAsync("Parallel.ForEachAsync", async () =>
//{
//    await Parallel.ForEachAsync(envioDeEmails, async (item, cancel) => 
//    {
//        await Task.Delay(item.Tempo, cancel);
//    });
//});

//await EnviarEmailAsync("Parallel.ForEachAsync com 100 MaxDegreeOfParallelism", async () =>
//{
//    await Parallel.ForEachAsync(envioDeEmails, new ParallelOptions { MaxDegreeOfParallelism = 100 }, async (item, cancel) =>
//    {
//        await Task.Delay(item.Tempo, cancel);
//    });
//});

//await EnviarEmailAsync("Parallel.ForEachAsync com 10 MaxDegreeOfParallelism", async () =>
//{
//    await Parallel.ForEachAsync(envioDeEmails, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async (item, cancel) =>
//    {
//        await Task.Delay(item.Tempo, cancel);
//    });
//});

//await EnviarEmailAsync("Parallel.For com 100 MaxDegreeOfParallelism", async () =>
//{
//    Parallel.For(0, envioDeEmails.Length, new ParallelOptions { MaxDegreeOfParallelism = 100 }, async (i) =>
//    {
//        Thread.Sleep(envioDeEmails[i].Tempo);
//    });
//});

//await EnviarEmailAsync("Parallel.For com 10 MaxDegreeOfParallelism", async () =>
//{
//    Parallel.For(0, envioDeEmails.Length, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async (i) =>
//    {
//        Thread.Sleep(envioDeEmails[i].Tempo);
//    });
//});

//await EnviarEmailAsync("Threading.Channels", async () =>
//{
//    var channel = Channel.CreateUnbounded<int>();

//    async Task Enviar(ChannelWriter<int> writer, int id, int tempo)
//    {
//        await Task.Delay(tempo);
//        await writer.WriteAsync(id);
//    }

//    async Task Receber(ChannelReader<int> reader)
//    {
//        while (await reader.WaitToReadAsync())
//        {
//            if (reader.TryRead(out var id))
//            {
//                // log(id + "email enviado");
//            }
//        }
//    }

//    var tarefaReceber = Receber(channel.Reader);
//    var processadoTarefas = envioDeEmails.AsParallel().Select(p => Enviar(channel.Writer, p.Index, p.Tempo));
//    await Task.WhenAll(processadoTarefas).ContinueWith(_ => channel.Writer.Complete());
//    await tarefaReceber;
//});

//async Task EnviarEmailAsync(string nome, Func<Task> processo)
//{
//    //var threadIncial = Process.GetCurrentProcess().Threads.Count;

//    var time = new Stopwatch();
//    time.Start();
//    await processo();
//    time.Stop();

//    Console.WriteLine($"[{nome}] = {time.ElapsedMilliseconds}ms");
//    //Console.WriteLine($"   {threadIncial} -> inicio");
//    //Console.WriteLine($"   {Process.GetCurrentProcess().Threads.Count} -> final");
//}

Console.WriteLine("Inicio");
BenchmarkRunner.Run<Email>();
Console.ReadLine();

// dotnet run TesteBenchmarkDotNet.csproj -c Release
// dotnet run -p TesteBenchmarkDotNet.csproj -c Release

// dotnet build -c Release
// dotnet C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste Outros\TesteBenchmarkDotNet\bin\Release\net7.0\TesteBenchmarkDotNet.dll

[MemoryDiagnoser]
public class Email
{
    ImmutableArray<(int Index, int Tempo)>  envioDeEmails = Enumerable.Range(0, 200)
        .Select(i => (Index: i, Tempo: Random.Shared.Next(10, 100)))
        .ToImmutableArray();

    [Benchmark]
    public async Task EnviarComParallelForEach() => await Parallel.ForEachAsync(envioDeEmails, async (item, cancel) => await Task.Delay(item.Tempo, cancel));

    [Benchmark]
    public async Task EnviarComParallelForEach100() => await Parallel.ForEachAsync(envioDeEmails, new ParallelOptions { MaxDegreeOfParallelism = 100 }, async (item, cancel) => await Task.Delay(item.Tempo, cancel));

    [Benchmark]
    public async Task EnviarComParallelForEach10() => await Parallel.ForEachAsync(envioDeEmails, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async (item, cancel) => await Task.Delay(item.Tempo, cancel));

    [Benchmark]
    public async Task EnviarComParallelsForCom100() => Parallel.For(0, envioDeEmails.Length, new ParallelOptions { MaxDegreeOfParallelism = 100 }, async (i) => Thread.Sleep(envioDeEmails[i].Tempo));

    [Benchmark]
    public async Task EnviarComParallelsForCom10() => Parallel.For(0, envioDeEmails.Length, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async (i) => Thread.Sleep(envioDeEmails[i].Tempo));

    [Benchmark]
    public async Task EnviarComPChannels()
    {
        var channel = Channel.CreateUnbounded<int>();

        async Task Enviar(ChannelWriter<int> writer, int id, int tempo)
        {
            await Task.Delay(tempo);
            await writer.WriteAsync(id);
        }

        async Task Receber(ChannelReader<int> reader)
        {
            while (await reader.WaitToReadAsync())
            {
                if (reader.TryRead(out var id))
                {
                    // log(id + "email enviado");
                }
            }
        }

        var tarefaReceber = Receber(channel.Reader);
        var processadoTarefas = envioDeEmails.AsParallel().Select(p => Enviar(channel.Writer, p.Index, p.Tempo));
        await Task.WhenAll(processadoTarefas).ContinueWith(_ => channel.Writer.Complete());
        await tarefaReceber;
    }
}
