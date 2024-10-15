using System.Diagnostics;

Console.WriteLine("INI");
try
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();

    var numTasks = 10_000; // 10 / 100 / 1_000 / 10_000 / 100_000 / 1_000_000
    int delaySeconds = 10; // Tempo de espera de cada tarefa
    int progressStep = numTasks / 10; // Agrupar a exibição de progresso a cada 10% da execução
    var tasks = new List<Task>();

    for (int i = 0; i < numTasks; i++)
    {
        // Console.WriteLine($"  -> Task: {i:n0}");
        // Console.WriteLine($"  -> Task: {i.ToString("#,##0")}");
        // Console.WriteLine($"  -> Task: {i.ToString("N0", new System.Globalization.CultureInfo("pt-BR"))}");

        //var task = Task.Run(async () =>  { await Task.Delay(TimeSpan.FromSeconds(delaySeconds)); });
        var task = Task.Delay(TimeSpan.FromSeconds(delaySeconds));  // Criar a tarefa com o delay configurado
        tasks.Add(task);

        if (i % progressStep == 0 && i != 0) Console.WriteLine($"Progresso: {i.ToString("N0")} tarefas criadas ({(i * 100) / numTasks}%)");
        if (i == numTasks-1) Console.WriteLine($"Progresso: {i.ToString("N0")} tarefas criadas (100%)");
    }

    await Task.WhenAll(tasks); // Aguardar a conclusão de todas as tarefas

    stopwatch.Stop();
    Console.WriteLine($"TEMPO: {stopwatch.Elapsed} - {stopwatch.ElapsedMilliseconds} ms");
}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex.Message}");
    Console.WriteLine($"DETAIL: {ex.StackTrace}");
}
finally
{
    Console.WriteLine("FIM");
    Console.ReadKey();
}