namespace MovimentosManuais.Application.DTOs;

public sealed record CriarMovimentoManualRequest(
    short Mes,
    short Ano,
    string CodigoProduto,
    string CodigoCosif,
    decimal Valor,
    string Descricao,
    string CodigoUsuario);
