using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Enums;

namespace MovimentosManuais.Domain.Tests.TestData;

public sealed class ProdutoBuilder
{
    private string _codigoProduto = "0001";
    private string? _descricao = "Produto Teste";
    private StatusRegistro? _status = StatusRegistro.Ativo;

    public static ProdutoBuilder Novo() => new();

    public ProdutoBuilder ComCodigoProduto(string codigoProduto)
    {
        _codigoProduto = codigoProduto;
        return this;
    }

    public ProdutoBuilder ComDescricao(string? descricao)
    {
        _descricao = descricao;
        return this;
    }

    public ProdutoBuilder ComStatus(StatusRegistro? status)
    {
        _status = status;
        return this;
    }

    public Produto Build()
    {
        return new Produto(_codigoProduto, _descricao, _status);
    }
}
