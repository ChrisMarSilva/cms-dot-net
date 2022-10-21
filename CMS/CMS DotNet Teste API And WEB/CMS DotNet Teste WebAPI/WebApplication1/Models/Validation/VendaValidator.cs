using FluentValidation;
using System;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Validation
{
    public class VendaValidator : AbstractValidator<Venda>
    {
        public VendaValidator()
        {
            RuleFor(v => v.Data)
                .NotNull().WithMessage("A data da venda não pode ser nula.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("A data da venda não pode estar no futuro.");

            RuleFor(v => v.Total)
                .GreaterThan(0).When(v => v.Tipo == TipoVenda.Padrao)
                .WithMessage("O total da venda deve ser maior que zero.");

            //RuleFor(v => v.Itens)
            // .NotNull().WithMessage("A propriedade Itens da venda não pode ser nula.")
            // .Must(i => i != null ? i.Count > 0 : false).WithMessage("A venda deve possuir pelo menos 1 item.")
            // .SetCollectionValidator(new ItemVendaValidator()
            // );

            When(v => v.Tipo == TipoVenda.Brinde, () =>
            {
                RuleFor(v => v.Total)
                    .Equal(0).WithMessage("O total da venda deve ser {ComparisonValue} para vendas do tipo brinde.");
                RuleFor(v => v.Itens.Count)
                    .Equal(1).WithMessage("Vendas do tipo brinde devem conter apenas {ComparisonValue} item.");
            });
        }
    }
}