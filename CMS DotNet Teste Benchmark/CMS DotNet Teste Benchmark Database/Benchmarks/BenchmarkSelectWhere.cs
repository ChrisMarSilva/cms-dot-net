using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using Dapper;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Benchmarks;

[ThreadingDiagnoser]
[MemoryDiagnoser]
[KeepBenchmarkFiles(false)]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams)] // ByMethod, ByJob, ByParams, ByCategory
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
[RankColumn] //[RankColumn, MinColumn, MaxColumn, Q1Column, Q3Column, AllStatisticsColumn]
[MarkdownExporter] // [MarkdownExporter, HtmlExporter, CsvExporter, RPlotExporter]
public class BenchmarkSelectWhere : BenchmarkSQLServer
{
    //long
    //private readonly string _rawSqlEFLimite = @"SELECT limite as Value FROM Cliente WITH(NOLOCK) WHERE IdCliente = @idCliente";
    //private readonly string _rawSqlDPLimite = @"SELECT limite FROM Cliente WITH(NOLOCK) WHERE idcliente = @pIdcliente";
    private readonly string _rawSqlDPLimite = "SELECT limite FROM cliente WHERE id = @pIdcliente";
    private readonly string _rawSqlPGLimite = "SELECT limite FROM cliente WHERE id = @pIdcliente";

    //ClienteModel
    //private readonly string _rawSqlEFFull = @"SELECT * FROM Cliente WITH(NOLOCK) WHERE idcliente = {0}";
    //private readonly string _rawSqlDPFull = @"SELECT * FROM Cliente WITH(NOLOCK) WHERE idcliente = @pIdcliente";
    private readonly string _rawSqlDPFull = "SELECT id, limite FROM cliente WHERE id = @pIdcliente";
    private readonly string _rawSqlPGFull = "SELECT id, limite FROM cliente WHERE id = @pIdcliente";

    [Params(1)] public short _idCliente { get; set; }
    //[Params(1, 100, 500, 1_000, 5_000, 10_000, 20_000, 30_000, 32_767)] public short _idCliente { get; set; }

    ////EF - long
    //[Benchmark] public async Task<long> SQLServer_EF_Select_Limite() => await ContextSQLServer.Database.SqlQueryRaw<long>(sql: _rawSqlEFLimite, parameters: new SqlParameter("@idCliente", _idCliente)).FirstOrDefaultAsync();
    //[Benchmark] public async Task<long> SQLServer_EF_WhereFirst_Limite() => await ContextSQLServer.Clientes.AsNoTracking().Where(x => x.IdCliente == _idCliente).Select(x => x.Limite).FirstOrDefaultAsync();
    ////[Benchmark] public async Task<long> SQLServer_EF_SelectFirst_Limite() => await ContextSQLServer.Clientes.AsNoTracking().Select(x => x.Limite).Where(x => x == _idCliente).FirstOrDefaultAsync();

    ////DP - long
    //[Benchmark] public async Task<long> SQLServer_DP_Select_Limite_01() => await ConnectionSQLServer.QueryFirstOrDefaultAsync<long>(_rawSqlDPLimite, new { pIdcliente = _idCliente }); // MELHOR
    [Benchmark] public async Task<long> SQLServer_DP_Select_Limite_01() => await ConnectionPostgreSQL.QueryFirstOrDefaultAsync<long>(_rawSqlDPLimite, new { pIdcliente = _idCliente }); // MELHOR

    //PG - long
    [Benchmark]
    public async Task<long> PostgreSQL_PG_Select_Limite_01()
    {
        long result = default;

        await using var command = ConnectionPostgreSQL.CreateCommand();
        command.CommandText = _rawSqlPGLimite;
        command.Parameters.AddWithValue("pIdcliente", _idCliente);

        result = Convert.ToInt64(await command.ExecuteScalarAsync());

        return result;
    }

    [Benchmark]
    public async Task<long> PostgreSQL_PG_Select_Limite_02()
    {
        long result = default;

        await using var command = ConnectionPostgreSQL.CreateCommand();
        command.CommandText = _rawSqlPGLimite;
        command.Parameters.AddWithValue("pIdcliente", _idCliente);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync()) result = reader.GetInt64(0);

        return result;
    }

    [Benchmark]
    public async Task<long> PostgreSQL_PG_Select_Limite_03()
    {
        long result = default;

        await using var command = ConnectionPostgreSQL.CreateCommand();
        command.CommandText = _rawSqlPGLimite;
        command.Parameters.AddWithValue("pIdcliente", _idCliente);

        await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
        if (await reader.ReadAsync().ConfigureAwait(false)) result = reader.GetInt64(0);

        return result;
    }

    //EF - ClienteModel
    //[Benchmark] public async Task<ClienteModel> SQLServer_EF_Select_Full() => await ContextSQLServer.Clientes.FromSqlRaw(_rawSqlEFFull, _idCliente).AsNoTracking().FirstOrDefaultAsync();
    //[Benchmark] public async Task<ClienteModel> SQLServer_EF_WhereFirst_Full() => await ContextSQLServer.Clientes.AsNoTracking().Where(x => x.IdCliente == _idCliente).Select(x => new ClienteModel(x.IdCliente, x.Limite)).FirstOrDefaultAsync();
    //[Benchmark] public async Task<ClienteModel> SQLServer_EF_SelectFirst_Full_01() => await ContextSQLServer.Clientes.AsNoTracking().Select(x => new ClienteModel(x.IdCliente, x.Limite)).Where(x => x.IdCliente == _idCliente).FirstOrDefaultAsync(); // erro
    //[Benchmark] public ClienteModel SQLServer_EF_SelectFirst_Full_02() => ContextSQLServer.Clientes.AsNoTracking().AsEnumerable().Select(x => new ClienteModel(x.IdCliente, x.Limite)).Where(x => x.IdCliente == _idCliente).ToList().FirstOrDefault();

    //DP - ClienteModel
    //[Benchmark] public async Task<ClienteModel> SQLServer_DP_Select_Full_01() => await ConnectionSQLServer.QueryFirstOrDefaultAsync<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente }); // MELHOR
    [Benchmark] public async Task<ClienteModel> SQLServer_DP_Select_Full_01() => await ConnectionPostgreSQL.QueryFirstOrDefaultAsync<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente }); // MELHOR
    ////[Benchmark] public async Task<ClienteModel> SQLServer_DP_Select_Full_02() => await ConnectionSQLServer.ExecuteScalarAsync<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });// erro
    //[Benchmark] public async Task<ClienteModel> SQLServer_DP_Select_Full_03() => await ConnectionSQLServer.QuerySingleAsync<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });
    //[Benchmark] public async Task<ClienteModel> SQLServer_DP_Select_Full_04() => await ConnectionSQLServer.QueryFirstAsync<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });

    //PG - ClienteModel
    [Benchmark]
    public async Task<ClienteModel> PostgreSQL_PG_Select_Full_01()
    {
        ClienteModel result = default;

        await using var command = ConnectionPostgreSQL.CreateCommand();
        command.CommandText = _rawSqlPGFull;
        command.Parameters.AddWithValue("pIdcliente", _idCliente);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync()) result = new ClienteModel(reader.GetInt16(0), reader.GetInt64(1));

        return result;
    }

    [Benchmark]
    public async Task<ClienteModel> PostgreSQL_PG_Select_Full_02()
    {
        ClienteModel result = default;

        await using var command = ConnectionPostgreSQL.CreateCommand();
        command.CommandText = _rawSqlPGFull;
        command.Parameters.AddWithValue("pIdcliente", _idCliente);

        await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
        if (await reader.ReadAsync().ConfigureAwait(false)) result = new ClienteModel(reader.GetInt16(0), reader.GetInt64(1));

        return result;
    }

    //[Benchmark]
    //public async Task<IList<ClienteModel>> PostgreSQL_PG_Select_Full_03()
    //{
    //    var result = new List<ClienteModel>();

    //    await using var command = ConnectionPostgreSQL.CreateCommand();
    //    command.CommandText = _rawSqlPGFull;
    //    command.Parameters.AddWithValue("pIdcliente", _idCliente);

    //    await using var reader = await command.ExecuteReaderAsync();

    //    while (await reader.ReadAsync())
    //    {
    //        var cliente = new ClienteModel(reader.GetInt16(0), reader.GetInt64(1));
    //        result.Add(cliente);
    //    }

    //    return result;
    //}
}


//| Method                         | _idCliente | Mean     | Error     | StdDev    | Median   | Rank | Completed Work Items | Lock Contentions | Allocated |
//|------------------------------- |----------- |---------:|----------:|----------:|---------:|-----:|---------------------:|-----------------:|----------:|
//| PostgreSQL_PG_Select_Limite_03 | 1          | 1.406 ms | 0.0215 ms | 0.0168 ms | 1.398 ms |    1 |               3.0000 |           0.0039 |   1.35 KB |
//| PostgreSQL_PG_Select_Limite_02 | 1          | 1.457 ms | 0.0291 ms | 0.0524 ms | 1.445 ms |    1 |               3.0000 |           0.0020 |   1.36 KB |
//| SQLServer_DP_Select_Limite_01  | 1          | 1.465 ms | 0.0290 ms | 0.0271 ms | 1.471 ms |    1 |               3.0000 |                - |   1.76 KB |
//| PostgreSQL_PG_Select_Limite_01 | 1          | 1.545 ms | 0.0537 ms | 0.1443 ms | 1.495 ms |    1 |               3.0000 |           0.0078 |   1.51 KB |

//| Method                         | _idCliente | Mean     | Error     | StdDev    | Median   | Rank | Completed Work Items | Lock Contentions | Allocated |
//|------------------------------- |----------- |---------:|----------:|----------:|---------:|-----:|---------------------:|-----------------:|----------:|
//| PostgreSQL_PG_Select_Full_02   | 1          | 1.409 ms | 0.0208 ms | 0.0174 ms | 1.403 ms |    1 |               3.0000 |           0.0078 |   1.51 KB |
//| PostgreSQL_PG_Select_Full_01   | 1          | 1.421 ms | 0.0223 ms | 0.0186 ms | 1.418 ms |    1 |               3.0000 |           0.0039 |   1.49 KB |
//| SQLServer_DP_Select_Full_01    | 1          | 1.516 ms | 0.0485 ms | 0.1369 ms | 1.455 ms |    1 |               3.0000 |           0.0020 |    1.9 KB |



//| Method                           | _idCliente | Mean     | Error     | StdDev    | Median   | Rank | Completed Work Items | Lock Contentions | Gen0   | Allocated |
//|--------------------------------- |----------- |---------:|----------:|----------:|---------:|-----:|---------------------:|-----------------:|-------:|----------:|
//| SQLServer_EF_WhereFirst_Full     | 1          | 3.755 ms | 0.6100 ms | 1.6491 ms | 2.973 ms |    3 |               3.0000 |           0.0156 | 7.8125 |  13.07 KB |
//| SQLServer_EF_SelectFirst_Full_02 | 1          | 2.798 ms | 0.2837 ms | 0.7670 ms | 2.494 ms |    1 |                    - |                - | 3.9063 |   6.28 KB |
//| SQLServer_DP_Select_Full_01      | 1          | 2.971 ms | 0.2756 ms | 0.7729 ms | 2.688 ms |    2 |               3.0000 |           0.0078 | 3.9063 |   6.73 KB |
//| SQLServer_DP_Select_Full_03      | 1          | 2.638 ms | 0.1441 ms | 0.4111 ms | 2.489 ms |    1 |               3.0000 |           0.0234 |      - |   6.81 KB |  SQLServer_DP_Select_Full_03
//| SQLServer_DP_Select_Full_04      | 1          | 3.186 ms | 0.3232 ms | 0.9115 ms | 3.115 ms |    2 |               3.0000 |           0.0117 | 3.9063 |   6.73 KB |
//|                                  |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_WhereFirst_Full     | 100        | 2.906 ms | 0.1694 ms | 0.4862 ms | 2.747 ms |    3 |               3.0000 |                - | 7.8125 |  14.98 KB |
//| SQLServer_EF_SelectFirst_Full_02 | 100        | 2.341 ms | 0.1474 ms | 0.4108 ms | 2.195 ms |    1 |                    - |                - | 3.9063 |   6.15 KB |  SQLServer_EF_SelectFirst_Full_02
//| SQLServer_DP_Select_Full_01      | 100        | 2.685 ms | 0.2500 ms | 0.6758 ms | 2.415 ms |    2 |               3.0000 |           0.0078 | 3.9063 |   6.55 KB |
//| SQLServer_DP_Select_Full_03      | 100        |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//| SQLServer_DP_Select_Full_04      | 100        |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//|                                  |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_WhereFirst_Full     | 500        | 4.124 ms | 0.5748 ms | 1.6117 ms | 3.652 ms |    2 |               3.0000 |                - |      - |  15.38 KB |
//| SQLServer_EF_SelectFirst_Full_02 | 500        | 2.437 ms | 0.1384 ms | 0.3993 ms | 2.311 ms |    1 |                    - |                - | 3.9063 |   6.17 KB |
//| SQLServer_DP_Select_Full_01      | 500        | 2.378 ms | 0.0909 ms | 0.2518 ms | 2.306 ms |    1 |               3.0000 |           0.0078 |      - |   6.56 KB |  SQLServer_DP_Select_Full_01
//| SQLServer_DP_Select_Full_03      | 500        |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//| SQLServer_DP_Select_Full_04      | 500        |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//|                                  |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_WhereFirst_Full     | 1000       | 3.238 ms | 0.2577 ms | 0.7268 ms | 3.021 ms |    3 |               3.0000 |           0.0625 | 7.8125 |  12.88 KB |
//| SQLServer_EF_SelectFirst_Full_02 | 1000       | 2.562 ms | 0.1741 ms | 0.4912 ms | 2.387 ms |    2 |                    - |                - |      - |   6.16 KB |
//| SQLServer_DP_Select_Full_01      | 1000       | 2.351 ms | 0.0969 ms | 0.2764 ms | 2.277 ms |    1 |               3.0000 |           0.0234 |      - |   5.37 KB |  SQLServer_DP_Select_Full_01
//| SQLServer_DP_Select_Full_03      | 1000       |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//| SQLServer_DP_Select_Full_04      | 1000       |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//|                                  |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_WhereFirst_Full     | 5000       | 3.099 ms | 0.2858 ms | 0.7529 ms | 2.965 ms |    1 |               3.0000 |           0.0391 | 7.8125 |  12.77 KB |  SQLServer_EF_WhereFirst_Full
//| SQLServer_EF_SelectFirst_Full_02 | 5000       | 3.744 ms | 0.7431 ms | 2.0716 ms | 2.835 ms |    1 |                    - |                - | 3.9063 |   6.15 KB |
//| SQLServer_DP_Select_Full_01      | 5000       | 5.677 ms | 1.4215 ms | 4.0557 ms | 3.793 ms |    2 |               3.0000 |           0.0078 |      - |   5.37 KB |
//| SQLServer_DP_Select_Full_03      | 5000       |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//| SQLServer_DP_Select_Full_04      | 5000       |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//|                                  |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_WhereFirst_Full     | 10000      | 6.623 ms | 1.4007 ms | 4.1301 ms | 5.093 ms |    3 |               3.0078 |           0.0078 | 7.8125 |  12.77 KB |
//| SQLServer_EF_SelectFirst_Full_02 | 10000      | 5.916 ms | 1.5733 ms | 4.6142 ms | 3.463 ms |    2 |                    - |                - | 3.9063 |   6.15 KB |
//| SQLServer_DP_Select_Full_01      | 10000      | 2.991 ms | 0.4296 ms | 1.1468 ms | 2.488 ms |    1 |               3.0000 |           0.0313 |      - |   5.38 KB |  SQLServer_DP_Select_Full_01
//| SQLServer_DP_Select_Full_03      | 10000      |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//| SQLServer_DP_Select_Full_04      | 10000      |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//|                                  |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_WhereFirst_Full     | 20000      | 2.869 ms | 0.2587 ms | 0.7169 ms | 2.609 ms |    1 |               3.0000 |                - |      - |  13.09 KB |
//| SQLServer_EF_SelectFirst_Full_02 | 20000      | 2.783 ms | 0.2653 ms | 0.7483 ms | 2.520 ms |    1 |                    - |                - |      - |   6.16 KB |
//| SQLServer_DP_Select_Full_01      | 20000      | 3.554 ms | 0.7020 ms | 1.9569 ms | 2.763 ms |    1 |               3.0000 |           0.0117 |      - |   5.37 KB |
//| SQLServer_DP_Select_Full_03      | 20000      |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//| SQLServer_DP_Select_Full_04      | 20000      |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//|                                  |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_WhereFirst_Full     | 30000      | 8.675 ms | 2.5018 ms | 7.0971 ms | 5.142 ms |    2 |               3.0000 |                - |      - |  12.83 KB |
//| SQLServer_EF_SelectFirst_Full_02 | 30000      | 3.212 ms | 0.4361 ms | 1.2300 ms | 2.753 ms |    1 |                    - |                - | 3.9063 |   6.15 KB |
//| SQLServer_DP_Select_Full_01      | 30000      | 3.126 ms | 0.2831 ms | 0.8168 ms | 2.897 ms |    1 |               3.0000 |                - |      - |   5.39 KB |  SQLServer_DP_Select_Full_01
//| SQLServer_DP_Select_Full_03      | 30000      |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//| SQLServer_DP_Select_Full_04      | 30000      |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//|                                  |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_WhereFirst_Full     | 32767      | 4.105 ms | 0.2746 ms | 0.7654 ms | 3.908 ms |    3 |               3.0000 |           0.0156 |      - |  15.35 KB |
//| SQLServer_EF_SelectFirst_Full_02 | 32767      | 2.456 ms | 0.1356 ms | 0.3954 ms | 2.354 ms |    1 |                    - |                - | 3.9063 |   6.15 KB |  SQLServer_EF_SelectFirst_Full_02
//| SQLServer_DP_Select_Full_01      | 32767      | 3.251 ms | 0.3040 ms | 0.8675 ms | 3.188 ms |    2 |               3.0000 |           0.0156 |      - |    6.6 KB |
//| SQLServer_DP_Select_Full_03      | 32767      |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |
//| SQLServer_DP_Select_Full_04      | 32767      |       NA |        NA |        NA |       NA |    ? |                   NA |               NA |     NA |        NA |


//| Method                         | _idCliente | Mean     | Error     | StdDev    | Median   | Rank | Completed Work Items | Lock Contentions | Gen0   | Allocated |
//|------------------------------- |----------- |---------:|----------:|----------:|---------:|-----:|---------------------:|-----------------:|-------:|----------:|
//| SQLServer_EF_Select_Limite     | 1          | 2.243 ms | 0.0439 ms | 0.0857 ms | 2.231 ms |    1 |               3.0000 |           0.0117 | 7.8125 |  14.01 KB |
//| SQLServer_EF_WhereFirst_Limite | 1          | 2.351 ms | 0.0736 ms | 0.2040 ms | 2.265 ms |    1 |               3.0000 |           0.0273 | 7.8125 |  12.27 KB |
//| SQLServer_DP_Select_Limite_01  | 1          | 2.136 ms | 0.0731 ms | 0.2061 ms | 2.049 ms |    1 |               3.0000 |           0.0156 |      - |   5.12 KB |  SQLServer_DP_Select_Limite_01
//| SQLServer_DP_Select_Limite_02  | 1          | 2.768 ms | 0.2903 ms | 0.8375 ms | 2.442 ms |    2 |               5.0000 |           0.0352 |      - |    5.3 KB |
//|                                |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_Select_Limite     | 100        | 3.168 ms | 0.2991 ms | 0.8533 ms | 2.940 ms |    2 |               3.0000 |           0.0234 | 7.8125 |  13.83 KB |
//| SQLServer_EF_WhereFirst_Limite | 100        | 2.412 ms | 0.0640 ms | 0.1752 ms | 2.351 ms |    1 |               3.0000 |           0.0078 | 7.8125 |  12.09 KB |
//| SQLServer_DP_Select_Limite_01  | 100        | 2.446 ms | 0.1370 ms | 0.3888 ms | 2.345 ms |    3 |               3.0000 |           0.0273 |      - |    5.1 KB |
//| SQLServer_DP_Select_Limite_02  | 100        | 2.400 ms | 0.1222 ms | 0.3367 ms | 2.342 ms |    2 |               5.0000 |           0.0313 |      - |   5.37 KB |  SQLServer_DP_Select_Limite_02
//|                                |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_Select_Limite     | 500        | 2.381 ms | 0.0853 ms | 0.2351 ms | 2.315 ms |    1 |               3.0000 |           0.0156 | 7.8125 |  13.82 KB |
//| SQLServer_EF_WhereFirst_Limite | 500        | 2.508 ms | 0.1070 ms | 0.3019 ms | 2.419 ms |    1 |               3.0000 |           0.0273 | 7.8125 |  12.08 KB |
//| SQLServer_DP_Select_Limite_01  | 500        | 2.248 ms | 0.0993 ms | 0.2783 ms | 2.157 ms |    2 |               3.0000 |           0.0117 |      - |    5.1 KB |
//| SQLServer_DP_Select_Limite_02  | 500        | 2.064 ms | 0.0410 ms | 0.0883 ms | 2.027 ms |    1 |               5.0000 |           0.0234 |      - |   5.37 KB |  SQLServer_DP_Select_Limite_01
//|                                |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_Select_Limite     | 1000       | 2.261 ms | 0.0517 ms | 0.1423 ms | 2.201 ms |    1 |               3.0000 |           0.0156 | 7.8125 |  13.82 KB |
//| SQLServer_EF_WhereFirst_Limite | 1000       | 2.419 ms | 0.0983 ms | 0.2757 ms | 2.326 ms |    1 |               3.0000 |           0.0156 | 7.8125 |  12.08 KB |
//| SQLServer_DP_Select_Limite_01  | 1000       | 2.130 ms | 0.0730 ms | 0.2023 ms | 2.053 ms |    1 |               3.0000 |           0.0273 |      - |    5.1 KB |  SQLServer_DP_Select_Limite_01
//| SQLServer_DP_Select_Limite_02  | 1000       | 2.066 ms | 0.0447 ms | 0.1225 ms | 2.009 ms |    1 |               5.0000 |           0.0117 |      - |   5.37 KB |
//|                                |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_Select_Limite     | 5000       | 2.252 ms | 0.0507 ms | 0.1388 ms | 2.204 ms |    1 |               3.0000 |           0.0234 | 7.8125 |  13.83 KB |
//| SQLServer_EF_WhereFirst_Limite | 5000       | 2.398 ms | 0.0862 ms | 0.2472 ms | 2.290 ms |    1 |               3.0000 |           0.0078 | 7.8125 |  12.08 KB |
//| SQLServer_DP_Select_Limite_01  | 5000       | 2.023 ms | 0.0396 ms | 0.0406 ms | 2.004 ms |    1 |               3.0000 |           0.0117 |      - |    5.1 KB |  SQLServer_DP_Select_Limite_01
//| SQLServer_DP_Select_Limite_02  | 5000       | 2.091 ms | 0.0486 ms | 0.1348 ms | 2.033 ms |    1 |               5.0000 |           0.0313 |      - |   5.37 KB |
//|                                |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_Select_Limite     | 10000      | 2.263 ms | 0.0634 ms | 0.1756 ms | 2.181 ms |    1 |               3.0000 |           0.0234 | 7.8125 |  13.83 KB |
//| SQLServer_EF_WhereFirst_Limite | 10000      | 2.273 ms | 0.0535 ms | 0.1483 ms | 2.202 ms |    1 |               3.0000 |           0.0273 | 7.8125 |  12.38 KB |
//| SQLServer_DP_Select_Limite_01  | 10000      | 2.102 ms | 0.0525 ms | 0.1445 ms | 2.056 ms |    1 |               3.0000 |           0.0117 |      - |    5.1 KB |  SQLServer_DP_Select_Limite_01
//| SQLServer_DP_Select_Limite_02  | 10000      | 2.120 ms | 0.0611 ms | 0.1694 ms | 2.064 ms |    1 |               5.0000 |           0.0078 |      - |   5.37 KB |
//|                                |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_Select_Limite     | 20000      | 2.233 ms | 0.0456 ms | 0.1264 ms | 2.176 ms |    1 |               3.0000 |           0.0117 | 7.8125 |  13.83 KB |
//| SQLServer_EF_WhereFirst_Limite | 20000      | 2.233 ms | 0.0414 ms | 0.0758 ms | 2.198 ms |    1 |               3.0000 |           0.0039 | 7.8125 |  12.38 KB |
//| SQLServer_DP_Select_Limite_01  | 20000      | 2.099 ms | 0.0513 ms | 0.1422 ms | 2.026 ms |    1 |               3.0000 |           0.0469 |      - |    5.1 KB |
//| SQLServer_DP_Select_Limite_02  | 20000      | 2.069 ms | 0.0412 ms | 0.0970 ms | 2.033 ms |    1 |               5.0000 |           0.0313 |      - |   5.37 KB |  SQLServer_DP_Select_Limite_02
//|                                |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_Select_Limite     | 30000      | 2.328 ms | 0.0782 ms | 0.2219 ms | 2.238 ms |    1 |               3.0000 |           0.0078 | 7.8125 |  13.83 KB |
//| SQLServer_EF_WhereFirst_Limite | 30000      | 2.185 ms | 0.0301 ms | 0.0251 ms | 2.182 ms |    1 |               3.0000 |           0.0234 | 7.8125 |  12.19 KB |
//| SQLServer_DP_Select_Limite_01  | 30000      | 2.067 ms | 0.0412 ms | 0.1113 ms | 2.020 ms |    1 |               3.0039 |           0.0039 |      - |    5.1 KB |
//| SQLServer_DP_Select_Limite_02  | 30000      | 2.020 ms | 0.0398 ms | 0.0473 ms | 1.999 ms |    1 |               5.0000 |           0.0273 |      - |   5.37 KB |  SQLServer_DP_Select_Limite_02
//|                                |            |          |           |           |          |      |                      |                  |        |           |
//| SQLServer_EF_Select_Limite     | 32767      | 2.228 ms | 0.0441 ms | 0.1146 ms | 2.177 ms |    1 |               3.0039 |                - | 7.8125 |  13.82 KB |
//| SQLServer_EF_WhereFirst_Limite | 32767      | 2.217 ms | 0.0440 ms | 0.0556 ms | 2.193 ms |    1 |               3.0000 |           0.0156 | 7.8125 |  12.08 KB |
//| SQLServer_DP_Select_Limite_01  | 32767      | 2.130 ms | 0.0718 ms | 0.2012 ms | 2.024 ms |    1 |               3.0000 |           0.0039 |      - |    5.1 KB |
//| SQLServer_DP_Select_Limite_02  | 32767      | 2.066 ms | 0.0413 ms | 0.0924 ms | 2.030 ms |    1 |               5.0000 |           0.0273 |      - |   5.37 KB |  SQLServer_DP_Select_Limite_02
