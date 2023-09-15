using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShop.IdentityServer.Data;

namespace VShop.IdentityServer.Services;

public class ProfileAppService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    public ProfileAppService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var userId = context.Subject.GetSubjectId(); // id do usuário no IS

        var user = await _userManager.FindByIdAsync(userId: userId); // localiza o usuário pelo id

        var userClaims = await _userClaimsPrincipalFactory.CreateAsync(user: user!); // cria ClaimsPrincipal para o usuario

        var claims = userClaims.Claims.ToList();  // define uma coleção de claims para o usuário e inclui o sobrenome e o nome do usuário
        claims.Add(item: new Claim(type: JwtClaimTypes.FamilyName, value: user!.LastName));
        claims.Add(item: new Claim(type: JwtClaimTypes.GivenName, value: user.FirstName));

        if (_userManager.SupportsUserRole) // se o userManager do identity suportar role
        {
            var roles = await _userManager.GetRolesAsync(user: user);  // obtem a lista dos nomes das roles para o usuário

            foreach (string role in roles) // percorre a lista
            {
                claims.Add(item: new Claim(type: JwtClaimTypes.Role, value: role)); // adiciona a role na claim

                if (_roleManager.SupportsRoleClaims) // se roleManager suportar claims para roles
                {
                    var identityRole = await _roleManager.FindByNameAsync(roleName: role); // localiza o perfil

                    if (identityRole != null) // inclui o perfil
                        claims.AddRange(collection: await _roleManager.GetClaimsAsync(role: identityRole));  // inclui as claims associada com a role
                }
            }
        }
        
        context.IssuedClaims = claims; //retorna as claims no contexto
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var userId = context.Subject.GetSubjectId(); // obtem o id do usuario no IS

        var user = await _userManager.FindByIdAsync(userId: userId); // localiza o usuário

        context.IsActive = user is not null; // verifica se esta ativo
    }
}