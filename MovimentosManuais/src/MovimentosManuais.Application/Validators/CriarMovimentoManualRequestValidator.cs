using FluentValidation;
using MovimentosManuais.Application.DTOs;

namespace MovimentosManuais.Application.Validators;

public sealed class CriarMovimentoManualRequestValidator
    : AbstractValidator<CriarMovimentoManualRequest>
{
    public CriarMovimentoManualRequestValidator()
    {
        RuleFor(x => x.Mes)
            .InclusiveBetween(1, 12)
            .WithMessage("O mês deve estar entre 1 e 12.");

        RuleFor(x => x.Ano)
            .GreaterThanOrEqualTo(1900)
            .WithMessage("O ano informado é inválido.");

        RuleFor(x => x.CodigoProduto)
            .NotEmpty()
            .WithMessage("O código do produto é obrigatório.")
            .MaximumLength(4)
            .WithMessage("O código do produto deve possuir no máximo 4 caracteres.");

        RuleFor(x => x.CodigoCosif)
            .NotEmpty()
            .WithMessage("O código COSIF é obrigatório.")
            .MaximumLength(11)
            .WithMessage("O código COSIF deve possuir no máximo 11 caracteres.");

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("O valor do movimento deve ser maior que zero.");

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("A descrição é obrigatória.")
            .MaximumLength(50)
            .WithMessage("A descrição deve possuir no máximo 50 caracteres.");

        RuleFor(x => x.CodigoUsuario)
            .NotEmpty()
            .WithMessage("O código do usuário é obrigatório.")
            .MaximumLength(15)
            .WithMessage("O código do usuário deve possuir no máximo 15 caracteres.");
    }
}
