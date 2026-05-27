using FluentAssertions;
using Moq;
using MovimentosManuais.Application.Services;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Enums;
using MovimentosManuais.Domain.Repositories;

namespace MovimentosManuais.Application.Tests.Services;

public sealed class ProdutoCosifServiceTests
{
    [Fact]
    public async Task ListarAtivosPorProdutoAsync_DeveriaRetornarCosifsDoProduto()
    {
        var codigoProduto = "0001";

        var cosifs = new List<ProdutoCosif>
        {
            new("0001", "12345678901", "123456", StatusRegistro.Ativo),
            new("0001", "98765432109", "654321", StatusRegistro.Ativo)
        };

        var produtoCosifRepositoryMock = new Mock<IProdutoCosifRepository>();

        produtoCosifRepositoryMock
            .Setup(x => x.ObterAtivosPorProdutoAsync(
                codigoProduto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(cosifs);

        var service = new ProdutoCosifService(produtoCosifRepositoryMock.Object);

        var response = await service.ListarAtivosPorProdutoAsync(
            codigoProduto,
            CancellationToken.None);

        response.Should().HaveCount(2);
        response.Should().OnlyContain(x => x.CodigoProduto == codigoProduto);

        produtoCosifRepositoryMock.Verify(
            x => x.ObterAtivosPorProdutoAsync(
                codigoProduto,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}