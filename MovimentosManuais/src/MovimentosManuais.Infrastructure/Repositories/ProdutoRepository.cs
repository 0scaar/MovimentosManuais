using Microsoft.EntityFrameworkCore;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Enums;
using MovimentosManuais.Domain.Repositories;
using MovimentosManuais.Infrastructure.Data;

namespace MovimentosManuais.Infrastructure.Repositories;

public sealed class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Produto>> ObterAtivosAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Produtos
            .AsNoTracking()
            .Where(x => x.Status == StatusRegistro.Ativo)
            .OrderBy(x => x.Descricao)
            .ToListAsync(cancellationToken);
    }
}
