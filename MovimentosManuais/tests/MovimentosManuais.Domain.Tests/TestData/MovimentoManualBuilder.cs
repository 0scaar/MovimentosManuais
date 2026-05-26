using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Domain.Tests.TestData;

public sealed class MovimentoManualBuilder
{
    private decimal _mes = 5;
    private decimal _ano = 2026;
    private decimal _numeroLancamento = 1;
    private string _codigoProduto = "0001";
    private string _codigoCosif = "12345678901";
    private decimal _valor = 100;
    private string _descricao = "Movimento manual teste";
    private string _codigoUsuario = "usuario";

    public static MovimentoManualBuilder Novo()
    {
        return new MovimentoManualBuilder();
    }

    public MovimentoManualBuilder ComMes(decimal mes)
    {
        _mes = mes;
        return this;
    }

    public MovimentoManualBuilder ComAno(decimal ano)
    {
        _ano = ano;
        return this;
    }

    public MovimentoManualBuilder ComNumeroLancamento(decimal numeroLancamento)
    {
        _numeroLancamento = numeroLancamento;
        return this;
    }

    public MovimentoManualBuilder ComCodigoProduto(string codigoProduto)
    {
        _codigoProduto = codigoProduto;
        return this;
    }

    public MovimentoManualBuilder ComCodigoCosif(string codigoCosif)
    {
        _codigoCosif = codigoCosif;
        return this;
    }

    public MovimentoManualBuilder ComValor(decimal valor)
    {
        _valor = valor;
        return this;
    }

    public MovimentoManualBuilder ComDescricao(string descricao)
    {
        _descricao = descricao;
        return this;
    }

    public MovimentoManualBuilder ComCodigoUsuario(string codigoUsuario)
    {
        _codigoUsuario = codigoUsuario;
        return this;
    }

    public MovimentoManual Build()
    {
        return MovimentoManual.Criar(
            _mes,
            _ano,
            _numeroLancamento,
            _codigoProduto,
            _codigoCosif,
            _valor,
            _descricao,
            _codigoUsuario);
    }
}
