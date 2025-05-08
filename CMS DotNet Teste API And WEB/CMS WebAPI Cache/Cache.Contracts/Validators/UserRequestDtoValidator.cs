using Cache.Contracts.Request;
using FluentValidation;

namespace Cache.Contracts.Validators;

internal sealed class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
{
    public UserRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
         .NotEmpty().WithMessage("Password is required")
         .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
    }
}