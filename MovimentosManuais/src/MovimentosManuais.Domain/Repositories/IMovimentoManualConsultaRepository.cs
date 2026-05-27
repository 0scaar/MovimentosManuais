using MovimentosManuais.Domain.ReadModels;

namespace MovimentosManuais.Domain.Repositories;

public interface IMovimentoManualConsultaRepository
{
    Task<IReadOnlyCollection<MovimentoManualConsulta>> ListarMovimentosAsync(
        CancellationToken cancellationToken);
}
