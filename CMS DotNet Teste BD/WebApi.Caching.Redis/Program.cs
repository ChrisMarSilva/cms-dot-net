var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOutputCache(opt =>
{
    opt.AddBasePolicy(x => x.Expire(TimeSpan.FromSeconds(10)));
    opt.AddPolicy("Expire20", x => x.Expire(TimeSpan.FromSeconds(20)));
    opt.AddPolicy("Expire30", x => x.Expire(TimeSpan.FromSeconds(30)));
});

builder.Services.AddStackExchangeRedisOutputCache(opt => 
{
    opt.InstanceName = "FormulaOneCache1";
    opt.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
});

builder.Services.AddStackExchangeRedisCache(opt => 
{
    opt.InstanceName = "FormulaOneCache2";
    opt.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseOutputCache();

app.Run();
