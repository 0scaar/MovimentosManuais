using Microsoft.AspNetCore.Mvc;
using MovimentosManuais.Application.DTOs;
using MovimentosManuais.Application.Interfaces;

namespace MovimentosManuais.Api.Controllers;

[ApiController]
[Route("api/movimentos-manuais")]
public sealed class MovimentosManuaisController : ControllerBase
{
    private readonly IMovimentoManualService _movimentoManualService;

    public MovimentosManuaisController(
        IMovimentoManualService movimentoManualService)
    {
        _movimentoManualService = movimentoManualService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarAsync(
        CancellationToken cancellationToken)
    {
        var movimentos = await _movimentoManualService.ListarAsync(cancellationToken);

        return Ok(movimentos);
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync(
        [FromBody] CriarMovimentoManualRequest request,
        CancellationToken cancellationToken)
    {
        var movimento = await _movimentoManualService.CriarAsync(
            request,
            cancellationToken);

        return Created(string.Empty, movimento);
    }

    [HttpPut("{mes:int}/{ano:int}/{numeroLancamento:int}/{codigoProduto}/{codigoCosif}")]
    public async Task<IActionResult> EditarAsync(
        short mes,
        short ano,
        int numeroLancamento,
        string codigoProduto,
        string codigoCosif,
        [FromBody] EditarMovimentoManualRequest request,
        CancellationToken cancellationToken)
    {
        var movimento = await _movimentoManualService.EditarAsync(
            mes,
            ano,
            numeroLancamento,
            codigoProduto,
            codigoCosif,
            request,
            cancellationToken);

        return Ok(movimento);
    }

    [HttpDelete("{mes:int}/{ano:int}/{numeroLancamento:int}/{codigoProduto}/{codigoCosif}")]
    public async Task<IActionResult> ExcluirAsync(
        short mes,
        short ano,
        int numeroLancamento,
        string codigoProduto,
        string codigoCosif,
        CancellationToken cancellationToken)
    {
        var request = new ExcluirMovimentoManualRequest(
            mes,
            ano,
            numeroLancamento,
            codigoProduto,
            codigoCosif);

        await _movimentoManualService.ExcluirAsync(
            request,
            cancellationToken);

        return NoContent();
    }
}
