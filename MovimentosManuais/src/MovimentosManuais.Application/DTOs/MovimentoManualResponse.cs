namespace MovimentosManuais.Application.DTOs;

public sealed record MovimentoManualResponse(
    decimal Mes,
    decimal Ano,
    decimal NumeroLancamento,
    string CodigoProduto,
    string? DescricaoProduto,
    string CodigoCosif,
    string? DescricaoCosif,
    decimal Valor,
    string Descricao,
    DateTime DataMovimento,
    string CodigoUsuario);
