namespace MovimentosManuais.Application.DTOs;

public sealed record EditarMovimentoManualRequest(
    decimal Mes,
    decimal Ano,
    decimal NumeroLancamento,
    string CodigoProduto,
    string CodigoCosif,
    decimal Valor,
    string Descricao,
    string CodigoUsuario);
