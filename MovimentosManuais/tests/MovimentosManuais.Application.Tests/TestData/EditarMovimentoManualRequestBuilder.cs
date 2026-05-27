using MovimentosManuais.Application.DTOs;

namespace MovimentosManuais.Application.Tests.TestData;

public sealed class EditarMovimentoManualRequestBuilder
{
    private decimal _valor = 200;
    private string _descricao = "Movimento alterado";
    private string _codigoUsuario = "usuario";

    public static EditarMovimentoManualRequestBuilder Novo() => new();

    public EditarMovimentoManualRequest Build()
    {
        return new EditarMovimentoManualRequest(
            _valor,
            _descricao,
            _codigoUsuario);
    }
}
