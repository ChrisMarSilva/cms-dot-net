using CMS_DotNet_Teste_WebAPI_FluentValidation.Models.Requests;
using FluentValidation;

namespace CMS_DotNet_Teste_WebAPI_FluentValidation.Models.Validators;

internal sealed class PersonalInfoValidator : AbstractValidator<PersonalInfo>
{
    public PersonalInfoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is required");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required");
    }
}