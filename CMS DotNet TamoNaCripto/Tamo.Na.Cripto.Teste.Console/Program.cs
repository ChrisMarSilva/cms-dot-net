using ExcelDataReader;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

Console.WriteLine("INI"); 
try
{
    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    ExcelDataSetConfiguration conf = new ExcelDataSetConfiguration { ConfigureDataTable = (DataTableReader) => new ExcelDataTableConfiguration { UseHeaderRow = true } };

    var filePath = @"C:\Users\chris\Desktop\CMS DotNet\CMS DotNet TamoNaCripto\Tamo.Na.Cripto.Arquivos\99. cms - export foxbit - Histórico Chris.xlsx";
    using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
    using var reader = ExcelReaderFactory.CreateReader(stream);
    
    var result = reader.AsDataSet(conf);
    if (result.Tables.Count <= 0) 
        return;

    var dataTable = result.Tables[0];
    //Console.WriteLine($"\nRows: {dataTable.Rows.Count} - Columns: {dataTable.Columns.Count}\n");

    var totDeposito = 0.0m;
    var totRetirada = 0.0m;
    for (var idxRow = 0; idxRow < dataTable.Rows.Count; idxRow++) //  foreach (DataRow row in dataTable.Rows)  //for (var i = 0; i < dataTable.Rows.Count; i++) for (var j = 0; j < dataTable.Columns.Count; j++) dataTable.Rows[i][j]
    {
        object?[] row = dataTable.Rows[idxRow].ItemArray;

        //var sn = row[0]?.ToString()?.Trim();
        var datahora = DateTime.ParseExact(row[1]?.ToString()?.Trim()!, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        var descricao = row[2]?.ToString()?.Trim();
        var moeda = row[3]?.ToString()?.Trim();
        var valor = row[4]?.ToString()?.Trim().Replace("R$", "");
        var taxa = row[5]?.ToString()?.Trim().Replace("R$", "");
        var saldo = row[6]?.ToString()?.Trim().Replace("R$", "");
        var dados = row[7]?.ToString()?.Trim();

        if (descricao!.Equals("EarnApplication", StringComparison.OrdinalIgnoreCase))
            continue;

        if (moeda!.Equals("BRL", StringComparison.OrdinalIgnoreCase))
        {
            if (descricao!.Equals("Depósito", StringComparison.OrdinalIgnoreCase))
            {
                totDeposito += Convert.ToDecimal(valor!);
                continue;
            }
            else if (descricao!.Equals("Retirada", StringComparison.OrdinalIgnoreCase))
            {
                totRetirada += Math.Abs(Convert.ToDecimal(valor!));
                continue;
            }
        }

        dados = dados!.Contains("Preço") ? Regex.Replace(dados!, "[^0-9.,]", "").Trim() : dados;
        Console.WriteLine($"{datahora:dd/MM/yyyy HH:mm:ss}\t {descricao}\t {moeda}\t {valor}\t {taxa}\t {saldo}\t {dados}");
    }

    Console.WriteLine("");
    Console.WriteLine($"Total Depósito\t: {totDeposito:C2}");
    Console.WriteLine($"Total Retirada\t: {totRetirada:C2}");
    Console.WriteLine($"Saldo Atual\t: {(totDeposito - totRetirada):C2}");
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