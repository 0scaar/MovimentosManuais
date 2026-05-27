namespace MovimentosManuais.Application.DTOs;

public sealed record EditarMovimentoManualRequest(
    short Mes,
    short Ano,
    int NumeroLancamento,
    string CodigoProduto,
    string CodigoCosif,
    decimal Valor,
    string Descricao,
    string CodigoUsuario);
