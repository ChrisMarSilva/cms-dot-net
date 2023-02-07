using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration _configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    // public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
    {
        var db = new SqlConnection(this._configuration.GetSection("ConnectionStrings").GetValue<string>("IWantDb")); // configuration["ConnectionStrings.IWantDb"]

        var query = @"
          SELECT U.Email, C.ClaimValue AS Name 
          FROM AspNetUsers U 
            INNER JOIN AspNetUserClaims C ON (C.UserId = U.Id and C.ClaimType = 'Name') 
          ORDER BY C.ClaimValue
          OFFSET (@page - 1) * @rows ROWS
          FETCH NEXT @rows ROWS ONLY
        ";

        var param = new { page = page, rows = rows };

        var employees = db.Query<EmployeeResponse>(query, param);
        // var employees = await db.QueryAsync<EmployeeResponse>(query, param);

        return employees;
    }

}
