﻿using Catalogo.API.Filters;
using Catalogo.API.Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO.Compression;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Catalogo.API.Configuration;

// AddTransient = VariasVezes           - registra um serviço que é criado cada vez que é solicitado
// AddScoped    = UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
// AddSingleton = UmVezQdoSobeAPI       - registra um serviço que é criado uma única vez durante todo o ciclo de vida do aplicativo

public static class DependencyInjection // Configure
{
    //public static IServiceCollection AddContexts(this IServiceCollection services, IConfiguration configuration)
    //{
    //    // AddContexts // AddPersistence
    //    var connectionString = configuration.GetConnectionString("DefaultConnection");
    //    services.AddDbContextPool<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    //    // buservicesices.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
    //    // bserviceses.AddDbContext<AppDbContext>(opt => opt.UseSqlite("DataSource=app.db;Cache=Shared"));
    //    services.AddScoped<IUnitOfWork, UnitOfWork>();// AddScoped = UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
    //    return services;
    //}

    //public static IServiceCollection AddMappers(this IServiceCollection services)
    //{
    //    var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
    //    IMapper mapper = mappingConfig.CreateMapper();
    //    services.AddSingleton(mapper); // AddSingleton = UmVezQdoSobeAPI - registra um serviço que é criado uma única vez durante todo o ciclo de vida do aplicativo

    //    //IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
    //    //builder.Services.AddSingleton(mapper); // AddSingleton = UmVezQdoSobeAPI - registra um serviço que é criado uma única vez durante todo o ciclo de vida do aplicativo
    //    //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    //    return services;
    //}

    //public static IServiceCollection AddRepositories(this IServiceCollection services)
    //{
    //    services.AddScoped<IProdutoRepository, ProdutoRepository>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
    //    services.AddScoped<ICategoriaRepository, CategoriaRepository>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
    //    services.AddScoped<IAlunoRepository, AlunoRepository>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.

    //    return services;
    //}

    //public static IServiceCollection AddServices(this IServiceCollection services)
    //{
    //    services.AddScoped<IAutorizaService, AutorizaService>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
    //    services.AddScoped<IProdutoService, ProdutoService>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
    //    services.AddScoped<ICategoriaService, CategoriaService>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
    //    services.AddScoped<IAlunoService, AlunoService>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
    //    services.AddTransient<IMeuServico, MeuServico>(); // VariasVezes - registra um serviço que é criado cada vez que é solicitado

    //    return services;
    //}

    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddScoped<ApiLoggingFilter>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.

        return services;
    }

    public static IServiceCollection AddCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(opt =>
        {
            opt.EnableForHttps = true;
            opt.Providers.Add<BrotliCompressionProvider>();
            opt.Providers.Add<GzipCompressionProvider>();
        });
        services.Configure<BrotliCompressionProviderOptions>(opt => { opt.Level = CompressionLevel.Fastest; });
        services.Configure<GzipCompressionProviderOptions>(opt => { opt.Level = CompressionLevel.Fastest; }); // SmallestSize

        return services;
    }

    public static IServiceCollection AddControllersWithJson(this IServiceCollection services)
    {
        services.AddControllers()
             .AddJsonOptions(opt =>
             {
                 opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                 opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
                 opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                 opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                 opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                 opt.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
             })
             .AddOData(opt => opt.Expand().Select().Count().OrderBy().Filter());

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        // services.AddSwagger();

        services.AddSwaggerGen(opt =>
        {

            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalogo.API", Version = "v1", Description = "Catalogo.API", TermsOfService = new Uri("https://www.google.com.br/"), Contact = new OpenApiContact() { Name = "CMS", Email = "cms@gmail.com", Url = new Uri("https://www.google.com.br/"), }, });
            opt.SwaggerDoc("v2", new OpenApiInfo { Title = "Catalogo.API", Version = "v2" });

            // c.EnableAnnotations();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);

            // opt.OperationFilter<SecurityRequirementsOperationFilter>();
            // var xmlApiPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml");
            // opt.IncludeXmlComments(xmlApiPath);

            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"Enter 'Bearer' [space] and your token!",  // Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer'[space].Example: \'Bearer 12345abcdef\'",
                Name = "Authorization",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement { {
                    new OpenApiSecurityScheme{ Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "Bearer"}, Scheme = "oauth2", Name = "Bearer", In= ParameterLocation.Header},
                    new List<string> ()
                }
            });
        });

        return services;
    }

    //public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    //        options.Password.RequireNonAlphanumeric = false;
    //        options.Password.RequireDigit = false;
    //        options.Password.RequireUppercase = false;
    //        options.Password.RequireLowercase = false;
    //        options.Password.RequiredLength = 3;
    //    })
    //        .AddEntityFrameworkStores<AppDbContext>()
    //        .AddDefaultTokenProviders();

    //    services.AddAuthentication(x => {
    //        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //    })
    //        .AddJwtBearer(opt => {
    //            opt.TokenValidationParameters = new TokenValidationParameters {
    //                ValidateActor = true,
    //                ValidateIssuer = true,
    //                ValidateAudience = true,
    //                ValidateLifetime = true,
    //                ClockSkew = TimeSpan.Zero,
    //                ValidAudience = configuration["TokenConfiguration:Audience"],
    //                ValidIssuer = configuration["TokenConfiguration:Issuer"],
    //                ValidateIssuerSigningKey = true,
    //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]))
    //            };
    //        });

    //    //services.AddAuthentication("Bearer")
    //    //    .AddJwtBearer("Bearer", options =>
    //    //    {
    //    //        options.Authority = "https://localhost:4435/";
    //    //        options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
    //    //    });

    //    //services.AddAuthorization(options =>
    //    //{
    //    //    options.AddPolicy("ApiScope", policy =>
    //    //    {
    //    //        policy.RequireAuthenticatedUser();
    //    //        policy.RequireClaim("scope", "geek_shopping");
    //    //    });
    //    //});

    //    //services.AddAuthorization(options =>
    //    //{
    //    //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    //    //      .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    //    //      .RequireAuthenticatedUser()
    //    //      .Build();
    //    //    options.AddPolicy("EmployeePolicy", p => p.RequireAuthenticatedUser().RequireClaim("EmployeeCode"));
    //    //    options.AddPolicy("Employee005Policy", p => p.RequireAuthenticatedUser().RequireClaim("EmployeeCode", "005"));
    //    //    options.AddPolicy("CpfPolicy", p => p.RequireAuthenticatedUser().RequireClaim("Cpf"));
    //    //});

    //    return services;
    //}

    public static IServiceCollection AddCorsLocal(this IServiceCollection services)
    {
        services.AddCors(policyBuilder => policyBuilder.AddPolicy("AllowAllOrigins", builder => { builder.AllowAnyOrigin(); builder.AllowAnyHeader(); builder.AllowAnyMethod(); }))
            .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddScoped<IUrlHelper>(x => x.GetRequiredService<IUrlHelperFactory>().GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext))
            .AddMvcCore()
            .AddMvcOptions(opt => { opt.Filters.Add<ValidateModelFilter>(-9999); opt.Filters.Add(new CacheControlFilter()); })
            //.AddAuthorization()
            .AddApiExplorer()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNamingPolicy = null; // JsonNamingPolicy.CamelCase //  null; // new CustomPropertyNamingPolicy();
                // opt.JsonSerializerOptions.IgnoreNullValues = true;
                opt.JsonSerializerOptions.WriteIndented = false;
                opt.JsonSerializerOptions.AllowTrailingCommas = false;
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
            })
            .AddDataAnnotations();

        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ReportApiVersions = true;
            // opt.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            opt.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });

        services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV"; // "VVV"
            opt.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        return app;
    }

    public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();

            //app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            //app.UseSwagger(c => {
            //    c.PreSerializeFilters.Add((document, request) => {
            //        var paths = document.Paths.ToDictionary(item => item.Key.ToLowerInvariant(), item => item.Value);
            //        document.Paths.Clear();
            //        foreach (var pathItem in paths) {
            //            document.Paths.Add(pathItem.Key, pathItem.Value);
            //        }
            //    });
            //});

            app.UseSwaggerUI(opt =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(opt.RoutePrefix) ? "." : "..";
                opt.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Catalogo.API - V1");
                opt.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v2/swagger.json", "Catalogo.API - V2");
                // opt.RoutePrefix = string.Empty; // COMETAR ESSA LINHA EM DEBUG
                // c.SupportedSubmitMethods(new Swashbuckle.AspNetCore.SwaggerUI.SubmitMethod[] { }); // para retirar os teste dos metodos
            });

            //app.UseReDoc(c => {
            //    c.RoutePrefix = "docs";
            //    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
            //    c.SpecUrl($"{swaggerJsonBasePath}/swagger/v1/swagger.json");
            //    c.DocumentTitle = "Catalogo.API - V1";
            //    c.EnableUntrustedSpec();//Se ativada, a especificação é considerada não confiável e todo o HTML / remarcação é higienizado para impedir o XSS. Desabilitado por padrão por motivos de desempenho.Ative esta opção se você trabalha com dados de usuários não confiáveis!
            //    c.ScrollYOffset(10); //Se definido, especifica um deslocamento de rolagem vertical            
            //    c.HideHostname(); //Se definido, o protocolo e o nome do host não são mostrados na definição da operação.
            //    c.HideDownloadButton(); //Não mostre o botão de especificação "Download". Isso não torna sua especificação privada , apenas oculta o botão.
            //    c.ExpandResponses("200,201"); //especifique quais respostas expandir por padrão por códigos de resposta.
            //    c.RequiredPropsFirst(); // mostra as propriedades necessárias primeiro ordenadas na mesma ordem que na requiredmatriz.
            //    c.NoAutoAuth(); //não injete a seção Autenticação automaticamente.
            //    c.PathInMiddlePanel(); //mostre o link do caminho e o verbo HTTP no painel do meio em vez do correto.
            //    c.HideLoading(); //não mostra carregando animação. Útil para documentos pequenos.
            //    c.NativeScrollbars(); // use a barra de rolagem nativa para sidemenu em vez da rolagem perfeita (otimização do desempenho da rolagem para grandes especificações).
            //    c.DisableSearch();// desabilite a indexação e a caixa de pesquisa.
            //      //c.OnlyRequiredInSamples(); //mostra apenas os campos obrigatórios nas amostras de solicitação.
            //    c.SortPropsAlphabetically(); // classifique as propriedades em ordem alfabética.
            //});
        }

        return app;
    }

    public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder app)
    {
        //app.UseCors(opt => {
        //    //opt.AllowAnyOrigin();
        //    opt.WithOrigins("*");
        //    opt.AllowAnyMethod();
        //    opt.AllowAnyHeader();
        //    // opt.AllowCredentials(); 
        //});

        app.UseCors("AllowAllOrigins");

        return app;
    }

    public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/status", new HealthCheckOptions()
        {
            ResponseWriter = async (context, report) =>
            {
                var result = JsonConvert.SerializeObject(new
                {
                    statusApplication = report.Status.ToString(),
                    healthChecks = report.Entries.Select(e => new
                    {
                        check = e.Key,
                        status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                        description = e.Value.Description,
                        exception = e.Value.Exception,
                    })
                },
                Formatting.None,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.None,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                });
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(result);
            }
        });
        return app;
    }

    public static ILoggingBuilder AddCustomLogger(this ILoggingBuilder logging)
    {
        logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration { LogLevel = LogLevel.Information }));

        return logging;
    }
}
