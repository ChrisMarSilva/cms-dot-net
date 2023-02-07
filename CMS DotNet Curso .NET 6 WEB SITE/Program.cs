using IWantApp.Endpoints.Categories;
using Microsoft.AspNetCore.Identity;
using IWantApp.Infra.Data;
using IWantApp.Endpoints.Employees;

var builder = WebApplication.CreateBuilder(args);

// builder.Configuration["ConnectionStrings.IWantDb"]
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("IWantDb"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 3;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<QueryAllUsersWithClaimName>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapGet("/config1", (IConfiguration configuration) =>
//{
//    return Results.Ok(configuration["ConnectionStrings:IWantDb"]);
//});

// app.MapGet("/config2", (IConfiguration configuration) => Results.Ok(configuration.GetSection("ConnectionStrings").GetValue<string>("IWantDb")));

//app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle);
//app.MapMethods(CategoryGetAll.Template, CategoryGetAll.Methods, CategoryGetAll.Handle);
//app.MapMethods(CategoryGet.Template, CategoryGet.Methods, CategoryGet.Handle);
//app.MapMethods(CategoryPut.Template, CategoryPut.Methods, CategoryPut.Handle);
//app.MapMethods(CategoryDelete.Template, CategoryDelete.Methods, CategoryDelete.Handle);

app.MapMethods(EmployeePost.Template, EmployeePost.Methods, EmployeePost.Handle);
app.MapMethods(EmployeeGetAll.Template, EmployeeGetAll.Methods, EmployeeGetAll.Handle);

app.Run();

// dotnet ef migrations add xxxxx
// dotnet ef migrations remove
// dotnet ef database update