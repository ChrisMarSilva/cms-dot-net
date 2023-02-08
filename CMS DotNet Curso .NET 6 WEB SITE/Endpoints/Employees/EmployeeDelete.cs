using Microsoft.AspNetCore.Identity;
using static System.Net.WebRequestMethods;

namespace IWantApp.Endpoints.Employees;

public class EmployeeDelete
{
    public static string Template => "/employees/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var user = await userManager.GetUserAsync(http.User);

        if (user == null)
            return Results.NotFound("Employee not found");

        await userManager.DeleteAsync(user);

        return Results.Ok();
    }
}
