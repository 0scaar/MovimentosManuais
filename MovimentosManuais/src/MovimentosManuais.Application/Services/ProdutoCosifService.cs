using MovimentosManuais.Application.DTOs;
using MovimentosManuais.Application.Interfaces;
using MovimentosManuais.Domain.Repositories;

namespace MovimentosManuais.Application.Services;

public sealed class ProdutoCosifService : IProdutoCosifService
{
    private readonly IProdutoCosifRepository _produtoCosifRepository;

    public ProdutoCosifService(IProdutoCosifRepository produtoCosifRepository)
    {
        _produtoCosifRepository = produtoCosifRepository;
    }

    public async Task<IReadOnlyCollection<ProdutoCosifResponse>> ListarAtivosPorProdutoAsync(
        string codigoProduto,
        CancellationToken cancellationToken)
    {
        var cosifs = await _produtoCosifRepository.ObterAtivosPorProdutoAsync(
            codigoProduto,
            cancellationToken);

        return cosifs
            .Select(cosif => new ProdutoCosifResponse(
                cosif.CodigoProduto,
                cosif.CodigoCosif,
                cosif.CodigoClassificacao))
            .ToList();
    }
}
