using MovimentosManuais.Application.DTOs;
using MovimentosManuais.Application.Interfaces;
using MovimentosManuais.Domain.Repositories;

namespace MovimentosManuais.Application.Services;

public sealed class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IReadOnlyCollection<ProdutoResponse>> ListarAtivosAsync(
        CancellationToken cancellationToken)
    {
        var produtos = await _produtoRepository.ObterAtivosAsync(cancellationToken);

        return produtos
            .Select(produto => new ProdutoResponse(
                produto.CodigoProduto,
                produto.Descricao))
            .ToList();
    }
}
