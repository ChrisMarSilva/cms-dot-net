namespace IWantApp.Domain.Users;

public class UserCreator
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserCreator(UserManager<IdentityUser> userManager)
    {
        this._userManager = userManager;
    }

    public async Task<(IdentityResult, string)> Create(string email, string password, List<Claim> claims)
    {
        var newUser = new IdentityUser { UserName = email, Email = email };

        var userResult = await this._userManager.CreateAsync(newUser, password);

        if (!userResult.Succeeded)
            return (userResult, String.Empty); // return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        //var claimResult = userManager.AddClaimAsync(user, new Claim("EmployeeCode", employeeRequest.EmployeeCode)).Result;
        //if (!claimResult.Succeeded)
        //    return (result, String.Empty); // return Results.BadRequest(claimResult.Errors.First());

        //claimResult = userManager.AddClaimAsync(user, new Claim("Name", employeeRequest.Name)).Result;
        //if (!claimResult.Succeeded)
        //    return (result, String.Empty); // return Results.BadRequest(claimResult.Errors.First());

        var claimsResult = await this._userManager.AddClaimsAsync(newUser, claims);

        if (!claimsResult.Succeeded)
            return (claimsResult, String.Empty); // return Results.ValidationProblem(claimsResult.Errors.ConvertToProblemDetails());

        return (claimsResult, newUser.Id);
    }
}
