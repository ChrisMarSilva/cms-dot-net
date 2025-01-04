﻿using FluentValidation;
using FluentValidation.Results;

namespace CMS_DotNet_Teste_WebAPI_FluentValidation.Models.Validators;

internal static class InlineValidationExtensions
{
    public static Task<ValidationResult> ValidateInlineAsync<T>(this T obj, Action<InlineValidator<T>> configure)
    {
        var validator = new InlineValidator<T>();
        configure(validator);

        return validator.ValidateAsync(obj);
    }
}
