using Web.App.TempoDeVida.Services;
using Web.App.TempoDeVida.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IOperationTransient, Operation>(); // VariasVezes     - registra um serviço que é criado cada vez que é solicitado
builder.Services.AddScoped<IOperationScoped, Operation>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
builder.Services.AddSingleton<IOperationSingleton, Operation>(); // UmVezQdoSobeAPI - registra um serviço que é criado uma única vez durante todo o ciclo de vida do aplicativo

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
