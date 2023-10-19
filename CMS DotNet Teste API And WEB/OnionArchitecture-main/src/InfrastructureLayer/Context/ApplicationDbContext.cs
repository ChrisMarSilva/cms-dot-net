using DomainLayer.EntityMapper;
using Microsoft.EntityFrameworkCore;

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef 
// dotnet ef migrations add AddTablesInitialOnDataTablesDb
// dotnet ef database update
// dotnet ef migrations remove

// dotnet ef --startup-project src\MyApi.WebApi\  migrations add MyMigration --project src\MyApi.Infrastructure
// dotnet ef --startup-project ..\MyApi.WebApi\ migrations add First_Migration -c DBContextName
// dotnet ef migrations add MyMigrationName --startup-project ./ --project ../MyProject.Data/
// dotnet ef database update --startup-project ./ --project ../MyProject.Data/
// dotnet ef migrations script --startup-project ./ --project ../MyProject.Data/
// dotnet ef migrations add AddTablesInitOnDataTablesDb --project InfrastructureLayer.Migrations
// dotnet ef --startup-project ../Project.Api/ migrations add Initial


namespace InfrastructureLayer.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)  { }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(@"Data Source=127.0.0.1,5402;Initial Catalog=OnionArcDb;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerMap());
        base.OnModelCreating(modelBuilder);
    }
}
