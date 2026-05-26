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
        if (string.IsNullOrWhiteSpace(codigoProduto))
            throw new DomainException("O código do produto é obrigatório.");

        if (codigoProduto.Length > 4)
            throw new DomainException("O código do produto deve possuir no máximo 4 caracteres.");

        if (!string.IsNullOrWhiteSpace(descricao) && descricao.Length > 30)
            throw new DomainException("A descrição do produto deve possuir no máximo 30 caracteres.");

        CodigoProduto = codigoProduto;
        Descricao = descricao;
        Status = status;
    }

    public bool EstaAtivo()
    {
        return Status == StatusRegistro.Ativo;
    }
}
