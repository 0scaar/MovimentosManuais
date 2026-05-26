using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Domain.Repositories;

public interface IProdutoCosifRepository
{
    Task<IReadOnlyCollection<ProdutoCosif>> ObterAtivosPorProdutoAsync(string codigoProduto, CancellationToken cancellationToken);
    Task<bool> ExisteAsync(string codigoProduto, string codigoCosif, CancellationToken cancellationToken);
}
