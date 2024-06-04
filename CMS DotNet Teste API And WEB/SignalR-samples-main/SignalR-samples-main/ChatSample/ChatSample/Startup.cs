using ChatSample.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatSample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(setup => setup.AddPolicy("signalr", configure =>
            {
                configure.SetIsOriginAllowed(isOriginAllowed => true)
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors("signalr");
            app.UseFileServer();
            app.UseRouting();
            app.UseEndpoints(endpoints => { 
                endpoints.MapHub<ChatHub>("/chat");
                //endpoints.MapControllers();
                //endpoints.MapControllerRoute(name: "default", pattern: "{controller=Piloto}/{action=Index}/{id?}");
            });
        }
    }
}
