namespace IWantApp.Infra.Data;

public class QueryAllProductsSold
{
    private readonly IConfiguration _configuration;

    public QueryAllProductsSold(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public async Task<IEnumerable<ProductSoldResponse>> ExecuteAsync()
    {
        // this._configuration["ConnectionStrings.IWantDb"]
        // this._configuration.GetSection("ConnectionStrings").GetValue<string>("IWantDb")
        var db = new SqlConnection(this._configuration.GetSection("ConnectionStrings").GetValue<string>("IWantDb"));

        var query = @"
          SELECT p.Id, p.Name, count(1) as Amount
          FROM Orders o
            INNER JOIN OrderProducts op on ( op.OrdersId = o.Id )
            INNER JOIN Products p on ( p.Id = op.ProductsId )
          GROUP BY p.Id, p.Name
          ORDER BY Amount DESC
        ";

        var result = await db.QueryAsync<ProductSoldResponse>(query);

        return result;
    }
}
