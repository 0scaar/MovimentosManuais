using MovimentosManuais.Domain.Common;

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
        ValidarMes(mes);
        ValidarAno(ano);
        ValidarNumeroLancamento(numeroLancamento);
        ValidarCodigoProduto(codigoProduto);
        ValidarCodigoCosif(codigoCosif);
        ValidarValor(valor);
        ValidarDescricao(descricao);
        ValidarCodigoUsuario(codigoUsuario);

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

    private static void ValidarMes(decimal mes)
    {
        if (mes < 1 || mes > 12)
            throw new DomainException("O mês deve estar entre 1 e 12.");
    }

    private static void ValidarAno(decimal ano)
    {
        if (ano < 1900)
            throw new DomainException("O ano informado é inválido.");
    }

    private static void ValidarNumeroLancamento(decimal numeroLancamento)
    {
        if (numeroLancamento <= 0)
            throw new DomainException("O número do lançamento deve ser maior que zero.");
    }

    private static void ValidarCodigoProduto(string codigoProduto)
    {
        if (string.IsNullOrWhiteSpace(codigoProduto))
            throw new DomainException("O código do produto é obrigatório.");

        if (codigoProduto.Length > 4)
            throw new DomainException("O código do produto deve possuir no máximo 4 caracteres.");
    }

    private static void ValidarCodigoCosif(string codigoCosif)
    {
        if (string.IsNullOrWhiteSpace(codigoCosif))
            throw new DomainException("O código COSIF é obrigatório.");

        if (codigoCosif.Length > 11)
            throw new DomainException("O código COSIF deve possuir no máximo 11 caracteres.");
    }

    private static void ValidarValor(decimal valor)
    {
        if (valor <= 0)
            throw new DomainException("O valor do movimento deve ser maior que zero.");
    }

    private static void ValidarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("A descrição é obrigatória.");

        if (descricao.Length > 50)
            throw new DomainException("A descrição deve possuir no máximo 50 caracteres.");
    }

    private static void ValidarCodigoUsuario(string codigoUsuario)
    {
        if (string.IsNullOrWhiteSpace(codigoUsuario))
            throw new DomainException("O código do usuário é obrigatório.");

        if (codigoUsuario.Length > 15)
            throw new DomainException("O código do usuário deve possuir no máximo 15 caracteres.");
    }
}
