using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Enums;

namespace MovimentosManuais.Domain.Tests.TestData;

public sealed class ProdutoCosifBuilder
{
    private string _codigoProduto = "0001";
    private string _codigoCosif = "12345678901";
    private string? _codigoClassificacao = "123456";
    private StatusRegistro? _status = StatusRegistro.Ativo;

    public static ProdutoCosifBuilder Novo() => new();

    public ProdutoCosifBuilder ComCodigoProduto(string codigoProduto)
    {
        _codigoProduto = codigoProduto;
        return this;
    }

    public ProdutoCosifBuilder ComCodigoCosif(string codigoCosif)
    {
        _codigoCosif = codigoCosif;
        return this;
    }

    public ProdutoCosifBuilder ComCodigoClassificacao(string? codigoClassificacao)
    {
        _codigoClassificacao = codigoClassificacao;
        return this;
    }

    public ProdutoCosifBuilder ComStatus(StatusRegistro? status)
    {
        _status = status;
        return this;
    }

    public ProdutoCosif Build()
    {
        return new ProdutoCosif(
            _codigoProduto,
            _codigoCosif,
            _codigoClassificacao,
            _status);
    }
}
