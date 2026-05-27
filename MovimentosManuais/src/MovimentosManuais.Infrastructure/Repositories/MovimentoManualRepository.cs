using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.ReadModels;
using MovimentosManuais.Domain.Repositories;
using MovimentosManuais.Infrastructure.Data;
using System.Data;

namespace MovimentosManuais.Infrastructure.Repositories;

public sealed class MovimentoManualRepository : IMovimentoManualRepository
{
    private readonly AppDbContext _context;
    private readonly string _connectionString;

    public MovimentoManualRepository(
        AppDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _connectionString =
            configuration.GetConnectionString("MovimentosManuaisDb")
            ?? configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'MovimentosManuaisDb' was not found.");
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

    public async Task<IReadOnlyCollection<MovimentoManualConsulta>> ListarMovimentosAsync(
        CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);

        var movimentos = await connection.QueryAsync<MovimentoManualConsulta>(
            new CommandDefinition(
                commandText: "sp_MovimentoManual_Listar",
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken));

        return movimentos.ToList();
    }
}
