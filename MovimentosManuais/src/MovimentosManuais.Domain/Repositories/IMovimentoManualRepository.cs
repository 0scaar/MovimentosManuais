using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.ReadModels;

namespace MovimentosManuais.Domain.Repositories;

public interface IMovimentoManualRepository
{
    Task<decimal> ObterProximoNumeroLancamentoAsync(
        decimal mes,
        decimal ano,
        CancellationToken cancellationToken);

    Task AdicionarAsync(
        MovimentoManual movimentoManual,
        CancellationToken cancellationToken);

    Task<MovimentoManual?> ObterPorChaveAsync(
    decimal mes,
    decimal ano,
    decimal numeroLancamento,
    CancellationToken cancellationToken);

    void Remover(MovimentoManual movimentoManual);

    Task<IReadOnlyCollection<MovimentoManualConsulta>> ListarMovimentosAsync(
        CancellationToken cancellationToken);
}
