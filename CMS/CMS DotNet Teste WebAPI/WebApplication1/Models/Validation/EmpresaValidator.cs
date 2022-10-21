using FluentValidation;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Validation
{
    public class EmpresaValidator : AbstractValidator<Empresa>
    {
        public EmpresaValidator()
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O nome da empresa deve ser preenchido")
                .Length(3, 100).WithMessage("O nome da empresa deve ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}