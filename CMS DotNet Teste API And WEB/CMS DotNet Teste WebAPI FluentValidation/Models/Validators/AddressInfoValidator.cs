using CMS_DotNet_Teste_WebAPI_FluentValidation.Models.Requests;
using FluentValidation;

namespace CMS_DotNet_Teste_WebAPI_FluentValidation.Models.Validators;

//public interface IAddressApiService
//{
//    Task<bool> ValidateAsync(AddressInfo address);
//}

internal sealed class AddressInfoValidator : AbstractValidator<AddressInfo>
{
    public AddressInfoValidator() // IAddressApiService addressApi
    {
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required");

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal Code is required");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required");

        //RuleFor(x => x)
        //    .MustAsync((a, _) => addressApi.ValidateAsync(a));
    }
}

