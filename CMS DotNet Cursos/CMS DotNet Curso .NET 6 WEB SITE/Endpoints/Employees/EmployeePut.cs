using Microsoft.AspNetCore.Identity;

namespace IWantApp.Endpoints.Employees;

public class EmployeePut
{
    public static string Template => "/employees/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var alterUser = await userManager.GetUserAsync(http.User);

        if (alterUser == null)
            return Results.NotFound("Employee not found");

        // alterUser.Email = employeeRequest.Email;
        // alterUser.Password = employeeRequest.Password;

        var userClaimsOld = await userManager.GetClaimsAsync(alterUser);
        var claimsResultOld = await userManager.RemoveClaimsAsync(alterUser, userClaimsOld);

        if (!claimsResultOld.Succeeded)
            return Results.ValidationProblem(claimsResultOld.Errors.ConvertToProblemDetails());

        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
            new Claim("CreatedBy", userId),
            new Claim("EditedBy", userId),
        };
;
        var claimsResult = await userManager.AddClaimsAsync(alterUser, userClaims);

        if (!claimsResult.Succeeded)
            return Results.ValidationProblem(claimsResult.Errors.ConvertToProblemDetails());

        await userManager.UpdateAsync(alterUser);

        return Results.Ok();
    }
}
