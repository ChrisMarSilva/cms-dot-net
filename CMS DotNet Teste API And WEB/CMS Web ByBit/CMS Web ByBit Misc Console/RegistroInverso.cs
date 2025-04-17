using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CMS_Web_ByBit_Misc_Console;

public static class RegistroInverso
{
    public static void Processar()
    {
        var path = @"C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste API And WEB\CMS Web ByBit\CMS Web ByBit Misc Console\Arquivos\";
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, Delimiter = ",", TrimOptions = TrimOptions.Trim };

        // Ler os registros do CSV original
        using var reader = new StreamReader($"{path}BybitFull.csv");
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<RegistroMap>();
        var registros = csv.GetRecords<Registro>().ToList();

        registros.Reverse();

        // Escrever em novo arquivo
        using var writer = new StreamWriter($"{path}BybitFull2.csv");
        using var csvWriter = new CsvWriter(writer, config);

        csvWriter.Context.RegisterClassMap<RegistroMap>();
        csvWriter.WriteHeader<Registro>();
        csvWriter.NextRecord();
        csvWriter.WriteRecords(registros);

        Console.WriteLine("Arquivo invertido salvo com sucesso");
    }
}
