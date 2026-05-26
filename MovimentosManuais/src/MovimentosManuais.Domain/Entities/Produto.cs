using MovimentosManuais.Domain.Common;
using MovimentosManuais.Domain.Enums;

namespace MovimentosManuais.Domain.Entities;

public sealed class Produto : Entity
{
    public string CodigoProduto { get; private set; }
    public string? Descricao { get; private set; }
    public StatusRegistro? Status { get; private set; }

    private readonly List<ProdutoCosif> _cosifs = [];
    public IReadOnlyCollection<ProdutoCosif> Cosifs => _cosifs.AsReadOnly();

    private Produto()
    {
        CodigoProduto = string.Empty;
    }

    public Produto(string codigoProduto, string? descricao, StatusRegistro? status)
    {
        Validar(codigoProduto, descricao);

        CodigoProduto = codigoProduto;
        Descricao = descricao;
        Status = status;
    }

    public bool EstaAtivo()
    {
        return Status == StatusRegistro.Ativo;
    }

    private static void Validar(string codigoProduto, string? descricao)
    {
        DomainValidation.NotNullOrWhiteSpace(codigoProduto, "O código do produto é obrigatório.");
        DomainValidation.MaxLength(codigoProduto, 4, "O código do produto deve possuir no máximo 4 caracteres.");

        DomainValidation.NotNullOrWhiteSpace(descricao, "A descrição é obrigatória.");
        DomainValidation.MaxLength(descricao, 30, "A descrição do produto deve possuir no máximo 30 caracteres.");
    }
}
