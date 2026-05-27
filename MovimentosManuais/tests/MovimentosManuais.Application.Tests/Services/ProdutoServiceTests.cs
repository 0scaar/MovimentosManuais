using FluentAssertions;
using Moq;
using MovimentosManuais.Application.Services;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Enums;
using MovimentosManuais.Domain.Repositories;

namespace MovimentosManuais.Application.Tests.Services;

public sealed class ProdutoServiceTests
{
    [Fact]
    public async Task ListarAtivosAsync_DeveriaRetornarProdutosAtivos()
    {
        var produtos = new List<Produto>
        {
            new("0001", "Produto 1", StatusRegistro.Ativo),
            new("0002", "Produto 2", StatusRegistro.Ativo)
        };

        var produtoRepositoryMock = new Mock<IProdutoRepository>();

        produtoRepositoryMock
            .Setup(x => x.ObterAtivosAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(produtos);

        var service = new ProdutoService(produtoRepositoryMock.Object);

        var response = await service.ListarAtivosAsync(CancellationToken.None);

        response.Should().HaveCount(2);
        response.Should().Contain(x => x.CodigoProduto == "0001");
        response.Should().Contain(x => x.CodigoProduto == "0002");

        produtoRepositoryMock.Verify(
            x => x.ObterAtivosAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}