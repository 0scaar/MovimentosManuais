using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Domain.Repositories;

public interface IProdutoRepository
{
    Task<IReadOnlyCollection<Produto>> ObterAtivosAsync(CancellationToken cancellationToken);
}
