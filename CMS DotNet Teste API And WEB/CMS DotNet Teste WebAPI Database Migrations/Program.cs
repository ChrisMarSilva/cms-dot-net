using Bogus;
using CMS_DotNet_Teste_WebAPI_Database_Migrations.Database;
using CMS_DotNet_Teste_WebAPI_Database_Migrations.Movies;
using CMS_DotNet_Teste_WebAPI_Database_Migrations.Weather;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration["DbConnectionString"]!);

    //optionsBuilder.UseNpgsql(builder.Configuration["DbConnectionString"]!)
    //    .UseAsyncSeeding(async (context, _, ct) =>
    //    {
    //        var faker = new Faker<Movie>()
    //            .UseSeed(420)
    //            .RuleFor(x => x.Id, f => f.Random.Guid())
    //            .RuleFor(x => x.Title, f => f.Person.FullName)
    //            .RuleFor(x => x.YearOfRelease, f => f.Person.DateOfBirth.Year);

    //        var moviesToSeed = faker.Generate(5);

    //        var contains = await context.Set<Movie>().ContainsAsync(moviesToSeed[0], cancellationToken: ct);
    //        if (!contains)
    //        {
    //            context.Set<Movie>().AddRange(moviesToSeed);
    //            await context.SaveChangesAsync();
    //        }
    //    })
    //    .UseSeeding((context, _) =>
    //    {
    //        var faker = new Faker<Movie>()
    //            .UseSeed(420)
    //            .RuleFor(x => x.Id, f => f.Random.Guid())
    //            .RuleFor(x => x.Title, f => f.Person.FullName)
    //            .RuleFor(x => x.YearOfRelease, f => f.Person.DateOfBirth.Year);

    //        var moviesToSeed = faker.Generate(5);

    //        var contains = context.Set<Movie>().Contains(moviesToSeed[0]);
    //        if (!contains)
    //        {
    //            context.Set<Movie>().AddRange(moviesToSeed);
    //            context.SaveChanges();
    //        }
    //    });
});

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    //await using (var serviceScope = app.Services.CreateAsyncScope())
    //await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
    //{
    //    await dbContext.Database.EnsureCreatedAsync();
    //}
}

app.UseHttpsRedirection();
app.MapMovieEndpoints();
app.MapWeatherEndpoints();
app.Run();
