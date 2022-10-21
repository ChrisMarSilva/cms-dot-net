using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FanSoft.Store.DI;
using FanSoft.Store.UI.Infra;
//using Microsoft.Extensions.Logging;

namespace FanSoft.Store.UI
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<ILogCustom, SeriLogFile>();

            services.AddDI();  // services.AddScoped<StoreDataContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    options.DefaultSignInScheme =
                        options.DefaultChallengeScheme =
                            CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => options.LoginPath = "/auth/signin");

        }

        // public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logFact)
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // logFact.AddDebug(LogLevel.None).AddStackify();
                // logFact.AddFile("logs/myapp-{Date}.txt");
                app.UseExceptionHandler("/Error");
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc((routConfig) =>
            {
                routConfig.MapRoute("edit", "{Controller}/Editar/{id}", new { action = "AddEdit" });
                routConfig.MapRoute("add", "{Controller}/Adicionar", new { action = "AddEdit" });
                routConfig.MapRoute("default", "{Controller=home}/{action=index}/{id?}");
            });

        }

    }
}
