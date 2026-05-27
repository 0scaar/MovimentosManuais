namespace MovimentosManuais.Application.DTOs;

public sealed record ExcluirMovimentoManualRequest(
    short Mes,
    short Ano,
    int NumeroLancamento);
