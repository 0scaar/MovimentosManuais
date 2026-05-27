using MovimentosManuais.Application.DTOs;

namespace MovimentosManuais.Application.Interfaces;

public interface IProdutoService
{
    Task<IReadOnlyCollection<ProdutoResponse>> ListarAtivosAsync(
        CancellationToken cancellationToken);
}
