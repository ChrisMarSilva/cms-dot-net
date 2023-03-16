using Catalogo.API.Configuration;
using Catalogo.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddContexts(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddFilters();
builder.Services.AddCompression();
// builder.Services.AddCorsLocal();
builder.Services.AddControllersWithJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.AddSwagger();

// builder.Logging.AddCustomLogger();

var app = builder.Build();

app.ConfigureExceptionHandler();
app.UseExceptionHandling(app.Environment);
app.UseSwaggerMiddleware(app.Environment);
app.UseCorsMiddleware();
app.UseHttpsRedirection();
app.UseResponseCompression();
// app.UseRouting(); 
app.UseAuthorization();
app.MapControllers();

app.Run();

