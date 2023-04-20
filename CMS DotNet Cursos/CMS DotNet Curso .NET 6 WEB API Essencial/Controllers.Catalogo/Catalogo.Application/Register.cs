using AutoMapper;
using Catalogo.Application.Interfaces;
using Catalogo.Application.Mappings;
using Catalogo.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalogo.Application;

public static class Register
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, int timeout = 30)
    {
        services.AddScoped<IAutorizaService, AutorizaService>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
        services.AddScoped<IProdutoService, ProdutoService>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
        services.AddScoped<ICategoriaService, CategoriaService>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
        services.AddScoped<IAlunoService, AlunoService>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
        services.AddTransient<IMeuServico, MeuServico>(); // VariasVezes - registra um serviço que é criado cada vez que é solicitado

        var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper); // AddSingleton = UmVezQdoSobeAPI - registra um serviço que é criado uma única vez durante todo o ciclo de vida do aplicativo

        return services;
    }
}
