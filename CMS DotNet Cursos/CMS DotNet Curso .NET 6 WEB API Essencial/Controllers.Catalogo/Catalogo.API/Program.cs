using Catalogo.API.Configuration;
using Catalogo.API.Extensions;
using Catalogo.API.GraphQL;
using Catalogo.Data.Persistence;

var builder = WebApplication.CreateBuilder(args);

// builder.Logging.AddCustomLogger();
builder.Services.AddContexts(builder.Configuration);
builder.Services.AddMappers();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddFilters();
builder.Services.AddCompression();
builder.Services.AddControllersWithJson();
builder.Services.AddVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddCorsLocal();
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // .OpenConnection();
}

app.ConfigureExceptionHandler();
app.UseExceptionHandling(app.Environment);
app.UseSwaggerMiddleware(app.Environment);
app.UseCorsMiddleware();
app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseRouting(); 
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(opt => opt.AllowAnyOrigin());
// app.UseMiddleware<TesteGraphQLMiddleware>();
app.MapControllers();

app.Run();
