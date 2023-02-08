using Microsoft.AspNetCore.Identity;
using System.Net;

namespace IWantApp.Endpoints.Clients;

public class ClientPost
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ClientRequest clientRequest, UserCreator userCreator) 
    {
        var clientClaims = new List<Claim>
        {
            new Claim("Cpf", clientRequest.Cpf),
            new Claim("Name", clientRequest.Name),
        };

        //----------
        //var newClient = new IdentityUser { UserName = clientRequest.Email, Email = clientRequest.Email };
        //var clientResult = await userManager.CreateAsync(newClient, clientRequest.Password);
        //if (!clientResult.Succeeded)
        //    return Results.ValidationProblem(clientResult.Errors.ConvertToProblemDetails());
        //var result = await userManager.AddClaimsAsync(newClient, clientClaims);
        //if (!result.Succeeded)
        //    return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
        //return Results.Created($"/{Template}/{newClient.Id}", newClient.Id)
        //----------

        (IdentityResult identity, string userId) result = 
            await userCreator.Create(clientRequest.Email, clientRequest.Password, clientClaims);

        if (!result.identity.Succeeded)
            return Results.ValidationProblem(result.identity.Errors.ConvertToProblemDetails());

        return Results.Created($"/{Template}/{result.userId}", result.userId);
    }
}
