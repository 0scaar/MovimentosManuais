using MovimentosManuais.Application.DTOs;

namespace MovimentosManuais.Application.Tests.TestData;

public sealed class CriarMovimentoManualRequestBuilder
{
    private short _mes = 5;
    private short _ano = 2026;
    private string _codigoProduto = "0001";
    private string _codigoCosif = "12345678901";
    private decimal _valor = 100;
    private string _descricao = "Movimento teste";
    private string _codigoUsuario = "usuario";

    public static CriarMovimentoManualRequestBuilder Novo() => new();

    public CriarMovimentoManualRequestBuilder ComMes(short mes)
    {
        _mes = mes;
        return this;
    }

    public CriarMovimentoManualRequestBuilder ComCodigoProduto(string codigoProduto)
    {
        _codigoProduto = codigoProduto;
        return this;
    }

    public CriarMovimentoManualRequest Build()
    {
        return new CriarMovimentoManualRequest(
            _mes,
            _ano,
            _codigoProduto,
            _codigoCosif,
            _valor,
            _descricao,
            _codigoUsuario);
    }
}
