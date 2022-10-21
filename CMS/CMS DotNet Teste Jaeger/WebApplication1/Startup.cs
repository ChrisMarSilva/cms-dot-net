using Jaeger;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTracing;
using OpenTracing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
            });

            services.AddOpenTracing();

            services.AddSingleton<ITracer>(serviceProvider =>
            {
                var serviceName = serviceProvider.GetRequiredService<IWebHostEnvironment>().ApplicationName;
                // var serviceName = serviceProvider.GetRequiredService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>().ApplicationName;
                // var serviceName = Assembly.GetEntryAssembly().GetName().Name;

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                Jaeger.Configuration.SenderConfiguration.DefaultSenderResolver = new SenderResolver(loggerFactory)
                    .RegisterSenderFactory<ThriftSenderFactory>();

                var tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(new ConstSampler(true))
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            //services.AddSingleton<ITracer>(serviceProvider =>
            //{
            //    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //    var senderConfig = new SenderConfiguration(loggerFactory)
            //     .WithAgentHost(Environment.GetEnvironmentVariable("JAEGER_AGENT_HOST"))
            //     .WithAgentPort(Convert.ToInt32(Environment.GetEnvironmentVariable("JAEGER_AGENT_PORT")));

            //    SenderConfiguration.DefaultSenderResolver = new SenderResolver(loggerFactory)
            //        .RegisterSenderFactory<ThriftSenderFactory>();

            //    var config = Configuration.FromEnv(loggerFactory);

            //    var samplerConfiguration = new SamplerConfiguration(loggerFactory)
            //        .WithType(ConstSampler.Type)
            //        .WithParam(1);

            //    var reporterConfiguration = new ReporterConfiguration(loggerFactory)
            //        .WithSender(senderConfig)
            //        .WithLogSpans(true);

            //    var tracer = config
            //        .WithSampler(samplerConfiguration)
            //        .WithReporter(reporterConfiguration)
            //        .GetTracer();

            //    GlobalTracer.Register(tracer);

            //    return tracer;
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
