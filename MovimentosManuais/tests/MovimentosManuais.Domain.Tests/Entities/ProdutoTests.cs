using FluentAssertions;
using MovimentosManuais.Domain.Common;
using MovimentosManuais.Domain.Enums;
using MovimentosManuais.Domain.Tests.TestData;

namespace MovimentosManuais.Domain.Tests.Entities;

public sealed class ProdutoTests
{
    [Fact]
    public void Criar_DeveriaCriarProduto_QuandoDadosForemValidos()
    {
        var produto = ProdutoBuilder.Novo().Build();

        produto.CodigoProduto.Should().Be("0001");
        produto.Descricao.Should().Be("Produto Teste");
        produto.Status.Should().Be(StatusRegistro.Ativo);
    }

    [Fact]
    public void EstaAtivo_DeveriaRetornarTrue_QuandoStatusForAtivo()
    {
        var produto = ProdutoBuilder.Novo()
            .ComStatus(StatusRegistro.Ativo)
            .Build();

        produto.EstaAtivo().Should().BeTrue();
    }

    [Fact]
    public void EstaAtivo_DeveriaRetornarFalse_QuandoStatusForInativo()
    {
        var produto = ProdutoBuilder.Novo()
            .ComStatus(StatusRegistro.Inativo)
            .Build();

        produto.EstaAtivo().Should().BeFalse();
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoCodigoProdutoForVazio()
    {
        var act = () => ProdutoBuilder.Novo()
            .ComCodigoProduto("")
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O código do produto é obrigatório.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoCodigoProdutoUltrapassarLimite()
    {
        var act = () => ProdutoBuilder.Novo()
            .ComCodigoProduto("00001")
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("O código do produto deve possuir no máximo 4 caracteres.");
    }

    [Fact]
    public void Criar_DeveriaFalhar_QuandoDescricaoUltrapassarLimite()
    {
        var descricao = new string('A', 31);

        var act = () => ProdutoBuilder.Novo()
            .ComDescricao(descricao)
            .Build();

        act.Should()
            .Throw<DomainException>()
            .WithMessage("A descrição do produto deve possuir no máximo 30 caracteres.");
    }
}
