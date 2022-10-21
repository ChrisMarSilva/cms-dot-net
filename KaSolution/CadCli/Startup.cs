using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadCli.Controllers;
using CadCli.Data;
using CadCli.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadCli
{
    public class Startup
    {

        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // services.AddSingleton(); // Objeto unico em toda aplicacao
            // services.AddScoped(); // é unico na requisição
            // services.AddTransient(); / ele sempre gerar um novo objeto

            // services.AddSingleton<IConfigSistemaService, ConfigSistemaService>();
            services.AddSingleton<IConfigSistemaService, ConfigSistemaServiceWeb>();
            services.AddScoped<CadCliDataContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.Run(async (context) => 
            //{
            //    await context.Response.WriteAsync("Hello World - 2!");
            //});

            app.Use(async (context, next) =>
            {
                // Tratamento para receber a requisicao
                await next();

                //tratametno da respota
                //if (context.Response.StatusCode == 404)
                //{
                //    await context.Response.WriteAsync("Ops, Deu Ruim ...");
                //}
                //else
                //{
                //    await context.Response.WriteAsync("Passei na Volta ...");
                //}

                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Errors/E404";
                    await next();
                }

                await context.Response.WriteAsync("Passei na Volta ...");

            });

            app.UseMvcWithDefaultRoute();

            // app.Run(async (context) => 
            //{
            //    await context.Response.WriteAsync("Respondendo ...");
            //});

        }
    }
}
