using MovimentosManuais.Domain.Common;
using MovimentosManuais.Domain.Enums;

namespace MovimentosManuais.Domain.Entities;

public sealed class ProdutoCosif : Entity
{
    public string CodigoProduto { get; private set; }
    public string CodigoCosif { get; private set; }
    public string? CodigoClassificacao { get; private set; }
    public StatusRegistro? Status { get; private set; }

    public Produto? Produto { get; private set; }

    private readonly List<MovimentoManual> _movimentos = [];
    public IReadOnlyCollection<MovimentoManual> Movimentos => _movimentos.AsReadOnly();

    private ProdutoCosif()
    {
        CodigoProduto = string.Empty;
        CodigoCosif = string.Empty;
    }

    public ProdutoCosif(
        string codigoProduto,
        string codigoCosif,
        string? codigoClassificacao,
        StatusRegistro? status)
    {
        if (string.IsNullOrWhiteSpace(codigoProduto))
            throw new DomainException("O código do produto é obrigatório.");

        if (codigoProduto.Length > 4)
            throw new DomainException("O código do produto deve possuir no máximo 4 caracteres.");

        if (string.IsNullOrWhiteSpace(codigoCosif))
            throw new DomainException("O código COSIF é obrigatório.");

        if (codigoCosif.Length > 11)
            throw new DomainException("O código COSIF deve possuir no máximo 11 caracteres.");

        if (!string.IsNullOrWhiteSpace(codigoClassificacao) && codigoClassificacao.Length > 6)
            throw new DomainException("O código de classificação deve possuir no máximo 6 caracteres.");

        CodigoProduto = codigoProduto;
        CodigoCosif = codigoCosif;
        CodigoClassificacao = codigoClassificacao;
        Status = status;
    }

    public bool EstaAtivo()
    {
        return Status == StatusRegistro.Ativo;
    }
}
