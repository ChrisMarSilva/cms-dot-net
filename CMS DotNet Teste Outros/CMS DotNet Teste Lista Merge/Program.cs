using System.Collections.Generic;
using Teste_com_Lista.Models;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("INI"); 
try
{

    // ===========================================================================================================
    // ===========================================================================================================

    var contasBase = new HashSet<PagadorContaModel>();

    //contasBase.Add(new PagadorContaModel(2024020500001, "A", 1234, "CC", 10040, DateTime.Now, null));
    //contasBase.Add(new PagadorContaModel(2024020500001, "A", 1234, "CC", 10050, null, null));
    //contasBase.Add(new PagadorContaModel(2024020500001, "A", 1234, "CC", 10060, null, null));

    Console.WriteLine($"");
    Console.WriteLine($"  CONTAS DA BASE DE DADOS");
    //if (contasBase?.Any() ?? false)
        foreach (var conta in contasBase)
            Console.WriteLine($"     {conta}");

    // ===========================================================================================================
    // ===========================================================================================================

    var contasNovas = new HashSet<PagadorContaModel>();

    //contasNovas.Add(new PagadorContaModel(2024020500001, "A", 1234, "CC", 10040, DateTime.Now, "E"));
    //contasNovas.Add(new PagadorContaModel(2024020500001, "A", 1234, "CC", 10050, null, "E"));
    //contasNovas.Add(new PagadorContaModel(2024020500001, "A", 1234, "CC", 10060, null, "E"));
    //contasNovas.Add(new PagadorContaModel(2024020500001, "A", 1234, "CC", 10070, DateTime.Now, "I"));

    Console.WriteLine($"");
    Console.WriteLine($"  CONTAS PARA SEREM ALTERADA OU EXLUIDAS");
    //if (contasNovas?.Any() ?? false)
        foreach (var conta in contasNovas)
            Console.WriteLine($"     {conta}");

    // ===========================================================================================================
    // ===========================================================================================================

    var contasValidadas = new HashSet<PagadorContaModel>();

    // SOMENTE ADD CONTAS DA BASE QUE NAO FORAM EXCLUIDAS

    if (contasBase?.Any() ?? false)
    //{
        //var contasBaseFiltradas = contasBase
        //      .Where(x => (x.IndrManutCtCliPagdr ?? "I") != "E")
        //      .ToList(); // Converta para lista para evitar chamadas repetitivas em um loop

        //foreach (var conta in contasBaseFiltradas)
        //if ((conta.IndrManutCtCliPagdr ?? "I") != "E")
        foreach (var conta in contasBase.Where(c => (c.IndrManutCtCliPagdr ?? "I") != "E"))
            contasValidadas.Add(new PagadorContaModel(conta.NumCtrlReq, conta.TpAgCliPagdr, conta.AgCliPagdr, conta.TpCtCliPagdr, conta.CtCliPagdr, conta.DtAdesCliPagdrDda?.Date, conta.IndrManutCtCliPagdr));
    //}

    // SOMENTE ADD AS NOVAS CONTAS NAO SERAO EXCLUIDAS

    if (contasNovas?.Any() ?? false)
    //{
        //    var contasNovasFiltradas = contasNovas
        //          .Where(x => (x.IndrManutCtCliPagdr ?? "I") != "E")
        //          .ToList(); // Converta para lista para evitar chamadas repetitivas em um loop

        //foreach (var conta in contasNovasFiltradas)
        //if ((conta.IndrManutCtCliPagdr ?? "I") != "E")
        foreach (var conta in contasNovas.Where(x => (x.IndrManutCtCliPagdr ?? "I") != "E"))
            contasValidadas.Add(new PagadorContaModel(conta.NumCtrlReq, conta.TpAgCliPagdr, conta.AgCliPagdr, conta.TpCtCliPagdr, conta.CtCliPagdr, conta.DtAdesCliPagdrDda?.Date, conta.IndrManutCtCliPagdr));
    //}

    // REMOVER AS NOVAS CONTAS EXCLUIDAS DAS CONTAS PARA VALIDAR

    //if (contasValidadas?.Any() ?? false)
    //{
    //    //foreach (var conta in contasValidadas)
    //    //    if (contasNovas!.Any(x => x.AgCliPagdr == conta.AgCliPagdr && x.TpAgCliPagdr == conta.TpAgCliPagdr && x.CtCliPagdr == conta.CtCliPagdr && x.IndrManutCtCliPagdr == "E"))
    //    //        contasValidadas.Remove(conta);

    //    //contasValidadas.RemoveWhere(conta =>
    //    //    contasNovas!.Any(x => x.AgCliPagdr == conta.AgCliPagdr &&
    //    //                    x.TpAgCliPagdr == conta.TpAgCliPagdr &&
    //    //                    x.CtCliPagdr == conta.CtCliPagdr &&
    //    //                    x.IndrManutCtCliPagdr == "E"));

    //    contasValidadas.RemoveWhere(conta =>
    //    {
    //        return contasNovas!.Any(x =>
    //            x.AgCliPagdr == conta.AgCliPagdr &&
    //            x.TpAgCliPagdr == conta.TpAgCliPagdr &&
    //            x.CtCliPagdr == conta.CtCliPagdr &&
    //            string.Equals(x.IndrManutCtCliPagdr, "E", StringComparison.OrdinalIgnoreCase));
    //    });
    //}

    contasValidadas?.RemoveWhere(conta =>
        contasNovas!.Any(x =>
            x.AgCliPagdr == conta.AgCliPagdr &&
            x.TpAgCliPagdr == conta.TpAgCliPagdr &&
            x.CtCliPagdr == conta.CtCliPagdr &&
            string.Equals(x.IndrManutCtCliPagdr, "E", StringComparison.OrdinalIgnoreCase)));


    Console.WriteLine($"");
    Console.WriteLine($"  CONTAS PARA SEREM VALIDADADS");
    //if (contasValidadas?.Any() ?? false)
        foreach (var conta in contasValidadas)
            Console.WriteLine($"     {conta}");

    // ===========================================================================================================
    // ===========================================================================================================

    Console.WriteLine($"");
    if (contasValidadas?.Any() ?? false)
    {
        var anyNumNotaFisDuplicate = contasValidadas
            .GroupBy(x => new { x.AgCliPagdr, x.TpAgCliPagdr, x.CtCliPagdr, x.IndrManutCtCliPagdr })
            .Any(g => g.Count() > 1);

        if (anyNumNotaFisDuplicate)
            Console.WriteLine($"  ==> VALIDAÇÃO: Existe conta duplicada"); 

        if (contasValidadas?.All(conta => conta?.DtAdesCliPagdrDda == null) ?? true)
            Console.WriteLine($"  ==> VALIDAÇÃO: Pelo menos um campo \"dtAdesCliPagdrDda\" é obrigatório.");
    }
    else
    {
        Console.WriteLine($"  ==> VALIDAÇÃO: Pelo menos uma conta deve permancer ativa..");
    }

    // ===========================================================================================================
    // ===========================================================================================================
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


