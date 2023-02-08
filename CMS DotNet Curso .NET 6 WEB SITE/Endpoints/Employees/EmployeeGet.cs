using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGet
{
    public static string Template => "/employees/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var user = await userManager.GetUserAsync(http.User);
        // var user = await userManager.GetUserIdAsync(id);
        // var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        // var user = userManager.Users.FirstOrDefault(u => u.Id == userId);
        // var user = userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

        if (user == null)
            return Results.NotFound("Employee not found");

        var claims = await userManager.GetClaimsAsync(user);
        var claimName = claims.FirstOrDefault(c => c.Type == "Name");
        var userName = claimName != null ? claimName.Value : string.Empty;

        var result = new EmployeeResponse(user.Email, userName);

        return Results.Ok(result);
    }
}
