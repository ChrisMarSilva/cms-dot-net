using RestWithASPNETUdemy_Calculator.Services.Implementations;
using RestWithASPNETUdemy_Calculator.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
