namespace IWantApp.Endpoints.Clients;

public class ClientGet
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(HttpContext http)
    {
        var user = http.User;

        var idUser = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var nameUser = user.Claims.FirstOrDefault(c => c.Type == "Name").Value;
        var cpfUser = user.Claims.FirstOrDefault(c => c.Type == "Cpf").Value;

        var result = new { Id = idUser, Name = nameUser, Cpf = cpfUser };

        return Results.Ok(result);
    }
}
