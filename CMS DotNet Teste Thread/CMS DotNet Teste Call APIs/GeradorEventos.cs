using Bogus;
using Bogus.Extensions.Brazil;
using CMS_DotNet_Teste_Call_APIs.Dtos;
using CMS_DotNet_Teste_Call_APIs.Dtos.Enums;
using CMS_DotNet_Teste_Call_APIs.Dtos.Request;
using System.Text;
using System.Text.Json;

namespace CMS_DotNet_Teste_Call_APIs;

public static class GeradorEventos
{
    private static readonly string UrlAutorizacao = "https://localhost:6001/jdpi/pa/api/v1/autorizacao";
    private const int QtdRecebedores = 100;
    private const int QtdPagadores = 100;

    public static async Task GerarAsync()
    {
        var recebedores = GerarRecebedores();
        var pagadores = GeraPagadores();
        var autorizacoes = GeraAutorizacoes(recebedores.Result, pagadores.Result);

        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(30);

        await Parallel.ForEachAsync(autorizacoes, async (autorizacao, cancellationToken) => 
        {
            var payload = JsonSerializer.Serialize(autorizacao, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            using var content = new StringContent(payload, Encoding.UTF8, "application/json");
            
            using var request = new HttpRequestMessage(HttpMethod.Post, UrlAutorizacao) { Content = content  };
            request.Headers.Add("Chave-idempotencia", Guid.NewGuid().ToString());

            try
            {
                var response = await httpClient.SendAsync(request, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    // Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - OK - {response.StatusCode} - {response.Content}");
                }
                else
                {
                    var errorDetails = await response.Content.ReadAsStringAsync(cancellationToken);
                    Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - NOK - {response.StatusCode} - {errorDetails}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - Erro na autorização {autorizacao.IdRecorrencia} - {ex.Message}");
            }
        });
    }

    private static Task<List<RecebedorDto>> GerarRecebedores()
    {
        var faker = new Faker<RecebedorDto>("pt_BR")
            .RuleFor(r => r.Ispb, f => f.Random.Number(1000000, 99999999)) 
            .RuleFor(r => r.Cnpj, f => long.Parse(f.Company.Cnpj(false)))
            .RuleFor(r => r.Nome, f => $"Empresa - {f.Company.CompanyName()}");

        //return faker.Generate(QtdRecebedores);
        return Task.FromResult(faker.Generate(QtdRecebedores));
    }

    private static Task<List<PagadorDto>> GeraPagadores()
    {
        var faker = new Faker<PagadorDto>("pt_BR")
           .RuleFor(p => p.Ispb, f => f.Random.Number(1000000, 99999999))
           .RuleFor(p => p.TpPessoa, f => TipoOwner.NATURAL_PERSON) 
           .RuleFor(p => p.CpfCnpj, f => long.Parse(f.Person.Cpf(false))) 
           //.RuleFor(r => r.Nome, f => f.Person.FullName)
           .RuleFor(p => p.NrAgencia, f => f.Finance.Account(4)) // f.Random.Number(1, 9999)
           .RuleFor(p => p.TpConta, f => f.PickRandom<TipoConta>()) 
           .RuleFor(p => p.NrConta, f => f.Finance.Account(20)) // f.Random.Number(1, 999999999)
           .RuleFor(p => p.CodMunIbge, f => f.Random.Number(1000, 99999));

        //return faker.Generate(QtdPagadores);
        return Task.FromResult(faker.Generate(QtdPagadores));
    }

    private static List<AutorizacaoRequestDto> GeraAutorizacoes(List<RecebedorDto> recebedores, List<PagadorDto> pagadores)
    {
        var faker = new Faker("pt_BR");
        var contador = 0;

        //var lista = new List<AutorizacaoRequestDto>();
        //foreach (var recebedor in recebedores)
        //{
        //    //Console.WriteLine("");
        //    //Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - {recebedor.Ispb} - {recebedor.Cnpj} - {recebedor.Nome}");

        //    foreach (var pagador in pagadores)
        //    {
        //        //Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - {pagador.Ispb} - {pagador.TpPessoa} - {pagador.CpfCnpj} - {pagador.NrAgencia} - {pagador.NrConta} - {pagador.CodMunIbge}");

        //        contador++;
        //        var dtAtual = DateTime.UtcNow;
        //        var tpFrequencia = faker.PickRandom<TipoFrequencia>();

        //        var autorizacao = new AutorizacaoRequestDto
        //        {
        //            IdRecorrencia = $"RR04358798{dtAtual:yyyyMMdd}{contador:D8}{Random.Shared.Next(1, 999):D3}", // RandomNumberGenerator.GetString("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 11)
        //            TpFrequencia = tpFrequencia,
        //            DtInicialRecorrencia = dtAtual.AddDays(31).Date, // faker.Date.Recent(30, dtAtual)
        //            DtFinalRecorrencia = tpFrequencia switch
        //            {
        //                TipoFrequencia.Semanal => dtAtual.AddDays(31).AddDays(7 * 10).Date,
        //                TipoFrequencia.Mensal => dtAtual.AddDays(31).AddMonths(10).Date,
        //                TipoFrequencia.Trimestral => dtAtual.AddDays(31).AddMonths(3 * 10).Date,
        //                TipoFrequencia.Semestral => dtAtual.AddDays(31).AddMonths(6 * 10).Date,
        //                TipoFrequencia.Anual => dtAtual.AddDays(31).AddYears(10).Date,
        //                _ => dtAtual.AddDays(31).AddDays(1).Date
        //            },
        //            Valor = faker.Finance.Amount(0.01M, 10_000M, 2), // faker.Random.Decimal(0.01M, 10_000M)
        //            Recebedor = recebedor,
        //            Pagador = pagador,
        //            NrContrato = $"{contador:D10}", // faker.Finance.Bic(), // faker.Lorem.Word() 
        //            DescContrato = $"Contrato de nº {contador}", // faker.Commerce.ProductName(), // faker.Lorem.Sentence(5)  // faker.Lorem.Sentence(), // $"Contrato de nº {i}",
        //            DtHrCriacaoRecorrencia = dtAtual,
        //            DtHrCriacaoSolicitacao = dtAtual.AddSeconds(10),
        //            DtHrExpiracaoSolicitacao = dtAtual.AddDays(30) // faker.Date.Recent(30, dtAtual)
        //        };

        //        //Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - {i} - {autorizacao.IdRecorrencia} - {autorizacao.DtInicialRecorrencia} - {autorizacao.DtFinalRecorrencia}- {autorizacao.Valor} - {autorizacao.NrContrato} - {autorizacao.DescContrato} - {autorizacao.DtHrCriacaoRecorrencia} - {autorizacao.DtHrCriacaoSolicitacao} - {autorizacao.DtHrExpiracaoSolicitacao}");

        //        lista.Add(autorizacao);
        //    }

        //    //Console.WriteLine("");
        //}


        var lista = recebedores.SelectMany(recebedor =>
            pagadores.Select(pagador =>
            {
                contador++;
                var baseDate = DateTime.UtcNow;
                var tpFrequencia = faker.PickRandom<TipoFrequencia>();
                var dtInicial = baseDate.AddDays(31);

                var dtFinal = tpFrequencia switch
                {
                    TipoFrequencia.Semanal => dtInicial.AddDays(7 * 10), // 10 semanas
                    TipoFrequencia.Mensal => dtInicial.AddMonths(10), // 10 meses
                    TipoFrequencia.Trimestral => dtInicial.AddMonths(3 * 10), // 10 trimestres
                    TipoFrequencia.Semestral => dtInicial.AddMonths(6 * 10), // 10 semestres
                    TipoFrequencia.Anual => dtInicial.AddYears(10), // 10 anos
                    _ => dtInicial.AddDays(1) // 1 dia
                };

                return new AutorizacaoRequestDto
                {
                    IdRecorrencia = $"RR04358798{baseDate:yyyyMMdd}{contador:D8}{Random.Shared.Next(1, 1000):D3}",
                    TpFrequencia = tpFrequencia,
                    DtInicialRecorrencia = dtInicial.Date,
                    DtFinalRecorrencia = dtFinal.Date,
                    Valor = faker.Finance.Amount(0.01M, 10_000M, 2),
                    Recebedor = recebedor,
                    Pagador = pagador,
                    NrContrato = $"{contador:D10}",
                    DescContrato = $"Contrato de nº {contador}",
                    DtHrCriacaoRecorrencia = baseDate,
                    DtHrCriacaoSolicitacao = baseDate.AddSeconds(10),
                    DtHrExpiracaoSolicitacao = baseDate.AddDays(30)
                };
            })
        ).ToList();

        return lista;
    }
}