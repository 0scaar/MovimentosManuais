using MovimentosManuais.Application.DTOs;

namespace MovimentosManuais.Application.Tests.TestData;

public sealed class EditarMovimentoManualRequestBuilder
{
    private short _mes = 5;
    private short _ano = 2026;
    private int _numeroLancamento = 1;
    private string _codigoProduto = "0001";
    private string _codigoCosif = "12345678901";
    private decimal _valor = 200;
    private string _descricao = "Movimento alterado";
    private string _codigoUsuario = "usuario";

    public static EditarMovimentoManualRequestBuilder Novo() => new();

    public EditarMovimentoManualRequest Build()
    {
        return new EditarMovimentoManualRequest(
            _mes,
            _ano,
            _numeroLancamento,
            _codigoProduto,
            _codigoCosif,
            _valor,
            _descricao,
            _codigoUsuario);
    }
}
