namespace MovimentosManuais.Application.DTOs;

public sealed record ProdutoCosifResponse(
    string CodigoProduto,
    string CodigoCosif,
    string? CodigoClassificacao);
