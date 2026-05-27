namespace MovimentosManuais.Application.DTOs;

public sealed record ExcluirMovimentoManualRequest(
    decimal Mes,
    decimal Ano,
    decimal NumeroLancamento);
