using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MovimentosManuais.Domain.ReadModels;
using MovimentosManuais.Domain.Repositories;
using System.Data;

namespace MovimentosManuais.Infrastructure.Repositories;

public sealed class MovimentoManualConsultaRepository : IMovimentoManualConsultaRepository
{
    private readonly string _connectionString;

    public MovimentoManualConsultaRepository(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("MovimentosManuaisDb")
            ?? configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'MovimentosManuaisDb' was not found.");
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
