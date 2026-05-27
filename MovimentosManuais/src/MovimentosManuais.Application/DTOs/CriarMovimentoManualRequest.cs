namespace MovimentosManuais.Application.DTOs;

public sealed record CriarMovimentoManualRequest(
    decimal Mes,
    decimal Ano,
    string CodigoProduto,
    string CodigoCosif,
    decimal Valor,
    string Descricao,
    string CodigoUsuario);
