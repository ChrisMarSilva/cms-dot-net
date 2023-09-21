using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VShop.CartApi.Context;
using VShop.CartApi.Repositories;
using VShop.CartApi.Repositories.Interfaces;
using VShop.CartApi.Services;
using VShop.CartApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VShop.CartApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"'Bearer' [space] seu token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
         {
            new OpenApiSecurityScheme
            {
               Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
               Scheme = "oauth2",
               Name = "Bearer",
               In= ParameterLocation.Header
            },
            new List<string> ()
         }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); // AddDbContextPool // AddDbContext

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options => { options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });

// Repository
builder.Services.AddScoped<ICartRepository, CartRepository>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.

// Service
builder.Services.AddScoped<ICartService, CartService>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["VShop.IdentityServer:ApplicationUrl"];
        options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "vshop");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
