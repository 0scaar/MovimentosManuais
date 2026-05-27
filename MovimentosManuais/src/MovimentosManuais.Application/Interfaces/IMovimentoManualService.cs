using MovimentosManuais.Application.DTOs;

namespace MovimentosManuais.Application.Interfaces;

public interface IMovimentoManualService
{
    Task<MovimentoManualResponse> CriarAsync(
        CriarMovimentoManualRequest request,
        CancellationToken cancellationToken);

    Task<MovimentoManualResponse> EditarAsync(
        EditarMovimentoManualRequest request,
        CancellationToken cancellationToken);

    Task ExcluirAsync(
        ExcluirMovimentoManualRequest request,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<MovimentoManualResponse>> ListarAsync(
        CancellationToken cancellationToken);
}
