using FluentValidation;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Validation
{
    public class VagaValidator : AbstractValidator<Vaga>
    {
        public VagaValidator()
        {
            RuleFor(v => v.Titulo)
                .NotEmpty().WithMessage("O titulo da vaga deve ser preenchido.")
                .Length(10, 100).WithMessage("O titulo da vaga deve ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(v => v.Descricao)
                .NotEmpty().WithMessage("A descricao da vaga deve ser preenchida.")
                .Length(10, 200).WithMessage("A descricao da vaga deve ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(v => v.Salario)
                .GreaterThan(0).WithMessage("O salario deve ser maior que zero.");

            RuleFor(v => v.LocalTrabalho)
                .Length(10, 100).WithMessage("O local de trabalho da vaga deve ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}