using MovimentosManuais.Application.DTOs;

namespace MovimentosManuais.Application.Interfaces;

public interface IMovimentoManualService
{
    Task<MovimentoManualResponse> CriarAsync(
        CriarMovimentoManualRequest request,
        CancellationToken cancellationToken);

    Task<MovimentoManualResponse> EditarAsync(
        short mes,
        short ano,
        int numeroLancamento,
        string codigoProduto,
        string codigoCosif,
        EditarMovimentoManualRequest request,
        CancellationToken cancellationToken);

    Task ExcluirAsync(
        ExcluirMovimentoManualRequest request,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<MovimentoManualResponse>> ListarAsync(
        CancellationToken cancellationToken);
}
