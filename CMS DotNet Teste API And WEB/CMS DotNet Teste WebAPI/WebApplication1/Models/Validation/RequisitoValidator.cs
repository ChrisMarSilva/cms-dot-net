using FluentValidation;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Validation
{
    public class RequisitoValidator : AbstractValidator<Requisito>
    {
        public RequisitoValidator()
        {
            RuleFor(r => r.Descricao)
                .NotEmpty().WithMessage("A descricao do requisito deve ser informada.")
                .Length(10, 100).WithMessage("A descricao do requisito deve ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}