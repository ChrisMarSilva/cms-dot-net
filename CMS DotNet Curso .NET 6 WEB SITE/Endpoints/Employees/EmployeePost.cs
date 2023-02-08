using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Categories;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(
        EmployeeRequest employeeRequest,
        HttpContext http,
        UserManager<IdentityUser> userManager)
    {
        var newUser = new IdentityUser{ UserName = employeeRequest.Email, Email = employeeRequest.Email};
        var userResult = userManager.CreateAsync(newUser, employeeRequest.Password).Result;

        if (!userResult.Succeeded)
            return Results.ValidationProblem(userResult.Errors.ConvertToProblemDetails());

        //var claimResult = userManager.AddClaimAsync(user, new Claim("EmployeeCode", employeeRequest.EmployeeCode)).Result;
        //if (!claimResult.Succeeded)
        //    return Results.BadRequest(claimResult.Errors.First());

        //claimResult = userManager.AddClaimAsync(user, new Claim("Name", employeeRequest.Name)).Result;
        //if (!claimResult.Succeeded)
        //    return Results.BadRequest(claimResult.Errors.First());
       
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var userClaims = new List<Claim> 
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
            new Claim("CreatedBy", userId),
        };

        var claimsResult = userManager.AddClaimsAsync(newUser, userClaims).Result;

        if (!claimsResult.Succeeded)
            return Results.ValidationProblem(claimsResult.Errors.ConvertToProblemDetails());

        return Results.Created($"/{Template}/{newUser.Id}", newUser.Id);
    }

}
