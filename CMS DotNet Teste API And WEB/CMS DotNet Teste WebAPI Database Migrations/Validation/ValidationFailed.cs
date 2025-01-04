using FluentValidation.Results;

namespace CMS_DotNet_Teste_WebAPI_Database_Migrations.Validation;

public record ValidationFailed(IEnumerable<ValidationFailure> Errors)
{
    public ValidationFailed(ValidationFailure error) : this(new[] { error }) { }
}
