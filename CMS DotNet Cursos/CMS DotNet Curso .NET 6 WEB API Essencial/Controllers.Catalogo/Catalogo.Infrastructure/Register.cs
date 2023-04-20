using Catalogo.Domain.Interfaces;
using Catalogo.Domain.Models;
using Catalogo.Infrastructure.Context;
using Catalogo.Infrastructure.Context.Interfaces;
using Catalogo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Catalogo.Infrastructure;

public static class Register
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, int timeout = 30)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContextPool<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        // buservicesices.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
        // bserviceses.AddDbContext<AppDbContext>(opt => opt.UseSqlite("DataSource=app.db;Cache=Shared"));
        services.AddScoped<IUnitOfWork, UnitOfWork>();// AddScoped = UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.

        services.AddScoped<IProdutoRepository, ProdutoRepository>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
        services.AddScoped<ICategoriaRepository, CategoriaRepository>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
        services.AddScoped<IAlunoRepository, AlunoRepository>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.

        services.AddIdentity<ApplicationUser, IdentityRole>(options => {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredLength = 3;
        })
           .AddEntityFrameworkStores<AppDbContext>()
           .AddDefaultTokenProviders();

        services.AddAuthentication(x => {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(opt => {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = configuration["TokenConfiguration:Audience"],
                    ValidIssuer = configuration["TokenConfiguration:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]))
                };
            });

        return services;
    }
}
