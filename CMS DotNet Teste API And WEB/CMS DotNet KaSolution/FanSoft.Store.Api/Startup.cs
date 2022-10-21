using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanSoft.Store.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace FanSoft.Store.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(opt => { opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; });
            services.AddSwaggerGen(opt => { opt.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "FansSoft Store API", Version = "v1" }); });
            services.AddDI();
            services.AddResponseCompression(opt => { opt.Providers.Add(new GzipCompressionProvider(new GzipCompressionProviderOptions { Level = System.IO.Compression.CompressionLevel.Optimal })); });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseCors(opt => { opt.AllowAnyHeader(); opt.AllowAnyMethod(); opt.AllowAnyOrigin(); } );
            app.UseResponseCompression();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("/swagger/v1/swagger.json", "FansSoft Store API"); opt.RoutePrefix = "docs"; });
        }
    }
}
