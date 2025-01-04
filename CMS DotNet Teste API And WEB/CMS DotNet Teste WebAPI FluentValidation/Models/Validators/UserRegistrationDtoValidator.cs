using CMS_DotNet_Teste_WebAPI_FluentValidation.Models.Requests;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace CMS_DotNet_Teste_WebAPI_FluentValidation.Models.Validators;

internal sealed class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
{
    public UserRegistrationDtoValidator(IOptions<ValidationSettings> options)
    {
        // Email Validation
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        // Password Validation
        RuleFor(x => x.Password)
         .NotEmpty().WithMessage("Password is required")
         .MinimumLength(8).WithMessage("Password must be at least 8 characters long");

        // ConfirmPassword Validation
        RuleFor(x => x.ConfirmPassword)
         .NotEmpty().WithMessage("Confirm Password is required")
         .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
         .Equal(x => x.Password).WithMessage("Passwords do not match");

        // Personal Info Validation
        RuleFor(x => x.PersonalInfo)
          .NotNull().WithMessage("Personal Info is required")
          .SetValidator(new PersonalInfoValidator() as IValidator<PersonalInfo?>);

        // Address Info Validation
        RuleFor(x => x.Address)
          .NotNull().WithMessage("Address is required")
          .SetValidator(new AddressInfoValidator() as IValidator<AddressInfo?>);

        // Interests Validation
        RuleFor(x => x.Interests)
          .NotEmpty().WithMessage("At least one interest is required")
          .Must(x => x.All(i => !string.IsNullOrEmpty(i))).WithMessage("Interests cannot be empty");

        //Age Validation
        var minAge = options.Value.MinimumAge;
        RuleFor(x => x.DateOfBirth)
          .NotNull().WithMessage("Date of Birth is required")
          //.Must(x => x < DateTime.Today).WithMessage("Date of Birth cannot be in the future")
          //.Must(x => CalculateAge(x.Value) >= minAge).WithMessage($"You must be at least {minAge} years old to register")
          .Must(BeInPast).WithMessage("Date of Birth cannot be in the future")
          .Must(x => BeValidAge(x, minAge)).WithMessage($"You must be at least {minAge} years old to register");

        // Terms Validation
        RuleFor(x => x.AcceptTerms!.Value)
            .Equal(true).WithMessage("You must accept the terms and conditions");
    }

    private static bool BeInPast(DateTime? dateOfBirth)
    {
        return dateOfBirth < DateTime.Today;
    }

    private static bool BeValidAge(DateTime? dateOfBirth, int minimumAge)
    {
        var age = DateTime.Now.Year - dateOfBirth.Value.Year;

        if (DateTime.Now.DayOfYear < dateOfBirth.Value.DayOfYear)
            age--;

        return age >= minimumAge;
    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var age = DateTime.Now.Year - dateOfBirth.Year;

        if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            age--;

        return age;
    }
}