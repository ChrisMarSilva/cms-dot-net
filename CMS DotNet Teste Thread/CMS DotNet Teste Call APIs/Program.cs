using CMS_DotNet_Teste_Call_APIs;

Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - INICIO ");
try
{
    await GeradorEventos.GerarAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - ERRO: {ex.Message}");
}
finally
{
    Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - FIM");
    Console.ReadLine();
}