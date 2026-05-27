using MovimentosManuais.Domain.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovimentosManuais.Domain.Entities;

public sealed class MovimentoManual : Entity
{
    public decimal Mes { get; private set; }
    public decimal Ano { get; private set; }
    public decimal NumeroLancamento { get; private set; }
    public string CodigoProduto { get; private set; }
    public string CodigoCosif { get; private set; }
    public decimal Valor { get; private set; }
    public string Descricao { get; private set; }
    public DateTime DataMovimento { get; private set; }
    public string CodigoUsuario { get; private set; }

    public ProdutoCosif? ProdutoCosif { get; private set; }

    private MovimentoManual()
    {
        CodigoProduto = string.Empty;
        CodigoCosif = string.Empty;
        Descricao = string.Empty;
        CodigoUsuario = string.Empty;
    }

    public MovimentoManual(
        decimal mes,
        decimal ano,
        decimal numeroLancamento,
        string codigoProduto,
        string codigoCosif,
        decimal valor,
        string descricao,
        DateTime dataMovimento,
        string codigoUsuario)
    {
        Validar(mes, ano, numeroLancamento, codigoProduto, codigoCosif, valor, descricao, codigoUsuario);

        Mes = mes;
        Ano = ano;
        NumeroLancamento = numeroLancamento;
        CodigoProduto = codigoProduto;
        CodigoCosif = codigoCosif;
        Valor = valor;
        Descricao = descricao;
        DataMovimento = dataMovimento;
        CodigoUsuario = codigoUsuario;
    }

    public static MovimentoManual Criar(
        decimal mes,
        decimal ano,
        decimal numeroLancamento,
        string codigoProduto,
        string codigoCosif,
        decimal valor,
        string descricao,
        string codigoUsuario)
    {
        return new MovimentoManual(
            mes,
            ano,
            numeroLancamento,
            codigoProduto,
            codigoCosif,
            valor,
            descricao,
            DateTime.Now,
            codigoUsuario);
    }

    public void Editar(
    string codigoProduto,
    string codigoCosif,
    decimal valor,
    string descricao,
    string codigoUsuario)
    {
        Validar(Mes, Ano, NumeroLancamento, codigoProduto, codigoCosif, valor, descricao, codigoUsuario);

        CodigoProduto = codigoProduto;
        CodigoCosif = codigoCosif;
        Valor = valor;
        Descricao = descricao;
        CodigoUsuario = codigoUsuario;
    }

    private static void Validar(
    decimal mes,
    decimal ano,
    decimal numeroLancamento,
    string codigoProduto,
    string codigoCosif,
    decimal valor,
    string descricao,
    string codigoUsuario)
    {
        DomainValidation.Between(mes, 1, 12, "O mês deve estar entre 1 e 12.");
        DomainValidation.GreaterThanOrEqual(ano, 1900, "O ano informado é inválido.");
        DomainValidation.GreaterThan(numeroLancamento, 0, "O número do lançamento deve ser maior que zero.");

        DomainValidation.NotNullOrWhiteSpace(codigoProduto, "O código do produto é obrigatório.");
        DomainValidation.MaxLength(codigoProduto, 4, "O código do produto deve possuir no máximo 4 caracteres.");

        DomainValidation.NotNullOrWhiteSpace(codigoCosif, "O código COSIF é obrigatório.");
        DomainValidation.MaxLength(codigoCosif, 11, "O código COSIF deve possuir no máximo 11 caracteres.");

        DomainValidation.GreaterThan(valor, 0, "O valor do movimento deve ser maior que zero.");

        DomainValidation.NotNullOrWhiteSpace(descricao, "A descrição é obrigatória.");
        DomainValidation.MaxLength(descricao, 50, "A descrição deve possuir no máximo 50 caracteres.");

        DomainValidation.NotNullOrWhiteSpace(codigoUsuario, "O código do usuário é obrigatório.");
        DomainValidation.MaxLength(codigoUsuario, 15, "O código do usuário deve possuir no máximo 15 caracteres.");
    }
}
