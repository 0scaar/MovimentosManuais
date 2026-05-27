namespace MovimentosManuais.Application.DTOs;

public sealed record EditarMovimentoManualRequest(
    decimal Valor,
    string Descricao,
    string CodigoUsuario);
