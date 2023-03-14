using Catalogo.API.Models;
using Catalogo.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace Catalogo.API.ApiEndpoints;

public static class AutenticacaoEndpoints
{
    public static void MapAutenticacaoEndpoints(this WebApplication app)
    {
        app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
        {
            if (userModel == null)
                return Results.BadRequest("Login Inválido");

            if ((userModel.UserName != "cms" || userModel.Password != "123") &&
                (userModel.UserName != "string" || userModel.Password != "string"))
                return Results.BadRequest("Login Inválido");

            var key = app.Configuration["Jwt:Key"];
            var issuer = app.Configuration["Jwt:Issuer"];
            var audience = app.Configuration["Jwt:Audience"];
            
            var tokenString = tokenService.GerarToken(key: key, issuer: issuer, audience: audience, user: userModel);
            return Results.Ok(new { token = tokenString });
        }).Produces(StatusCodes.Status400BadRequest)
          .Produces(StatusCodes.Status200OK)
          .WithName("Login")
          .WithTags("Autenticacao");
    }
}
