using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Domain.Repositories;

public interface IMovimentoManualRepository
{
    Task<decimal> ObterProximoNumeroLancamentoAsync(decimal mes, decimal ano, CancellationToken cancellationToken);
    Task AdicionarAsync(MovimentoManual movimentoManual, CancellationToken cancellationToken);
}
