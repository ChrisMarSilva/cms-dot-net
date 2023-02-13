using RestWithASPNETUdemy_Calculator.Services.Implementations;
using RestWithASPNETUdemy_Calculator.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
