using Catalogo.Web.Mvc.Services;
using Catalogo.Web.Mvc.Services.Interfaces;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Add services to the container.

builder.Services.AddHttpClient("CategoriasApi", c =>
{
#pragma warning disable CS8604 // Possible null reference argument.
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:CategoriasApi"]);
#pragma warning restore CS8604 // Possible null reference argument.
});

builder.Services.AddHttpClient("AutenticaApi", c =>
{
#pragma warning disable CS8604 // Possible null reference argument.
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:AutenticaApi"]);
#pragma warning restore CS8604 // Possible null reference argument.
    c.DefaultRequestHeaders.Accept.Clear();
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient("ProdutosApi", c =>
{
#pragma warning disable CS8604 // Possible null reference argument.
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProdutosApi"]);
#pragma warning restore CS8604 // Possible null reference argument.
});

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IAutenticacaoService, AutenticacaoService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Configure the HTTP request pipeline.
    app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
