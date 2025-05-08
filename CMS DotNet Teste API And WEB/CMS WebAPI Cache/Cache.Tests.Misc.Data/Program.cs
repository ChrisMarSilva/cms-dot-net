using Cache.Domain.Models;
using Cache.Domain.Repository;
using Cache.Infra.Data.Context;
using Cache.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine(new string('-', 60)); 
Console.WriteLine($"[{DateTime.Now.TimeOfDay}]: INICIO");
Console.WriteLine(new string('-', 60));
try
{
    var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
           .AddUserSecrets<Program>(optional: true, reloadOnChange: false)
           .Build();

    var services = new ServiceCollection();
    services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
    services.AddScoped<IPessoaCommandRepository, PessoaCommandRepository>();
    services.AddScoped<IPessoaQueryRepository, PessoaQueryRepository>();

    var serviceProvider = services.BuildServiceProvider();
    await using var scope = serviceProvider.CreateAsyncScope();
    using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var pessoaCommandRepository = scope.ServiceProvider.GetRequiredService<IPessoaCommandRepository>();
    var pessoaQueryRepository = scope.ServiceProvider.GetRequiredService<IPessoaQueryRepository>();

    var pes = new PessoaModel($"Pessoa {DateTime.Now.TimeOfDay}");
    pes.Pendente();
    pes.Aceita();
   
    pessoaCommandRepository.Add(pes);

    await using var transacation = await pessoaCommandRepository.BeginTransactionAsync();
    await pessoaCommandRepository.SaveChangesAsync();
    await transacation.CommitAsync();

    var pessoas = await pessoaQueryRepository.ObterTodos(
        incluiSituacao: true,
        disableTracking: true);

    if (pessoas.Count == 0)
    {
        Console.WriteLine($"[{DateTime.Now.TimeOfDay}]: Sem registro.");
        return;
    }

    foreach (var pessoa in pessoas)
    {
        Console.WriteLine("");
        Console.WriteLine($"[{DateTime.Now.TimeOfDay}]: {pessoa.Id} - {pessoa.Nome} - {pessoa.DtHrRegistro}");

        var situacoes = pessoa.Situacoes
            .OrderBy(x => x.Situacao);

        foreach (var situacao in situacoes)
        {
            Console.WriteLine($"[{DateTime.Now.TimeOfDay}]:    {situacao.IdSituacao} - {(int)situacao.Situacao}-{situacao.Situacao} - {situacao.DtHrRegistro}");
        }
    }

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
