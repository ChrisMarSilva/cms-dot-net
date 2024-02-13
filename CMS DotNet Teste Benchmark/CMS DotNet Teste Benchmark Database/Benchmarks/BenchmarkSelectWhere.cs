using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using Dapper;
using Microsoft.EntityFrameworkCore;
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
    //private readonly string _rawSqlEFLimite = @"SELECT limite as Value FROM Cliente WITH(NOLOCK) WHERE IdCliente = @idCliente";
    //private readonly string _rawSqlDPLimite = @"SELECT limite FROM Cliente WITH(NOLOCK) WHERE idcliente = @pIdcliente";
    //private readonly string _rawSqlEFFull = @"SELECT * FROM Cliente WITH(NOLOCK) WHERE idcliente = {0}";
    private readonly string _rawSqlDPFull = @"SELECT * FROM Cliente WITH(NOLOCK) WHERE idcliente = @pIdcliente";
    [Params(1, 100, 500, 1_000, 5_000, 10_000, 20_000, 30_000, 32_767)] public short _idCliente { get; set; }

    ////EF - long
    //[Benchmark] public async Task<long> SQLServer_EF_Select_Limite() => await ContextSQLServer.Database.SqlQueryRaw<long>(sql: _rawSqlEFLimite, parameters: new SqlParameter("@idCliente", _idCliente)).FirstOrDefaultAsync();
    //[Benchmark] public async Task<long> SQLServer_EF_WhereFirst_Limite() => await ContextSQLServer.Clientes.AsNoTracking().Where(x => x.IdCliente == _idCliente).Select(x => x.Limite).FirstOrDefaultAsync();
    ////[Benchmark] public async Task<long> SQLServer_EF_SelectFirst_Limite() => await ContextSQLServer.Clientes.AsNoTracking().Select(x => x.Limite).Where(x => x == _idCliente).FirstOrDefaultAsync();

    ////DP - long
    //[Benchmark] public async Task<long> SQLServer_DP_Select_Limite_01() => await ConnectionSQLServer.QueryFirstOrDefaultAsync<long>(_rawSqlDPLimite, new { pIdcliente = _idCliente }); // MELHOR
    //[Benchmark] public async Task<long> SQLServer_DP_Select_Limite_02() => await ConnectionSQLServer.ExecuteScalarAsync<long>(_rawSqlDPLimite, new { pIdcliente = _idCliente });

    //EF - ClienteModel
    //[Benchmark] public async Task<ClienteModel> SQLServer_EF_Select_Full() => await ContextSQLServer.Clientes.FromSqlRaw(_rawSqlEFFull, _idCliente).AsNoTracking().FirstOrDefaultAsync();
    [Benchmark] public async Task<ClienteModel> SQLServer_EF_WhereFirst_Full() => await ContextSQLServer.Clientes.AsNoTracking().Where(x => x.IdCliente == _idCliente).Select(x => new ClienteModel(x.IdCliente, x.Limite)).FirstOrDefaultAsync();
    //[Benchmark] public async Task<ClienteModel> SQLServer_EF_SelectFirst_Full_01() => await ContextSQLServer.Clientes.AsNoTracking().Select(x => new ClienteModel(x.IdCliente, x.Limite)).Where(x => x.IdCliente == _idCliente).FirstOrDefaultAsync();
    [Benchmark] public async Task<ClienteModel> SQLServer_EF_SelectFirst_Full_02() => ContextSQLServer.Clientes.AsNoTracking().AsEnumerable().Select(x => new ClienteModel(x.IdCliente, x.Limite)).Where(x => x.IdCliente == _idCliente).ToList().FirstOrDefault();
    
    //DP - ClienteModel
    [Benchmark] public async Task<ClienteModel> SQLServer_DP_Select_Full_01() => await ConnectionSQLServer.QueryFirstOrDefaultAsync<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });
    //[Benchmark] public async Task<ClienteModel> SQLServer_DP_Select_Full_02() => await ConnectionSQLServer.ExecuteScalarAsync<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });
    [Benchmark] public async Task<ClienteModel> SQLServer_DP_Select_Full_03() => await ConnectionSQLServer.QuerySingleAsync<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });
    [Benchmark] public async Task<ClienteModel> SQLServer_DP_Select_Full_04() => await ConnectionSQLServer.QueryFirstAsync<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });

}


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
