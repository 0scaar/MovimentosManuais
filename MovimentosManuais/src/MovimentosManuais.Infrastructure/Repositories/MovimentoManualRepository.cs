using Microsoft.EntityFrameworkCore;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Repositories;
using MovimentosManuais.Infrastructure.Data;

namespace MovimentosManuais.Infrastructure.Repositories;

public sealed class MovimentoManualRepository : IMovimentoManualRepository
{
    private readonly AppDbContext _context;

    public MovimentoManualRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> ObterProximoNumeroLancamentoAsync(
        short mes,
        short ano,
        CancellationToken cancellationToken)
    {
        var ultimoLancamento = await _context.MovimentosManuais
            .AsNoTracking()
            .Where(x => x.Mes == mes && x.Ano == ano)
            .MaxAsync(x => (int?)x.NumeroLancamento, cancellationToken);

        return (ultimoLancamento ?? 0) + 1;
    }

    public Task<MovimentoManual?> ObterPorChaveAsync(
        short mes,
        short ano,
        int numeroLancamento,
        CancellationToken cancellationToken)
    {
        return _context.MovimentosManuais
            .FirstOrDefaultAsync(x =>
                x.Mes == mes &&
                x.Ano == ano &&
                x.NumeroLancamento == numeroLancamento,
                cancellationToken);
    }

    public Task<MovimentoManual?> ObterPorChaveCompletaAsync(
        short mes,
        short ano,
        int numeroLancamento,
        string codigoProduto,
        string codigoCosif,
        CancellationToken cancellationToken)
    {
        return _context.MovimentosManuais
            .FirstOrDefaultAsync(x =>
                x.Mes == mes &&
                x.Ano == ano &&
                x.NumeroLancamento == numeroLancamento &&
                x.CodigoProduto == codigoProduto &&
                x.CodigoCosif == codigoCosif,
                cancellationToken);
    }

    public async Task AdicionarAsync(
        MovimentoManual movimentoManual,
        CancellationToken cancellationToken)
    {
        await _context.MovimentosManuais.AddAsync(
            movimentoManual,
            cancellationToken);
    }

    public void Remover(MovimentoManual movimentoManual)
    {
        _context.MovimentosManuais.Remove(movimentoManual);
    }
}
