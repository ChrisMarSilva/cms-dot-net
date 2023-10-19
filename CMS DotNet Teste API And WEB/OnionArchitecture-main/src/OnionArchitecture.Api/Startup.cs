using DomainServiceLayer;
using DomainServiceLayer.Interface;
using InfrastructureLayer.Context;
using InfrastructureLayer.Repositories;
using InfrastructureLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace OnionArchitecture.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnionArchitecture.Api", Version = "v1" }); });

        var connection = Configuration["SqlConnection:SqlConnectionString"];
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
        // services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection, x => x.MigrationsAssembly("OnionArchitecture.Api")));

        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddTransient<ICustomerService, CustomerService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnionArchitecture.Api v1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
