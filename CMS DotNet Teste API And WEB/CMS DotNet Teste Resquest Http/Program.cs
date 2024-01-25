using HttpClientFactoryProject.Configuration;
using HttpClientFactoryProject.Abstractions;
using HttpClientFactoryProject.Services;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection(nameof(ApiConfig)));
builder.Services.AddSingleton<IApiConfig>(x => x.GetRequiredService<IOptions<ApiConfig>>().Value);

var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

builder.Services.AddHttpClient("TodoApi", options => // builder.Services.AddHttpClient<ITodoService, TodoService>(options => //
{
    options.BaseAddress = new Uri(builder.Configuration["ApiConfig:BaseUrl"]!);
    options.Timeout = new TimeSpan(0, 0, 50);
}).AddPolicyHandler(retryPolicy);

builder.Services.AddRefitClient<ITodoApi>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ApiConfig:BaseUrl"]!);
    c.Timeout = new TimeSpan(0, 0, 50);
}).AddPolicyHandler(retryPolicy);

builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
