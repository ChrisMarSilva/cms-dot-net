using CMS_Web_ByBit_Misc_Console;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - INICIO ");
try
{
    // RegistroInverso.Processar();

    var path = @"C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste API And WEB\CMS Web ByBit\CMS Web ByBit Misc Console\Arquivos\";
    var file = $"{path}BybitFull.csv"; // Bybit // BybitFull // Registros de fundos	2024-01-01 a 2024-12-31	2025-04-17 09:12:08
    using var reader = new StreamReader(file);

    var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, Delimiter = ",", TrimOptions = TrimOptions.Trim }; 
    using var csv = new CsvReader(reader, config);
    csv.Context.RegisterClassMap<RegistroMap>();

    var registros = csv
        .GetRecords<Registro>()
        .Where(r => r.Contract.Contains("AERO"))
        .OrderBy(r => r.TimeUTC) // OrderBy // OrderByDescending
        //.GroupBy(r => r.Contract)
        .ToList();

    //.Where(r => r.Contract == "ETHUSDT")
    //.Where(r => r.Contract == "AEROUSDT")
    //.Where(r => r.Contract == "AEROUSDT" && r.Type == "TRADE" && (r.Direction == "BUY" || r.Direction == "SELL"))
    //.Where(r => r.Contract.Contains("ETH"))
    //.Where(r => new[] { "TRADE", "TRANSFER" }.Contains(r.Type))

    // registros.Reverse();

    var totalBuyQty = 0M;
    var totalSellQty = 0M;

    //foreach (var grupo in registros)
    //{
    //    Console.WriteLine($"--- Contract: {grupo.Key} ---");

    //foreach (var r in grupo)
    foreach (var r in registros)
    {
            Console.WriteLine(
                $"{r.TimeUTC:yyyy-MM-dd HH:mm:ss} | " + // Data e Hora (UTC)
                $"{r.Direction} | " + // BUY //SELL
                $"{r.Quantity} | " + // Quantidade
                $"{r.FilledPrice} | " + // Preço Executado	
                $"{r.FeePaid} | " + // Taxa Paga
                $"{r.Change} | " + // Mudança no saldo
                $"{r.WalletBalance}"); // Saldo

            if (r.Direction == "BUY") totalBuyQty += (r.Quantity - Math.Abs(r.FeePaid));
            if (r.Direction == "SELL") totalSellQty += (r.Quantity - Math.Abs(r.FeePaid));
        }
    //    Console.WriteLine();

    //    // var totalBuyQty = grupo.Where(r => r.Direction == "BUY").Sum(r => r.Quantity - r.FeePaid); // .Sum(r => r.Quantity);
    //    // var totalSellQty = grupo.Where(r => r.Direction == "SELL").Sum(r => r.Quantity - r.FeePaid); // .Sum(r => r.Quantity);
    //    Console.WriteLine($"Quantity: {totalSellQty - totalBuyQty:F6}");

    //    //var totalBuyFlow = grupo.Where(r => r.Direction == "BUY").Sum(r => r.CashFlow);
    //    //var totalSellFlow = grupo.Where(r => r.Direction == "SELL").Sum(r => r.CashFlow);
    //    //Console.WriteLine($"CashFlow: {totalSellFlow + totalBuyFlow:F2}");

    //    Console.WriteLine();
    //}

    /*

    ETHUSDT
        Quantity    BUY:  -129315,098817    SELL: 143361,841696     Balance:     272676,940513
        CashFlow    BUY:  -129315,10        SELL: 143361,84         Balance:     14046,74
    AEROUSDT
        Quantity    BUY:  -795,266602       SELL: 202,375770        Balance:     997,642372
        CashFlow    BUY:  -795,27           SELL: 202,38            Balance:     -592,89


        ETH     - 8.9658            - 28,918	- $3,225.38 / unit
        FOXY	- 2,875,980.3012	- 37,923	- $0.013 / unit
        SOL	    - 42.7590	        - 9,306	    - $217.64 / unit
        ALGO	- 11,051.4295	    - 4,715	    - $0.43 / unit
        LINK	- 167.5707	        - 4,146	    - $24.74 / unit
        DOT	    - 461.4644	        - 4,395 	- $9.52 / unit
        FET	    - 2,265.0286	    - 4,342	    - $1.92 / unit
        INJ	    - 97.1933	        - 1,996	    - $20.54 / unit
        XAVA	- 1,758.4399	    - 1,000 	- $0.57 / unit
        AERO	- 653.7496	        - 1,000	    - $1.53 / unit
        $84,549

        ETH  -         8,9658 - R$ 177.982,00 - R$ 19.851,12 / unit
        FOXY - 2.875.980,3012 - R$ 221.412,00 - R$     0,077 / unit
        SOL  -        42,7590 - R$  56.784,00 - R$  1.328,00 / unit
        ALGO -    11.051,4295 - R$  28.439,00 - R$      2,57 / unit
        DOT  -       461,4644 - R$  26.482,00 - R$     57,39 / unit
        FET  -     2.265,0286 - R$  26.166,00 - R$     11,55 / unit
        LINK -       167,5707 - R$  25.968,00 - R$    154,97 / unit
        INJ  -        97,1933 - R$  12.369,00 - R$    127,27 / unit
        XAVA -     1.758,4399 - R$   6.196,00 - R$      3,52 / unit
        AERO -       653,7496 - R$   6.196,00 - R$      9,48 / unit

        R$ 588.588
    
     */
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