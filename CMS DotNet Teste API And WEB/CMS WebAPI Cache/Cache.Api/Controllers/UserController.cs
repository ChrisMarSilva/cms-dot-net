using Cache.Api.Contracts.Mappings;
using Cache.Api.Contracts.Requests;
using Cache.Api.Database.Contexts;
using Cache.Api.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Cache.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private IValidator<UserRequestDto> _validator;

    public UserController(ILogger<UserController> logger, IUserService userService, AppDbContext ctx, IValidator<UserRequestDto> validator)
    {
        _logger = logger;
        _userService = userService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IResult> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            var usersModel = await _userService.GetAllAsync(cancellationToken);

            var response = usersModel.MapToResponse();
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    [HttpGet("{id:Guid}")]
    public async Task<IResult> FindById(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var userModel = await _userService.GetByIdAsync(id, cancellationToken);

            if (userModel is null || userModel.Id == Guid.Empty)
                return Results.NotFound();

            var response = userModel.MapToResponse();
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IResult> Create([FromBody] UserRequestDto request, CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var userModel = request.MapToUserModel();
            var result = await _userService.CreateAsync(userModel, cancellationToken);
            var response = result.MapToResponse();

            return Results.Created(nameof(FindById), response);
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IResult> Update([FromBody] UserRequestDto request, Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var userModel = await _userService.GetByIdAsync(id, cancellationToken);
            if (userModel is null || userModel.Id == Guid.Empty)
                return Results.NotFound();

            var userRequest = request.MapToUserModel();

            userModel.AlterUser(userRequest.Name, userRequest.Email, userRequest.Password);

            var result = await _userService.UpdateAsync(userModel, cancellationToken);
            var response = result!.MapToResponse();

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var userModel = await _userService.GetByIdAsync(id, cancellationToken);

            if (userModel is null || userModel.Id == Guid.Empty)
                return Results.NotFound();

            await _userService.DeleteByIdAsync(userModel, cancellationToken);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }
}
