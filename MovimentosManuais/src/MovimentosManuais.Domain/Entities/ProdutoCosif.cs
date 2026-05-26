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
        Validar(codigoProduto, codigoCosif, codigoClassificacao);

        CodigoProduto = codigoProduto;
        CodigoCosif = codigoCosif;
        CodigoClassificacao = codigoClassificacao;
        Status = status;
    }

    public bool EstaAtivo()
    {
        return Status == StatusRegistro.Ativo;
    }

    private static void Validar(
        string codigoProduto,
        string codigoCosif,
        string? codigoClassificacao)
    {
        DomainValidation.NotNullOrWhiteSpace(codigoProduto, "O código do produto é obrigatório.");
        DomainValidation.MaxLength(codigoProduto, 4, "O código do produto deve possuir no máximo 4 caracteres.");

        DomainValidation.NotNullOrWhiteSpace(codigoCosif, "O código COSIF é obrigatório.");
        DomainValidation.MaxLength(codigoCosif, 11, "O código COSIF deve possuir no máximo 11 caracteres.");

        DomainValidation.NotNullOrWhiteSpace(codigoClassificacao, "O código de classificação é obrigatório.");
        DomainValidation.MaxLength(codigoClassificacao, 6, "O código de classificação deve possuir no máximo 6 caracteres.");
    }
}
