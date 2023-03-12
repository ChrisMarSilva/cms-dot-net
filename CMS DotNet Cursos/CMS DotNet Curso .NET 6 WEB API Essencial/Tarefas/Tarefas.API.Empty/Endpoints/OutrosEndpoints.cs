namespace Tarefas.API.Empty.Endpoints;

public static class OutrosEndpoints
{
    public static void UseMapOutrosEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => $"Bem-Vindo a API Tarefas {DateTime.Now}");
        
        app.MapGet("frases", async () => 
            await new HttpClient().GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes")
        );
    }
}
