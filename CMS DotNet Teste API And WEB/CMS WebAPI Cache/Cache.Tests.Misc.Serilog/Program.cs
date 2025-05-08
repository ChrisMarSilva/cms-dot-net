Console.WriteLine(new string('-', 60));
Console.WriteLine($"[{DateTime.Now.TimeOfDay}]: INICIO");
Console.WriteLine(new string('-', 60));
try
{

    Console.WriteLine("");
}
catch (Exception ex)
{
    Console.WriteLine(new string('-', 60));
    Console.WriteLine($"[{DateTime.Now.TimeOfDay}]: ERRO -> {ex.Message}");
    Console.WriteLine(new string('-', 60));
    throw;
}
finally
{
    Console.WriteLine(new string('-', 60));
    Console.WriteLine($"[{DateTime.Now.TimeOfDay}]: FIM");
    Console.WriteLine(new string('-', 60));
    Console.ReadKey();
}