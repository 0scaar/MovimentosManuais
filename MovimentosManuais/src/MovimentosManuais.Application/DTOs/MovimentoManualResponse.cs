namespace MovimentosManuais.Application.DTOs;

public sealed record MovimentoManualResponse(
    short Mes,
    short Ano,
    int NumeroLancamento,
    string CodigoProduto,
    string? DescricaoProduto,
    string CodigoCosif,
    string? DescricaoCosif,
    decimal Valor,
    string Descricao,
    DateTime DataMovimento,
    string CodigoUsuario);
