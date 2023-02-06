using IWantApp.Endpoints.Categories;
using IWantApp.Domain.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

// builder.Configuration["ConnectionStrings.IWantDb"]
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("IWantDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/config1", (IConfiguration configuration) =>
{
    return Results.Ok(configuration["ConnectionStrings:IWantDb"]);
});

app.MapGet("/config2", (IConfiguration configuration) =>
{
    return Results.Ok(configuration.GetSection("ConnectionStrings").GetValue<string>("IWantDb"));
});

app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle);
app.MapMethods(CategoryGetAll.Template, CategoryGetAll.Methods, CategoryGetAll.Handle);
app.MapMethods(CategoryGet.Template, CategoryGet.Methods, CategoryGet.Handle);
app.MapMethods(CategoryPut.Template, CategoryPut.Methods, CategoryPut.Handle);
app.MapMethods(CategoryDelete.Template, CategoryDelete.Methods, CategoryDelete.Handle);

app.Run();
