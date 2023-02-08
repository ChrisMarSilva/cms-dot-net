namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration _configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public async Task<IEnumerable<EmployeeResponse>> ExecuteAsync(int page, int rows)
    {
        // this._configuration["ConnectionStrings.IWantDb"]
        // this._configuration.GetSection("ConnectionStrings").GetValue<string>("IWantDb")
        var db = new SqlConnection(this._configuration.GetSection("ConnectionStrings").GetValue<string>("IWantDb")); 

        var query = @"
          SELECT U.Email, C.ClaimValue AS Name 
          FROM AspNetUsers U 
            INNER JOIN AspNetUserClaims C ON (C.UserId = U.Id and C.ClaimType = 'Name') 
          ORDER BY C.ClaimValue
          OFFSET (@page - 1) * @rows ROWS
          FETCH NEXT @rows ROWS ONLY
        ";

        var param = new { page = page, rows = rows };
        var result = await db.QueryAsync<EmployeeResponse>(query, param);

        return result;
    }

}
