using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        page = page == null || page <= 0 ? 1 : page;
        rows = rows == null || rows <= 0 || rows >= 10 ? 3 : rows;

        // Teste 01
        // public static IResult Action(int page, int rows, UserManager<IdentityUser> userManager)
        // var users = userManager.Users.OrderBy(u => u.UserName).ToList();
        // var employees = users.Select(u => new EmployeeResponse(u.Email, "Name"));
        // return Results.Ok(employees);

        // Teste 02
        // public static IResult Action(int page, int rows, UserManager<IdentityUser> userManager)
        // var users = userManager.Users.OrderBy(u => u.UserName).Skip((page - 1) * rows).Take(rows).ToList();
        // var employees = new List<EmployeeResponse>();
        // foreach (var item in users)
        // {
        //    var claims = userManager.GetClaimsAsync(item).Result;
        //    var claimName = claims.FirstOrDefault(c => c.Type == "Name");
        //    var userName = claimName != null ? claimName.Value : string.Empty;
        //    employees.Add(new EmployeeResponse(item.Email, userName));
        // }

        // Teste 03
        // public static IResult Action(int? page, int? rows, IConfiguration configuration)
        // var db = new SqlConnection(configuration.GetSection("ConnectionStrings").GetValue<string>("IWantDb")); // configuration["ConnectionStrings.IWantDb"]
        // var query = @"SELECT U.Email, C.ClaimValue AS Name FROM AspNetUsers U INNER JOIN AspNetUserClaims C ON (C.UserId = U.Id and C.ClaimType = 'Name') ORDER BY C.ClaimValue OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY ";
        // var param = new { page = page, rows = rows };
        // var employees = db.Query<EmployeeResponse>(query, param);

        // Teste 04
        var employees = query.Execute(page.Value, rows.Value);

        return Results.Ok(employees);
    }

}
