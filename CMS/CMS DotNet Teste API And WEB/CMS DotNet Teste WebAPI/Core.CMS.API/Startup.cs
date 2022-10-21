using Core.CMS.Data.Contexts;
using Core.CMS.Data.Repositories;
using Core.CMS.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.CMS.API
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

            services.AddMvc(option => option.EnableEndpointRouting = false);
            
            // services.AddSwaggerGen(opt => { opt.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "FansSoft Store API", Version = "v1" }); });
            
            //services.AddDI();
            services.AddScoped<BancoDeDadosContext>(); //services.AddDbContext<BancoDeDadosContext>();
            services.AddTransient<IUnitofWork, UnitOfWork>();
            services.AddTransient<IRepository<Empresa>, EmpresaRepository>();
            
            services.AddResponseCompression(opt => { opt.Providers.Add(new GzipCompressionProvider(new GzipCompressionProviderOptions { Level = System.IO.Compression.CompressionLevel.Optimal })); });
            
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseCors(opt => { opt.AllowAnyHeader(); opt.AllowAnyMethod(); opt.AllowAnyOrigin(); });
            app.UseResponseCompression();
            app.UseMvc();
        }
    }
}
