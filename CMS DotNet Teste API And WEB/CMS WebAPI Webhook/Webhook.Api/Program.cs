using Microsoft.EntityFrameworkCore;
using Webhook.Api.Order;
using Webhook.Api.WeatherForecast;
using Webhook.Api.Webhook;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

//builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddSingleton<InMemoryOrderRepository>();
builder.Services.AddSingleton<InMemoryWebhookRepository>();

builder.Services.AddHttpClient<WebhookDispatcher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    //await using (var serviceScope = app.Services.CreateAsyncScope())
    //await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
    //{
    //    await dbContext.Database.EnsureCreatedAsync();
    //}
}

app.UseHttpsRedirection();

app.AddMapWeatherEndpoints();
app.AddMapOrderEndpoints();
app.AddMapWebhookEndpoints();

app.Run();