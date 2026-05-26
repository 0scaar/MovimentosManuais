using FluentAssertions;
using MovimentosManuais.Domain.Common;
using MovimentosManuais.Domain.Tests.TestData;

namespace MovimentosManuais.Domain.Tests.Entities;

public sealed class MovimentoManualTests
{
    [Fact]
    public void Criar_DeveriaCriarMovimentoManual_QuandoDadosForemValidos()
    {
        var movimento = MovimentoManualBuilder.Novo().Build();

        movimento.Mes.Should().Be(5);
        movimento.Ano.Should().Be(2026);
        movimento.NumeroLancamento.Should().Be(1);
        movimento.CodigoProduto.Should().Be("0001");
        movimento.CodigoCosif.Should().Be("12345678901");
        movimento.Valor.Should().Be(100);
        movimento.Descricao.Should().Be("Movimento manual teste");
        movimento.CodigoUsuario.Should().Be("usuario");
        movimento.DataMovimento.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(13)]
    public void Criar_DeveriaFalhar_QuandoMesForInvalido(decimal mes)
    {
        var act = () => MovimentoManualBuilder.Novo()
            .ComMes(mes)
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O mês deve estar entre 1 e 12.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoAnoForInvalido()
    {
        var act = () => MovimentoManualBuilder.Novo()
            .ComAno(1899)
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O ano informado é inválido.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoNumeroLancamentoForZero()
    {
        var act = () => MovimentoManualBuilder.Novo()
            .ComNumeroLancamento(0)
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O número do lançamento deve ser maior que zero.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoCodigoProdutoForVazio()
    {
        var act = () => MovimentoManualBuilder.Novo()
            .ComCodigoProduto("")
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O código do produto é obrigatório.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoCodigoProdutoUltrapassarLimite()
    {
        var act = () => MovimentoManualBuilder.Novo()
            .ComCodigoProduto("00001")
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O código do produto deve possuir no máximo 4 caracteres.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoCodigoCosifForVazio()
    {
        var act = () => MovimentoManualBuilder.Novo()
            .ComCodigoCosif("")
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O código COSIF é obrigatório.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoCodigoCosifUltrapassarLimite()
    {
        var act = () => MovimentoManualBuilder.Novo()
            .ComCodigoCosif("123456789012")
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O código COSIF deve possuir no máximo 11 caracteres.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoValorForZero()
    {
        var act = () => MovimentoManualBuilder.Novo()
            .ComValor(0)
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O valor do movimento deve ser maior que zero.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoDescricaoForVazia()
    {
        var act = () => MovimentoManualBuilder.Novo()
            .ComDescricao("")
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("A descrição é obrigatória.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoDescricaoUltrapassarLimite()
    {
        var descricao = new string('A', 51);

        var act = () => MovimentoManualBuilder.Novo()
            .ComDescricao(descricao)
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("A descrição deve possuir no máximo 50 caracteres.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoCodigoUsuarioForVazio()
    {
        var act = () => MovimentoManualBuilder.Novo()
            .ComCodigoUsuario("")
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O código do usuário é obrigatório.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoCodigoUsuarioUltrapassarLimite()
    {
        var act = () => MovimentoManualBuilder.Novo()
            .ComCodigoUsuario("usuario-com-mais-de-15-caracteres")
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O código do usuário deve possuir no máximo 15 caracteres.");
    }
}
