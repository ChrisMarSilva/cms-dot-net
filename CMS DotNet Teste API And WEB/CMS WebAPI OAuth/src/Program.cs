using CMS_WebAPI_OAuth.Data.Context;
using CMS_WebAPI_OAuth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, false)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        //opt.Authority = "https://localhost:7256/";
        //opt.Audience = builder.Configuration["Jwt:Audience"];
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            //RoleClaimType = "role",
            //ValidateActor = true,

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ClockSkew = TimeSpan.FromMinutes(2.0),
        };
    });

builder.Services.AddAuthorization(opt =>
{
    //opt.FallbackPolicy = new AuthorizationPolicyBuilder()
    //  .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    //  .RequireAuthenticatedUser()
    //  .Build();
    opt.AddPolicy("ApiScope", policy  => policy .RequireAuthenticatedUser().RequireClaim("aud", "api"));
    opt.AddPolicy("FrontScope", p => p.RequireAuthenticatedUser().RequireClaim("aud", "front"));
});

builder.Services
    //.AddIdentityCore<ApplicationUser, ApplicationRole>()
    //.AddIdentityApiEndpoints<IdentityUser>()
    //.AddDefaultIdentity<IdentityUser>(opt => opt.SignIn.RequireConfirmedAccount = true)
    .AddIdentity<ApplicationUser, ApplicationRole>(opt => // builder.Services.Configure<IdentityOptions>(opt =>
    {
        // Password settings.
        opt.Password.RequireDigit = true;
        opt.Password.RequireLowercase = true;
        opt.Password.RequireNonAlphanumeric = true;
        opt.Password.RequireUppercase = true;
        opt.Password.RequiredLength = 6;
        opt.Password.RequiredUniqueChars = 1;

        // Default Lockout settings.
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        opt.Lockout.MaxFailedAccessAttempts = 5;
        opt.Lockout.AllowedForNewUsers = true;

        // User settings.
        opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        opt.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    //.AddApiEndpoints()
    .AddDefaultTokenProviders();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();