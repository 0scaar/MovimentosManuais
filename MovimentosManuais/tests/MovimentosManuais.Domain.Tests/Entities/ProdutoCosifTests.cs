using FluentAssertions;
using MovimentosManuais.Domain.Common;
using MovimentosManuais.Domain.Enums;
using MovimentosManuais.Domain.Tests.TestData;

namespace MovimentosManuais.Domain.Tests.Entities
{
    public sealed class ProdutoCosifTests
    {
        [Fact]
        public void Criar_DeveriaCriarProdutoCosif_QuandoDadosForemValidos()
        {
            var produtoCosif = ProdutoCosifBuilder.Novo().Build();

            produtoCosif.CodigoProduto.Should().Be("0001");
            produtoCosif.CodigoCosif.Should().Be("12345678901");
            produtoCosif.CodigoClassificacao.Should().Be("123456");
            produtoCosif.Status.Should().Be(StatusRegistro.Ativo);
        }

        [Fact]
        public void EstaAtivo_DeveriaRetornarTrue_QuandoStatusForAtivo()
        {
            var produtoCosif = ProdutoCosifBuilder.Novo()
                .ComStatus(StatusRegistro.Ativo)
                .Build();

            produtoCosif.EstaAtivo().Should().BeTrue();
        }

        [Fact]
        public void EstaAtivo_DeveriaRetornarFalse_QuandoStatusForInativo()
        {
            var produtoCosif = ProdutoCosifBuilder.Novo()
                .ComStatus(StatusRegistro.Inativo)
                .Build();

            produtoCosif.EstaAtivo().Should().BeFalse();
        }

        [Fact]
        public void Criar_DeveriaFalhar_QuandoCodigoProdutoForVazio()
        {
            var act = () => ProdutoCosifBuilder.Novo()
                .ComCodigoProduto("")
                .Build();

            act.Should()
                .Throw<DomainException>()
                .WithMessage("O código do produto é obrigatório.");
        }

        [Fact]
        public void Criar_DeveriaFalhar_QuandoCodigoProdutoUltrapassarLimite()
        {
            var act = () => ProdutoCosifBuilder.Novo()
                .ComCodigoProduto("00001")
                .Build();

            act.Should()
                .Throw<DomainException>()
                .WithMessage("O código do produto deve possuir no máximo 4 caracteres.");
        }

        [Fact]
        public void Criar_DeveriaFalhar_QuandoCodigoCosifForVazio()
        {
            var act = () => ProdutoCosifBuilder.Novo()
                .ComCodigoCosif("")
                .Build();

            act.Should()
                .Throw<DomainException>()
                .WithMessage("O código COSIF é obrigatório.");
        }

        [Fact]
        public void Criar_DeveriaFalhar_QuandoCodigoCosifUltrapassarLimite()
        {
            var act = () => ProdutoCosifBuilder.Novo()
                .ComCodigoCosif("123456789012")
                .Build();

            act.Should()
                .Throw<DomainException>()
                .WithMessage("O código COSIF deve possuir no máximo 11 caracteres.");
        }

        [Fact]
        public void Criar_DeveriaFalhar_QuandoCodigoClassificacaoUltrapassarLimite()
        {
            var act = () => ProdutoCosifBuilder.Novo()
                .ComCodigoClassificacao("1234567")
                .Build();

            act.Should()
                .Throw<DomainException>()
                .WithMessage("O código de classificação deve possuir no máximo 6 caracteres.");
        }
    }
}
