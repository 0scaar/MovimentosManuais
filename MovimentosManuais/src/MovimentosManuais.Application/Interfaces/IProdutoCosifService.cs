using MovimentosManuais.Application.DTOs;

namespace MovimentosManuais.Application.Interfaces;

public interface IProdutoCosifService
{
    Task<IReadOnlyCollection<ProdutoCosifResponse>> ListarAtivosPorProdutoAsync(
        string codigoProduto,
        CancellationToken cancellationToken);
}
