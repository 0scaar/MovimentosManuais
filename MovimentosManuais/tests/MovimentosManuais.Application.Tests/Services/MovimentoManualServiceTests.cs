using FluentAssertions;
using FluentValidation;
using Moq;
using MovimentosManuais.Application.Abstractions;
using MovimentosManuais.Application.DTOs;
using MovimentosManuais.Application.Services;
using MovimentosManuais.Application.Tests.TestData;
using MovimentosManuais.Application.Validators;
using MovimentosManuais.Domain.Common;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Repositories;

namespace MovimentosManuais.Application.Tests.Services;

public sealed class MovimentoManualServiceTests
{
    private readonly Mock<IMovimentoManualRepository> _movimentoManualRepositoryMock = new();
    private readonly Mock<IMovimentoManualConsultaRepository> _movimentoManualConsultaRepositoryMock = new();
    private readonly Mock<IProdutoCosifRepository> _produtoCosifRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private readonly IValidator<CriarMovimentoManualRequest> _validator =
        new CriarMovimentoManualRequestValidator();

    private MovimentoManualService CriarService()
    {
        return new MovimentoManualService(
            _movimentoManualRepositoryMock.Object,
            _movimentoManualConsultaRepositoryMock.Object,
            _produtoCosifRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _validator);
    }

    [Fact]
    public async Task CriarAsync_DeveriaCriarMovimentoManual_QuandoRequestForValido()
    {
        var request = CriarMovimentoManualRequestBuilder.Novo().Build();

        _produtoCosifRepositoryMock
            .Setup(x => x.ExisteAsync(
                request.CodigoProduto,
                request.CodigoCosif,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _movimentoManualRepositoryMock
            .Setup(x => x.ObterProximoNumeroLancamentoAsync(
                request.Mes,
                request.Ano,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var service = CriarService();

        var response = await service.CriarAsync(request, CancellationToken.None);

        response.Mes.Should().Be(request.Mes);
        response.Ano.Should().Be(request.Ano);
        response.NumeroLancamento.Should().Be(1);
        response.CodigoProduto.Should().Be(request.CodigoProduto);
        response.CodigoCosif.Should().Be(request.CodigoCosif);
        response.Valor.Should().Be(request.Valor);
        response.Descricao.Should().Be(request.Descricao);

        _movimentoManualRepositoryMock.Verify(
            x => x.AdicionarAsync(
                It.IsAny<MovimentoManual>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CriarAsync_DeveriaFalhar_QuandoRequestForInvalido()
    {
        var request = CriarMovimentoManualRequestBuilder.Novo()
            .ComMes(13)
            .Build();

        var service = CriarService();

        var act = async () => await service.CriarAsync(request, CancellationToken.None);

        await act.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("*O mês deve estar entre 1 e 12.*");

        _movimentoManualRepositoryMock.Verify(
            x => x.AdicionarAsync(
                It.IsAny<MovimentoManual>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CriarAsync_DeveriaFalhar_QuandoProdutoCosifNaoExistir()
    {
        var request = CriarMovimentoManualRequestBuilder.Novo().Build();

        _produtoCosifRepositoryMock
            .Setup(x => x.ExisteAsync(
                request.CodigoProduto,
                request.CodigoCosif,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var service = CriarService();

        var act = async () => await service.CriarAsync(request, CancellationToken.None);

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Produto COSIF não encontrado.");

        _movimentoManualRepositoryMock.Verify(
            x => x.AdicionarAsync(
                It.IsAny<MovimentoManual>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task EditarAsync_DeveriaEditarMovimentoManual_QuandoMovimentoExistir()
    {
        const short mes = 5;
        const short ano = 2026;
        const int numeroLancamento = 1;
        const string codigoProduto = "0001";
        const string codigoCosif = "12345678901";
        var request = EditarMovimentoManualRequestBuilder.Novo().Build();

        var movimento = MovimentoManual.Criar(
            mes,
            ano,
            numeroLancamento,
            codigoProduto,
            codigoCosif,
            100,
            "Descrição antiga",
            request.CodigoUsuario);

        _movimentoManualRepositoryMock
            .Setup(x => x.ObterPorChaveCompletaAsync(
                mes,
                ano,
                numeroLancamento,
                codigoProduto,
                codigoCosif,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(movimento);

        var service = CriarService();

        var response = await service.EditarAsync(
            mes,
            ano,
            numeroLancamento,
            codigoProduto,
            codigoCosif,
            request,
            CancellationToken.None);

        response.Valor.Should().Be(request.Valor);
        response.Descricao.Should().Be(request.Descricao);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task EditarAsync_DeveriaFalhar_QuandoMovimentoNaoExistir()
    {
        const short mes = 5;
        const short ano = 2026;
        const int numeroLancamento = 1;
        const string codigoProduto = "0001";
        const string codigoCosif = "12345678901";
        var request = EditarMovimentoManualRequestBuilder.Novo().Build();

        _movimentoManualRepositoryMock
            .Setup(x => x.ObterPorChaveCompletaAsync(
                mes,
                ano,
                numeroLancamento,
                codigoProduto,
                codigoCosif,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((MovimentoManual?)null);

        var service = CriarService();

        var act = async () => await service.EditarAsync(
            mes,
            ano,
            numeroLancamento,
            codigoProduto,
            codigoCosif,
            request,
            CancellationToken.None);

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Movimento manual não encontrado para a chave informada.");

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task ExcluirAsync_DeveriaExcluirMovimentoManual_QuandoMovimentoExistir()
    {
        var request = new ExcluirMovimentoManualRequest(5, 2026, 1, "0001", "12345678901");

        var movimento = MovimentoManual.Criar(
            5,
            2026,
            1,
            "0001",
            "12345678901",
            100,
            "Movimento teste",
            "usuario");

        _movimentoManualRepositoryMock
            .Setup(x => x.ObterPorChaveCompletaAsync(
                request.Mes,
                request.Ano,
                request.NumeroLancamento,
                request.CodigoProduto,
                request.CodigoCosif,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(movimento);

        var service = CriarService();

        await service.ExcluirAsync(request, CancellationToken.None);

        _movimentoManualRepositoryMock.Verify(
            x => x.Remover(movimento),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ExcluirAsync_DeveriaFalhar_QuandoMovimentoNaoExistir()
    {
        var request = new ExcluirMovimentoManualRequest(5, 2026, 1, "0001", "12345678901");

        _movimentoManualRepositoryMock
            .Setup(x => x.ObterPorChaveCompletaAsync(
                request.Mes,
                request.Ano,
                request.NumeroLancamento,
                request.CodigoProduto,
                request.CodigoCosif,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((MovimentoManual?)null);

        var service = CriarService();

        var act = async () => await service.ExcluirAsync(request, CancellationToken.None);

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Movimento manual não encontrado para a chave informada.");

        _movimentoManualRepositoryMock.Verify(
            x => x.Remover(It.IsAny<MovimentoManual>()),
            Times.Never);
    }
}
