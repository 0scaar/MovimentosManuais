using FluentValidation;
using MovimentosManuais.Application.Abstractions;
using MovimentosManuais.Application.DTOs;
using MovimentosManuais.Application.Interfaces;
using MovimentosManuais.Domain.Common;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Repositories;

namespace MovimentosManuais.Application.Services;

public sealed class MovimentoManualService : IMovimentoManualService
{
    private readonly IMovimentoManualRepository _movimentoManualRepository;
    private readonly IProdutoCosifRepository _produtoCosifRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CriarMovimentoManualRequest> _validator;

    public MovimentoManualService(
        IMovimentoManualRepository movimentoManualRepository,
        IProdutoCosifRepository produtoCosifRepository,
        IUnitOfWork unitOfWork,
        IValidator<CriarMovimentoManualRequest> validator)
    {
        _movimentoManualRepository = movimentoManualRepository;
        _produtoCosifRepository = produtoCosifRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<MovimentoManualResponse> CriarAsync(
        CriarMovimentoManualRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var produtoCosifExiste = await _produtoCosifRepository.ExisteAsync(
            request.CodigoProduto,
            request.CodigoCosif,
            cancellationToken);

        if (!produtoCosifExiste)
            throw new DomainException("Produto COSIF não encontrado.");

        var proximoNumeroLancamento =
            await _movimentoManualRepository.ObterProximoNumeroLancamentoAsync(
                request.Mes,
                request.Ano,
                cancellationToken);

        var movimentoManual = MovimentoManual.Criar(
            request.Mes,
            request.Ano,
            proximoNumeroLancamento,
            request.CodigoProduto,
            request.CodigoCosif,
            request.Valor,
            request.Descricao,
            request.CodigoUsuario);

        await _movimentoManualRepository.AdicionarAsync(
            movimentoManual,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MovimentoManualResponse(
            movimentoManual.Mes,
            movimentoManual.Ano,
            movimentoManual.NumeroLancamento,
            movimentoManual.CodigoProduto,
            null,
            movimentoManual.CodigoCosif,
            null,
            movimentoManual.Valor,
            movimentoManual.Descricao,
            movimentoManual.DataMovimento,
            movimentoManual.CodigoUsuario);
    }

    public async Task<MovimentoManualResponse> EditarAsync(
    EditarMovimentoManualRequest request,
    CancellationToken cancellationToken)
    {
        var movimentoManual = await _movimentoManualRepository.ObterPorChaveAsync(
            request.Mes,
            request.Ano,
            request.NumeroLancamento,
            cancellationToken);

        if (movimentoManual is null)
            throw new DomainException("Movimento manual não encontrado.");

        var produtoCosifExiste = await _produtoCosifRepository.ExisteAsync(
            request.CodigoProduto,
            request.CodigoCosif,
            cancellationToken);

        if (!produtoCosifExiste)
            throw new DomainException("Produto COSIF não encontrado.");

        movimentoManual.Editar(
            request.CodigoProduto,
            request.CodigoCosif,
            request.Valor,
            request.Descricao,
            request.CodigoUsuario);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MovimentoManualResponse(
            movimentoManual.Mes,
            movimentoManual.Ano,
            movimentoManual.NumeroLancamento,
            movimentoManual.CodigoProduto,
            null,
            movimentoManual.CodigoCosif,
            null,
            movimentoManual.Valor,
            movimentoManual.Descricao,
            movimentoManual.DataMovimento,
            movimentoManual.CodigoUsuario);
    }

    public async Task ExcluirAsync(
    ExcluirMovimentoManualRequest request,
    CancellationToken cancellationToken)
    {
        var movimentoManual = await _movimentoManualRepository.ObterPorChaveAsync(
            request.Mes,
            request.Ano,
            request.NumeroLancamento,
            cancellationToken);

        if (movimentoManual is null)
            throw new DomainException("Movimento manual não encontrado.");

        _movimentoManualRepository.Remover(movimentoManual);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<MovimentoManualResponse>> ListarAsync(
        CancellationToken cancellationToken)
    {
        var movimentos = await _movimentoManualRepository.ListarMovimentosAsync(
            cancellationToken);

        return movimentos
            .Select(movimento => new MovimentoManualResponse(
                movimento.Mes,
                movimento.Ano,
                movimento.NumeroLancamento,
                movimento.CodigoProduto,
                movimento.DescricaoProduto,
                movimento.CodigoCosif,
                movimento.DescricaoCosif,
                movimento.Valor,
                movimento.Descricao,
                movimento.DataMovimento,
                movimento.CodigoUsuario))
            .ToList();
    }
}
