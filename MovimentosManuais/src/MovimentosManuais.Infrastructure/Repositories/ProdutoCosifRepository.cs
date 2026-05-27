using Microsoft.EntityFrameworkCore;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Enums;
using MovimentosManuais.Domain.Repositories;
using MovimentosManuais.Infrastructure.Data;

namespace MovimentosManuais.Infrastructure.Repositories;

public sealed class ProdutoCosifRepository : IProdutoCosifRepository
{
    private readonly AppDbContext _context;

    public ProdutoCosifRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ProdutoCosif>> ObterAtivosPorProdutoAsync(
        string codigoProduto,
        CancellationToken cancellationToken)
    {
        return await _context.ProdutosCosif
            .AsNoTracking()
            .Where(x =>
                x.CodigoProduto == codigoProduto &&
                x.Status == StatusRegistro.Ativo)
            .OrderBy(x => x.CodigoCosif)
            .ToListAsync(cancellationToken);
    }

    public Task<bool> ExisteAsync(
        string codigoProduto,
        string codigoCosif,
        CancellationToken cancellationToken)
    {
        return _context.ProdutosCosif
            .AsNoTracking()
            .AnyAsync(x =>
                x.CodigoProduto == codigoProduto &&
                x.CodigoCosif == codigoCosif &&
                x.Status == StatusRegistro.Ativo,
                cancellationToken);
    }
}
