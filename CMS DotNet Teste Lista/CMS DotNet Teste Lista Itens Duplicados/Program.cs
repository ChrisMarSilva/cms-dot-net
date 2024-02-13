using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("INI"); 
try
{
    Console.WriteLine($"");

    var NotaFis = new List<NotaFisDto>(); // NotaFisDto[]?
    NotaFis.Add(new NotaFisDto("0001", DateTime.Now, 10));
    NotaFis.Add(new NotaFisDto("0002", DateTime.Now, 20));
    NotaFis.Add(new NotaFisDto("0003", DateTime.Now, 30));
    NotaFis.Add(new NotaFisDto("0002", DateTime.Now, 30));
    NotaFis.Add(new NotaFisDto("0004", DateTime.Now, 30));
    NotaFis.Add(new NotaFisDto("0001", DateTime.Now, 30));
    NotaFis.Add(new NotaFisDto("0005", DateTime.Now, 30));
    NotaFis.Add(new NotaFisDto("0002", DateTime.Now, 30));
    NotaFis.Add(new NotaFisDto("0006", DateTime.Now, 30));

    if (NotaFis?.Any() ?? false)
    {
        //if (NotaFis.Count() > 30)
        //    Console.WriteLine($"ERRO: Quantidade de grupo nota fiscal do título maior que o permitido.");
        foreach (var item in NotaFis)
            Console.WriteLine($"NumNotaFis: {item.NumNotaFis}");
    }


    Teste1(NotaFis);
    Teste2(NotaFis);
    Teste3(NotaFis);
    Teste4(NotaFis);

    Console.WriteLine($"");
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

static void Teste1(List<NotaFisDto>? NotaFis)
{
    Console.WriteLine($"");

    var query = NotaFis!
      .GroupBy(x => x.NumNotaFis)
      .Where(x => x.Count() > 1)
      .Select(x => new { NumNotaFis = x.Key, Counter = x.Count() }) // Select(x => x.Key)
      .ToList(); // .ToDictionary(x => x.Key, y => y.Count());

    if (query?.Any() ?? false)
        foreach (var item in query)
            //if (item.Counter > 1)
                Console.WriteLine($"NumNotaFis: {item.NumNotaFis} = {item.Counter}");
}

static void Teste2(List<NotaFisDto>? NotaFis)
{
    Console.WriteLine($"");

    var anyDuplicate = NotaFis!
        .GroupBy(x => x.NumNotaFis)
        .Any(g => g.Count() > 1);

    Console.WriteLine($"anyDuplicate: {anyDuplicate}");


    var totalDupeItems = NotaFis!
        .GroupBy(x => x.NumNotaFis)
        .Count(grp => grp.Count() > 1);

    Console.WriteLine($"totalDupeItems: {totalDupeItems}");
}

static void Teste3(List<NotaFisDto>? NotaFis)
{
    Console.WriteLine($"");

    var allUnique = NotaFis!
        .GroupBy(x => x.NumNotaFis)
        .All(g => g.Count() == 1); ;

    Console.WriteLine($"allUnique: {allUnique}");
}

static void Teste4(List<NotaFisDto>? NotaFis)
{
    Console.WriteLine($"");

    var allUnique = NotaFis!
        .GroupBy(x => x.NumNotaFis)
        .Where(g => g.Count() > 1);

    if (allUnique?.Any() ?? false)
        foreach (var item in allUnique)
            Console.WriteLine($"duplicates: {item.Key}");

    Console.WriteLine($"");

    var duplicates = NotaFis!
         .GroupBy(x => x.NumNotaFis)
         .Where(g => g.Count() > 1)
         .Count() > 0;

    if (duplicates)
        Console.WriteLine($"duplicates okkkkkkkk");

    Console.WriteLine($"");

    var duplicates2 = NotaFis!
        .GroupBy(x => x.NumNotaFis)
        .Where(x => x.Skip(1).Any());

    if (duplicates2?.Any() ?? false)
        foreach (var item in duplicates2)
            Console.WriteLine($"duplicates2: {item.Key}");

    Console.WriteLine($"");

    var duplicates3 = NotaFis!
        .GroupBy(x => x.NumNotaFis)
        .Where(x => x.Skip(1).Any())
        .Any();

    if (duplicates3)
        Console.WriteLine($"duplicates3 okkkkkkkk");


    Console.WriteLine($"");

    var duplicates4 = NotaFis!
        .GroupBy(x => x.NumNotaFis)
        .Where(x => x.Skip(1).Any())
        .ToArray();

    if (duplicates4.Any())
        foreach (var item in duplicates4)
            Console.WriteLine($"duplicates4: {item.Key} {item.Count()}");

    Console.WriteLine($"");

    var duplicates5 = NotaFis!
        .GroupBy(x => x.NumNotaFis)
        .SelectMany(g => g.Skip(1));

    //Console.WriteLine("Duplicate elements are: " + String.Join(",", duplicates5));

    if (duplicates5.Any())
        foreach (var item in duplicates5)
            Console.WriteLine($"duplicates5: {item.NumNotaFis}");


    Console.WriteLine($"");

    var duplicates6 = NotaFis!
        .GroupBy(x => x.NumNotaFis) 
        .Where(g => g.Skip(1).Any()) 
        .SelectMany(g => g);

    //Console.WriteLine("Duplicate elements are: " + String.Join(",", duplicates6));

    if (duplicates6.Any())
        foreach (var item in duplicates6)
            Console.WriteLine($"duplicates6: {item.NumNotaFis}");

    Console.WriteLine($"");


    Dictionary<string, int> duplicates7 = NotaFis!
        .GroupBy(x => x.NumNotaFis)
        .Where(g => g.Count() > 1)
        .ToDictionary(x => x.Key, x => x.Count());


    if (duplicates7.Any())
        foreach (var item in duplicates7)
            Console.WriteLine($"duplicates7: {item.Key}");


    Console.WriteLine($"");

    var hashset = new HashSet<string>();
    var duplicates8 = NotaFis!.Where(x => !hashset.Add(x.NumNotaFis));

    if (duplicates8.Any())
        foreach (var item in duplicates8)
            Console.WriteLine($"duplicates8: {item.NumNotaFis}");

}

public record NotaFisDto(string? NumNotaFis, DateTime? DtEmsNotaFis, decimal? VlrNotaFis);
