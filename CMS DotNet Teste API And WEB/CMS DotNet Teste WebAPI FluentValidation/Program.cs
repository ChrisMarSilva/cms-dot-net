using CMS_DotNet_Teste_WebAPI_FluentValidation.Models.Requests;
using CMS_DotNet_Teste_WebAPI_FluentValidation.Models.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection("ValidationSettings"));
builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

app.MapPost("/api/register", async (UserRegistrationDto request, IValidator<UserRegistrationDto> validator) =>
{
    var personalInfoResult = await request.PersonalInfo.ValidateInlineAsync(v => 
    {
        v.RuleFor(x => x.FirstName).NotEmpty().WithMessage("3 - First name is required.");
        v.RuleFor(x => x.LastName).NotEmpty().WithMessage("3 - Last name is required.");
        v.RuleFor(x => x.PreferredName).NotEmpty().WithMessage("3 - Preferred name is required.");
    });

    if (!personalInfoResult.IsValid)
        return Results.ValidationProblem(personalInfoResult.ToDictionary());

    var validationResult = validator.Validate(request);

    // update the error response section
    if (!validationResult.IsValid)
    {
        var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "2 - Validation failed.",
            Detail = "2 - One or more validation errors occurred.",
            Instance = "/api/register"
        };

        return Results.Problem(problemDetails);
    }

    return Results.Ok(new { Message = "Registration successful!" });
});

app.MapPost("/api/register/old", (UserRegistrationDto request) =>
{
    var errors = request.Validate();

    // update the error response section
    if (errors.Any())
    {
        var problemDetails = new HttpValidationProblemDetails(
            new Dictionary<string, string[]>
            {
                { "ValidationsErros", errors.ToArray() }
            })
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation failed.",
            Detail = "One or more validation errors occurred.",
            Instance = "/api/register"
        };

        return Results.Problem(problemDetails);
    }

    return Results.Ok(new { Message = "Registration successful!" });
});

app.Run();
