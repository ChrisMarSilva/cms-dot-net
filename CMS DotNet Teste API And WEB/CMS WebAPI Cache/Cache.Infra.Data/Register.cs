using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Cache.Infra.Data;


public static class Register
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
    {
      
        //var connectionStringsConfiguration = configuration.GetSection("ConnectionStrings");

        //var connectionString = connectionStringsConfiguration.GetValue<string>("JDPI_SPI_AGE");
        //var vendor = connectionStringsConfiguration.GetValue("Provider:Vendor", "PostgreSQL")!;
        //var useWithNoLock = configuration.GetValue("Provider:UseWithNoLock", true);

        //services.AddDbContext<DataContext>(options =>
        //{
        //    if (vendor.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
        //    {
        //        JdId.SetMssqlGuidGenerator();

        //        options.UseSqlServer(connectionString, builder =>
        //        {
        //            builder.CommandTimeout(timeout);
        //        });

        //        if (useWithNoLock)
        //        {
        //            options.UseCustomSqlServerQuerySqlGenerator();
        //        }

        //        SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);
        //    }
        //    else if (vendor.Equals("Oracle", StringComparison.OrdinalIgnoreCase))
        //    {
        //        JdId.SetOracleGuidGenerator();

        //        options.UseOracle(connectionString, builder =>
        //        {
        //            builder.CommandTimeout(timeout);
        //            builder.UseOracleSQLCompatibility((OracleSQLCompatibility)connectionStringsConfiguration.GetValue("Provider:UseOracleSQLCompatibility", 2));
        //        });

        //        SqlMapper.AddTypeHandler(new OracleGuidTypeHandler());
        //        SqlMapper.RemoveTypeMap(typeof(Guid));
        //        SqlMapper.RemoveTypeMap(typeof(Guid?));
        //    }
        //    else if (vendor.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
        //    {
        //        JdId.SetNpgsqlGuidGenerator();

        //        options.UseNpgsql(connectionString, builder =>
        //        {
        //            builder.CommandTimeout(timeout);

        //        });

        //        // ArgumentException: Cannot write DateTime with Kind=Unspecified to PostgreSQL type  'timestamp with time zone', only UTC is supported.Note that its not possible to mix DateTimes with different Kinds in an array, range, or multirange.Arg_ParamName_Name
        //        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        //        //AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        //    }
        //}).AddUnitOfWork<DataContext>().AddScoped<IDataContext>(sp => sp.GetRequiredService<DataContext>());

        //services.AddScoped<IRepository, BaseRepository>().AddScoped<IRepositoryTransaction>(sp => sp.GetRequiredService<BaseRepository>());
        //services.AddScoped<IQueryRepository, QueryRepository>();
        //services.AddScoped<ICommandRepository, CommandRepository>();

        //services.AddServiceDomain();

        return services;
    }
}