namespace MovimentosManuais.Domain.ReadModels;

public sealed record MovimentoManualConsulta(
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
