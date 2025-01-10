using Cache.Web.Components;
using Cache.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddCollections(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();

    //await using (var serviceScope = app.Services.CreateAsyncScope())
    //await using (var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
    //{
    //    await context.Database.EnsureCreatedAsync();
    //}
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.UseCollections(builder.Configuration);

app.Run();
