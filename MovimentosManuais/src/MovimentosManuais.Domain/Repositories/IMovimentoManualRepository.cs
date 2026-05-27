using MovimentosManuais.Domain.Entities;
namespace MovimentosManuais.Domain.Repositories;

public interface IMovimentoManualRepository
{
    Task<int> ObterProximoNumeroLancamentoAsync(
        short mes,
        short ano,
        CancellationToken cancellationToken);

    Task AdicionarAsync(
        MovimentoManual movimentoManual,
        CancellationToken cancellationToken);

    Task<MovimentoManual?> ObterPorChaveAsync(
    short mes,
    short ano,
    int numeroLancamento,
    CancellationToken cancellationToken);

    Task<MovimentoManual?> ObterPorChaveCompletaAsync(
        short mes,
        short ano,
        int numeroLancamento,
        string codigoProduto,
        string codigoCosif,
        CancellationToken cancellationToken);

    void Remover(MovimentoManual movimentoManual);
}
